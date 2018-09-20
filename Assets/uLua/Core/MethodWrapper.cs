using System;
using System.Reflection;

namespace LuaInterface
{
    struct MethodCache
    {
        private MethodBase _cachedMethod;

        public MethodBase cachedMethod
        {
            get
            {
                return _cachedMethod;
            }
            set
            {
                _cachedMethod = value;
                MethodInfo mi = value as MethodInfo;
                if (mi != null) IsReturnVoid = mi.ReturnType == typeof(void);
            }
        }

        public bool IsReturnVoid;

        // List or arguments, 
        // 这个会缓存每一次调用某个函数的参数，导致内存释放不了。
        // 修改方案: 调用完成后，或中间出错退出时一定要清空这个数组的所有元素
        public object[] args;
        // Positions of out parameters
        public int[] outList;
        // Types of parameters
        public MethodArgs[] argTypes;
    }


    struct MethodArgs
    {
        // Position of parameter
        public int index;
        // Type-conversion function
        public ExtractValue extractValue;

        public bool isParamsArray;

        public Type paramsArrayType;
    }

    delegate object ExtractValue(IntPtr luaState, int stackPos);

    class LuaMethodWrapper
    {
        private ObjectTranslator _Translator;
        private MethodCache _LastCalledMethod = new MethodCache();
        private string _MethodName;
        private MemberInfo[] _Members;
        public IReflect _TargetType;
        private ExtractValue _ExtractTarget;
        private BindingFlags _BindingType;

        public LuaMethodWrapper(ObjectTranslator translator, IReflect targetType, string methodName, BindingFlags bindingType)
        {
            _Translator = translator;
            _MethodName = methodName;
            _TargetType = targetType;

            if (targetType != null)
                _ExtractTarget = translator.typeChecker.getExtractor(targetType);

            _BindingType = bindingType;

            // Removed NonPublic binding search and added IgnoreCase
            _Members = targetType.UnderlyingSystemType.GetMember(methodName, MemberTypes.Method, bindingType | BindingFlags.Public | BindingFlags.IgnoreCase/*|BindingFlags.NonPublic*/);
        }

        int SetPendingException(Exception e)
        {
            return _Translator.interpreter.SetPendingException(e);
        }

        // 清空缓存的 args 数组，否则这里会造成内存泄露，无法被GC掉, 纹理内存最严重
        private void ClearCachedArgs()
        {
            if (_LastCalledMethod.args == null) { return; }
            for (int i = 0; i < _LastCalledMethod.args.Length; i++)
            {
                _LastCalledMethod.args[i] = null;
            }
        }

        public int call(IntPtr luaState)
        {
            object targetObject;
            bool failedCall = true;
            int nReturnValues = 0;

            if (!LuaAPI.lua_checkstack(luaState, 5))
                throw new LuaException("Lua stack overflow");

            bool isStatic = (_BindingType & BindingFlags.Static) == BindingFlags.Static;

            SetPendingException(null);

            if (isStatic)
                targetObject = null;
            else
                targetObject = _ExtractTarget(luaState, 1);

            if (_LastCalledMethod.cachedMethod != null) // Cached?
            {
                int numStackToSkip = isStatic ? 0 : 1; // If this is an instance invoe we will have an extra arg on the stack for the targetObject
                int numArgsPassed = LuaAPI.lua_gettop(luaState) - numStackToSkip;
                MethodBase method = _LastCalledMethod.cachedMethod;

                if (numArgsPassed == _LastCalledMethod.argTypes.Length) // No. of args match?
                {
                    if (!LuaAPI.lua_checkstack(luaState, _LastCalledMethod.outList.Length + 6))
                        throw new LuaException("Lua stack overflow");

                    //这里 args 只是将 _LastCalledMethod.args 拿来做缓冲区用，避免内存再分配, 里面的值是可以干掉的
                    object[] args = _LastCalledMethod.args;

                    try
                    {
                        for (int i = 0; i < _LastCalledMethod.argTypes.Length; i++)
                        {
                            MethodArgs type = _LastCalledMethod.argTypes[i];
                            object luaParamValue = type.extractValue(luaState, i + 1 + numStackToSkip);
                            if (_LastCalledMethod.argTypes[i].isParamsArray)
                            {
                                args[type.index] = _Translator.tableToArray(luaParamValue, type.paramsArrayType);
                            }
                            else
                            {
                                args[type.index] = luaParamValue;
                            }

                            if (args[type.index] == null &&
                                !LuaAPI.lua_isnil(luaState, i + 1 + numStackToSkip))
                            {
                                throw new LuaException("argument number " + (i + 1) + " is invalid");
                            }
                        }
                        if ((_BindingType & BindingFlags.Static) == BindingFlags.Static)
                        {
                            _Translator.push(luaState, method.Invoke(null, args));
                        }
                        else
                        {
                            if (_LastCalledMethod.cachedMethod.IsConstructor)
                                _Translator.push(luaState, ((ConstructorInfo)method).Invoke(args));
                            else
                                _Translator.push(luaState, method.Invoke(targetObject, args));
                        }
                        failedCall = false;
                    }
                    catch (TargetInvocationException e)
                    {
                        return SetPendingException(e.GetBaseException());
                    }
                    catch (Exception e)
                    {
                        if (_Members.Length == 1)
                            return SetPendingException(e);
                    }
                }
            }

            // Cache miss
            if (failedCall)
            {
                if (!isStatic)
                {
                    if (targetObject == null)
                    {
                        _Translator.throwError(luaState, String.Format("instance method '{0}' requires a non null target object", _MethodName));
                        LuaAPI.lua_pushnil(luaState);
                        return 1;
                    }
                    LuaAPI.lua_remove(luaState, 1); // Pops the receiver
                }

                bool hasMatch = false;
                string candidateName = null;

                foreach (MemberInfo member in _Members)
                {
                    candidateName = member.ReflectedType.Name + "." + member.Name;
                    MethodBase m = (MethodInfo)member;
                    bool isMethod = _Translator.matchParameters(luaState, m, ref _LastCalledMethod);
                    if (isMethod)
                    {
                        hasMatch = true;
                        break;
                    }
                }
                if (!hasMatch)
                {
                    string msg = (candidateName == null)
                        ? "invalid arguments to method call"
                        : ("invalid arguments to method: " + candidateName);

                    LuaAPI.luaL_error(luaState, msg);
                    LuaAPI.lua_pushnil(luaState);
                    ClearCachedArgs();
                    return 1;
                }
            }

            if (failedCall)
            {
                if (!LuaAPI.lua_checkstack(luaState, _LastCalledMethod.outList.Length + 6))
                {
                    ClearCachedArgs();
                    throw new LuaException("Lua stack overflow");
                }
                try
                {
                    if (isStatic)
                    {
                        _Translator.push(luaState, _LastCalledMethod.cachedMethod.Invoke(null, _LastCalledMethod.args));
                    }
                    else
                    {
                        if (_LastCalledMethod.cachedMethod.IsConstructor)
                            _Translator.push(luaState, ((ConstructorInfo)_LastCalledMethod.cachedMethod).Invoke(_LastCalledMethod.args));
                        else
                            _Translator.push(luaState, _LastCalledMethod.cachedMethod.Invoke(targetObject, _LastCalledMethod.args));
                    }
                }
                catch (TargetInvocationException e)
                {
                    ClearCachedArgs();
                    return SetPendingException(e.GetBaseException());
                }
                catch (Exception e)
                {
                    ClearCachedArgs();
                    return SetPendingException(e);
                }
            }

            // Pushes out and ref return values
            for (int index = 0; index < _LastCalledMethod.outList.Length; index++)
            {
                nReturnValues++;
                _Translator.push(luaState, _LastCalledMethod.args[_LastCalledMethod.outList[index]]);
            }

            //Desc:
            //  if not return void,we need add 1,
            //  or we will lost the function's return value
            //  when call dotnet function like "int foo(arg1,out arg2,out arg3)" in lua code
            if (!_LastCalledMethod.IsReturnVoid && nReturnValues > 0)
            {
                nReturnValues++;
            }
            ClearCachedArgs();
            return nReturnValues < 1 ? 1 : nReturnValues;
        }
    }
}