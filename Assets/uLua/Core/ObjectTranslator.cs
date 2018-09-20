namespace LuaInterface
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class ObjectExtends
    {
        public static object RefObject(this object obj)
        {
            return new WeakReference(obj).Target;
        }
    }
    
    public class ObjectTranslator
    {
        private class CompareObject : IEqualityComparer<object>
        {
            public new bool Equals(object x, object y)
            {
                return x == y;
            }

            public int GetHashCode(object obj)
            {
                if (obj != null) return obj.GetHashCode();
                return 0;
            }
        }

        internal CheckType typeChecker;
        internal LuaState interpreter;

        public readonly Dictionary<int, object> objects = new Dictionary<int, object>();
        public readonly Dictionary<object, int> objectsBackMap = new Dictionary<object, int>(new CompareObject());
        static Dictionary<Type, int> typeMetaMap = new Dictionary<Type, int>();
        
        public MetaFunctions metaFunctions;
        public List<Assembly> assemblies;
        private LuaCSFunction registerTableFunction, unregisterTableFunction, importTypeFunction, loadAssemblyFunction, ctypeFunction, enumFromIntFunction;

        internal EventHandlerContainer pendingEvents = new EventHandlerContainer();

        static List<ObjectTranslator> list = new List<ObjectTranslator>();
        static int indexTranslator = 0;

        public int weakTableRef { get; private set; }

        public static ObjectTranslator FromState(IntPtr luaState)
        {
            LuaAPI.lua_getglobal(luaState, "_translator");
            int pos = (int)LuaAPI.lua_tonumber(luaState, -1);
            LuaAPI.lua_pop(luaState, 1);
            return list[pos];
        }

        public void PushTranslator(IntPtr L)
        {
            list.Add(this);
            LuaAPI.lua_pushnumber(L, indexTranslator);
            LuaAPI.lua_setglobal(L, "_translator");
            ++indexTranslator;
        }

        public ObjectTranslator(LuaState interpreter, IntPtr luaState)
        {
            this.interpreter = interpreter;
            weakTableRef = -1;
            typeChecker = new CheckType(this);
            metaFunctions = new MetaFunctions(this);
            assemblies = new List<Assembly>();
            assemblies.Add(Assembly.GetExecutingAssembly());

            importTypeFunction = new LuaCSFunction(importType);
            loadAssemblyFunction = new LuaCSFunction(loadAssembly);
            registerTableFunction = new LuaCSFunction(registerTable);
            unregisterTableFunction = new LuaCSFunction(unregisterTable);
            ctypeFunction = new LuaCSFunction(ctype);
            enumFromIntFunction = new LuaCSFunction(enumFromInt);

            createLuaObjectList(luaState);
            createIndexingMetaFunction(luaState);
            createBaseClassMetatable(luaState);
            createClassMetatable(luaState);
            createFunctionMetatable(luaState);
            setGlobalFunctions(luaState);
        }

        public void Destroy()
        {
            IntPtr L = interpreter.L;

            foreach (KeyValuePair<Type, int> kv in typeMetaMap)
            {
                int reference = kv.Value;
                LuaAPI.lua_unref(L, reference);
            }

            LuaAPI.lua_unref(L, weakTableRef);
            typeMetaMap.Clear();
        }
        
        private void createLuaObjectList(IntPtr luaState)
        {
            LuaAPI.lua_pushstring(luaState, "luaNet_objects");
            LuaAPI.lua_newtable(luaState);
            LuaAPI.lua_pushvalue(luaState, -1);
            weakTableRef = LuaAPI.luaL_ref(luaState, LuaAPI.LUA_REGISTRYINDEX);
            LuaAPI.lua_pushvalue(luaState, -1);
            LuaAPI.lua_setmetatable(luaState, -2);
            LuaAPI.lua_pushstring(luaState, "__mode");
            LuaAPI.lua_pushstring(luaState, "v");
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_settable(luaState, LuaAPI.LUA_REGISTRYINDEX);
        }
    
        private void createIndexingMetaFunction(IntPtr luaState)
        {
            LuaAPI.lua_pushstring(luaState, "luaNet_indexfunction");
            LuaAPI.luaL_dostring(luaState, MetaFunctions.luaIndexFunction);
            //LuaAPI.lua_pushstdcallcfunction(luaState,indexFunction);
            LuaAPI.lua_rawset(luaState, LuaAPI.LUA_REGISTRYINDEX);
        }

        private void createBaseClassMetatable(IntPtr luaState)
        {
            LuaAPI.luaL_newmetatable(luaState, "luaNet_searchbase");
            LuaAPI.lua_pushstring(luaState, "__gc");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.gcFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__tostring");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.toStringFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__index");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.baseIndexFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__newindex");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.newindexFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_settop(luaState, -2);
        }

        private void createClassMetatable(IntPtr luaState)
        {
            LuaAPI.luaL_newmetatable(luaState, "luaNet_class");
            LuaAPI.lua_pushstring(luaState, "__gc");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.gcFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__tostring");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.toStringFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__index");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.classIndexFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__newindex");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.classNewindexFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__call");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.callConstructorFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_settop(luaState, -2);
        }

        private void setGlobalFunctions(IntPtr luaState)
        {
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.indexFunction);
            LuaAPI.lua_setglobal(luaState, "get_object_member");
            LuaAPI.lua_getglobal(luaState, "luanet");
            LuaAPI.lua_pushstring(luaState, "import_type");
            LuaAPI.lua_pushstdcallcfunction(luaState, importTypeFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "load_assembly");
            LuaAPI.lua_pushstdcallcfunction(luaState, loadAssemblyFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "make_object");
            LuaAPI.lua_pushstdcallcfunction(luaState, registerTableFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "free_object");
            LuaAPI.lua_pushstdcallcfunction(luaState, unregisterTableFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "ctype");
            LuaAPI.lua_pushstdcallcfunction(luaState, ctypeFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "enum");
            LuaAPI.lua_pushstdcallcfunction(luaState, enumFromIntFunction);
            LuaAPI.lua_settable(luaState, -3);
        }
        
        private void createFunctionMetatable(IntPtr luaState)
        {
            LuaAPI.luaL_newmetatable(luaState, "luaNet_function");
            LuaAPI.lua_pushstring(luaState, "__gc");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.gcFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_pushstring(luaState, "__call");
            LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.execDelegateFunction);
            LuaAPI.lua_settable(luaState, -3);
            LuaAPI.lua_settop(luaState, -2);
        }

        internal void throwError(IntPtr luaState, string message)
        {
            LuaAPI.luaL_error(luaState, message);
        }
  
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int loadAssembly(IntPtr luaState)
        {
            ObjectTranslator translator = ObjectTranslator.FromState(luaState);
            try
            {
                string assemblyName = LuaAPI.lua_tostring(luaState, 1);

                Assembly assembly = null;
                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch (BadImageFormatException)
                {
                    // The assemblyName was invalid.  It is most likely a path.
                }

                if (assembly == null)
                {
                    assembly = Assembly.Load(AssemblyName.GetAssemblyName(assemblyName));
                }
                if (assembly != null && !translator.assemblies.Contains(assembly))
                {
                    translator.assemblies.Add(assembly);
                }
            }
            catch (Exception e)
            {
                translator.throwError(luaState, e.Message);
            }

            return 0;
        }

        internal Type FindType(string className)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type klass = assembly.GetType(className);
                if (klass != null)
                {
                    return klass;
                }
            }
            return null;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int importType(IntPtr luaState)
        {
            ObjectTranslator translator = ObjectTranslator.FromState(luaState);
            string className = LuaAPI.lua_tostring(luaState, 1);
            Type klass = translator.FindType(className);
            if (klass != null)
                translator.pushType(luaState, klass);
            else
                LuaAPI.lua_pushnil(luaState);
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int registerTable(IntPtr luaState)
        {
            ObjectTranslator translator = ObjectTranslator.FromState(luaState);
            if (LuaAPI.lua_type(luaState, 1) == LuaTypes.LUA_TTABLE)
            {
                LuaTable luaTable = translator.getTable(luaState, 1);
                string superclassName = LuaAPI.lua_tostring(luaState, 2);
                if (superclassName != null)
                {
                    Type klass = translator.FindType(superclassName);
                    if (klass != null)
                    {
                        // Creates and pushes the object in the stack, setting
                        // it as the  metatable of the first argument
                        object obj = CodeGeneration.Instance.GetClassInstance(klass, luaTable);
                        translator.pushObject(luaState, obj, "luaNet_metatable");
                        LuaAPI.lua_newtable(luaState);
                        LuaAPI.lua_pushstring(luaState, "__index");
                        LuaAPI.lua_pushvalue(luaState, -3);
                        LuaAPI.lua_settable(luaState, -3);
                        LuaAPI.lua_pushstring(luaState, "__newindex");
                        LuaAPI.lua_pushvalue(luaState, -3);
                        LuaAPI.lua_settable(luaState, -3);
                        LuaAPI.lua_setmetatable(luaState, 1);
                        // Pushes the object again, this time as the base field
                        // of the table and with the luaNet_searchbase metatable
                        LuaAPI.lua_pushstring(luaState, "base");
                        int index = translator.addObject(obj);
                        translator.pushNewObject(luaState, obj, index, "luaNet_searchbase");
                        LuaAPI.lua_rawset(luaState, 1);
                    }
                    else
                        translator.throwError(luaState, "register_table: can not find superclass '" + superclassName + "'");
                }
                else
                    translator.throwError(luaState, "register_table: superclass name can not be null");
            }
            else translator.throwError(luaState, "register_table: first arg is not a table");
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int unregisterTable(IntPtr luaState)
        {
            ObjectTranslator translator = ObjectTranslator.FromState(luaState);
            try
            {
                if (LuaAPI.lua_getmetatable(luaState, 1) != 0)
                {
                    LuaAPI.lua_pushstring(luaState, "__index");
                    LuaAPI.lua_gettable(luaState, -2);
                    object obj = translator.getRawNetObject(luaState, -1);
                    if (obj == null) translator.throwError(luaState, "unregister_table: arg is not valid table");
                    FieldInfo luaTableField = obj.GetType().GetField("__luaInterface_luaTable");
                    if (luaTableField == null) translator.throwError(luaState, "unregister_table: arg is not valid table");
                    luaTableField.SetValue(obj, null);
                    LuaAPI.lua_pushnil(luaState);
                    LuaAPI.lua_setmetatable(luaState, 1);
                    LuaAPI.lua_pushstring(luaState, "base");
                    LuaAPI.lua_pushnil(luaState);
                    LuaAPI.lua_settable(luaState, 1);
                }
                else translator.throwError(luaState, "unregister_table: arg is not valid table");
            }
            catch (Exception e)
            {
                translator.throwError(luaState, e.Message);
            }
            return 0;
        }

        private Type typeOf(IntPtr luaState, int idx)
        {
            int udata = LuaAPI.luanet_checkudata(luaState, 1, "luaNet_class");
            if (udata == -1)
            {
                return null;
            }
            else
            {
                ProxyType pt = (ProxyType)objects[udata];
                return pt.UnderlyingSystemType;
            }
        }

        public int pushError(IntPtr luaState, string msg)
        {
            LuaAPI.lua_pushnil(luaState);
            LuaAPI.lua_pushstring(luaState, msg);
            return 2;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int ctype(IntPtr luaState)
        {
            ObjectTranslator translator = ObjectTranslator.FromState(luaState);
            Type t = translator.typeOf(luaState, 1);
            if (t == null)
            {
                return translator.pushError(luaState, "not a CLR class");
            }
            translator.pushObject(luaState, t, "luaNet_metatable");
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int enumFromInt(IntPtr luaState)
        {
            ObjectTranslator translator = ObjectTranslator.FromState(luaState);
            Type t = translator.typeOf(luaState, 1);
            if (t == null || !t.IsEnum)
            {
                return translator.pushError(luaState, "not an enum");
            }
            object res = null;
            LuaTypes lt = LuaAPI.lua_type(luaState, 2);
            if (lt == LuaTypes.LUA_TNUMBER)
            {
                int ival = (int)LuaAPI.lua_tonumber(luaState, 2);
                res = Enum.ToObject(t, ival);
            }
            else
                if (lt == LuaTypes.LUA_TSTRING)
            {
                string sflags = LuaAPI.lua_tostring(luaState, 2);
                string err = null;
                try
                {
                    res = Enum.Parse(t, sflags);
                }
                catch (ArgumentException e)
                {
                    err = e.Message;
                }
                if (err != null)
                {
                    return translator.pushError(luaState, err);
                }
            }
            else
            {
                return translator.pushError(luaState, "second argument must be a integer or a string");
            }
            translator.pushObject(luaState, res, "luaNet_metatable");
            return 1;
        }

        internal void pushType(IntPtr luaState, Type t)
        {
            pushObject(luaState, new ProxyType(t), "luaNet_class");
        }
  
        internal void pushFunction(IntPtr luaState, LuaCSFunction func)
        {
            pushObject(luaState, func, "luaNet_function");
        }

        static Dictionary<Type, bool> valueTypeMap = new Dictionary<Type, bool>();

        bool IsValueType(Type t)
        {
            bool isValue = false;

            if (!valueTypeMap.TryGetValue(t, out isValue))
            {
                isValue = t.IsValueType;
                valueTypeMap.Add(t, isValue);
            }

            return isValue;
        }

        public void pushObject(IntPtr luaState, object o, string metatable)
        {
            if (o == null)
            {
                LuaAPI.lua_pushnil(luaState);
                return;
            }

            int index = -1;
            // Object already in the list of Lua objects? Push the stored reference.
            bool beValueType = o.GetType().IsValueType;
            if (!beValueType && objectsBackMap.TryGetValue(o, out index))
            {
                if (LuaAPI.ulua_pushudata(luaState, weakTableRef, index))
                {
                    return;
                }

                // Note: starting with lua5.1 the garbage collector may remove weak reference items (such as our luaNet_objects values) when the initial GC sweep
                // occurs, but the actual call of the __gc finalizer for that object may not happen until a little while later.  During that window we might call
                // this routine and find the element missing from luaNet_objects, but collectObject() has not yet been called.  In that case, we go ahead and call collect
                // object here
                // did we find a non nil object in our table? if not, we need to call collect object             
                // Remove from both our tables and fall out to get a new ID
                collectObject(o, index);
            }
            index = addObject(o, beValueType);
            pushNewObject(luaState, o, index, metatable);
        }

        static void PushMetaTable(IntPtr L, Type t)
        {
            int reference = -1;

            if (!typeMetaMap.TryGetValue(t, out reference))
            {
                LuaAPI.luaL_getmetatable(L, t.AssemblyQualifiedName);

                if (!LuaAPI.lua_isnil(L, -1))
                {
                    LuaAPI.lua_pushvalue(L, -1);
                    reference = LuaAPI.luaL_ref(L, LuaAPI.LUA_REGISTRYINDEX);
                    typeMetaMap.Add(t, reference);
                }
            }
            else
            {
                LuaAPI.ulua_rawgeti(L, LuaAPI.LUA_REGISTRYINDEX, reference);
            }
        }

        /*
         * Pushes a new object into the Lua stack with the provided metatable
         */
        private void pushNewObject(IntPtr luaState, object o, int index, string metatable)
        {
            LuaAPI.ulua_rawgeti(luaState, LuaAPI.LUA_REGISTRYINDEX, weakTableRef);
            LuaAPI.luanet_newudata(luaState, index);

            if (metatable == "luaNet_metatable")
            {
                Type t = o.GetType();
                PushMetaTable(luaState, o.GetType());

                if (LuaAPI.lua_isnil(luaState, -1))
                {
                    string meta = t.AssemblyQualifiedName;
                    Debugger.LogWarning("Create not wrap ulua type:" + meta);
                    LuaAPI.lua_settop(luaState, -2);
                    LuaAPI.luaL_newmetatable(luaState, meta);
                    LuaAPI.lua_pushstring(luaState, "cache");
                    LuaAPI.lua_newtable(luaState);
                    LuaAPI.lua_rawset(luaState, -3);
                    LuaAPI.lua_pushlightuserdata(luaState, LuaAPI.luanet_gettag());
                    LuaAPI.lua_pushnumber(luaState, 1);
                    LuaAPI.lua_rawset(luaState, -3);
                    LuaAPI.lua_pushstring(luaState, "__index");
                    LuaAPI.lua_pushstring(luaState, "luaNet_indexfunction");
                    LuaAPI.lua_rawget(luaState, LuaAPI.LUA_REGISTRYINDEX);
                    LuaAPI.lua_rawset(luaState, -3);
                    LuaAPI.lua_pushstring(luaState, "__gc");
                    LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.gcFunction);
                    LuaAPI.lua_rawset(luaState, -3);
                    LuaAPI.lua_pushstring(luaState, "__tostring");
                    LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.toStringFunction);
                    LuaAPI.lua_rawset(luaState, -3);
                    LuaAPI.lua_pushstring(luaState, "__newindex");
                    LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.newindexFunction);
                    LuaAPI.lua_rawset(luaState, -3);
                }
            }
            else
            {
                LuaAPI.luaL_getmetatable(luaState, metatable);
            }

            LuaAPI.lua_setmetatable(luaState, -2);
            LuaAPI.lua_pushvalue(luaState, -1);
            LuaAPI.ulua_rawseti(luaState, -3, index);
            LuaAPI.lua_remove(luaState, -2);
        }

        public void PushNewValueObject(IntPtr luaState, object o, int index)
        {
            LuaAPI.luanet_newudata(luaState, index);
            Type t = o.GetType();
            PushMetaTable(luaState, o.GetType());
            if (LuaAPI.lua_isnil(luaState, -1))
            {
                string meta = t.AssemblyQualifiedName;
                Debugger.LogWarning("Create not wrap ulua type:" + meta);
                LuaAPI.lua_settop(luaState, -2);
                LuaAPI.luaL_newmetatable(luaState, meta);
                LuaAPI.lua_pushstring(luaState, "cache");
                LuaAPI.lua_newtable(luaState);
                LuaAPI.lua_rawset(luaState, -3);
                LuaAPI.lua_pushlightuserdata(luaState, LuaAPI.luanet_gettag());
                LuaAPI.lua_pushnumber(luaState, 1);
                LuaAPI.lua_rawset(luaState, -3);
                LuaAPI.lua_pushstring(luaState, "__index");
                LuaAPI.lua_pushstring(luaState, "luaNet_indexfunction");
                LuaAPI.lua_rawget(luaState, LuaAPI.LUA_REGISTRYINDEX);
                LuaAPI.lua_rawset(luaState, -3);
                LuaAPI.lua_pushstring(luaState, "__gc");
                LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.gcFunction);
                LuaAPI.lua_rawset(luaState, -3);
                LuaAPI.lua_pushstring(luaState, "__tostring");
                LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.toStringFunction);
                LuaAPI.lua_rawset(luaState, -3);
                LuaAPI.lua_pushstring(luaState, "__newindex");
                LuaAPI.lua_pushstdcallcfunction(luaState, metaFunctions.newindexFunction);
                LuaAPI.lua_rawset(luaState, -3);
            }
            LuaAPI.lua_setmetatable(luaState, -2);
        }

        internal object getAsType(IntPtr luaState, int stackPos, Type paramType)
        {
            ExtractValue extractor = typeChecker.checkType(luaState, stackPos, paramType);
            if (extractor != null) return extractor(luaState, stackPos);
            return null;
        }


        /// <summary>
        /// Given the Lua int ID for an object remove it from our maps
        /// </summary>    

        internal void collectObject(int udata)
        {
            object o;
            bool found = objects.TryGetValue(udata, out o);

            // The other variant of collectObject might have gotten here first, in that case we will silently ignore the missing entry
            if (found)
            {
                objects.Remove(udata);

                if (o != null && !o.GetType().IsValueType)
                {
                    objectsBackMap.Remove(o);
                }
            }
        }


        /// <summary>
        /// Given an object reference, remove it from our maps
        /// </summary>
        void collectObject(object o, int udata)
        {
            objectsBackMap.Remove(o);
            objects.Remove(udata);
        }


        /// <summary>
        /// We want to ensure that objects always have a unique ID
        /// </summary>
        int nextObj = 0;

        public int addObject(object obj)
        {
            int index = nextObj++;
            objects[index] = obj;
            if (!obj.GetType().IsValueType)
            {
                objectsBackMap[obj] = index;
            }
            return index;
        }

        public int addObject(object obj, bool isValueType)
        {
            int index = nextObj++;
            objects[index] = obj;
            if (!isValueType)
            {
                objectsBackMap[obj] = index;
            }

            return index;
        }

        /*
         * Gets an object from the Lua stack according to its Lua type.
         */
        public object getObject(IntPtr luaState, int index)
        {
            return LuaScriptMgr.GetVarObject(luaState, index);
        }
        /*
         * Gets the table in the index positon of the Lua stack.
         */
        internal LuaTable getTable(IntPtr luaState, int index)
        {
            LuaAPI.lua_pushvalue(luaState, index);
            return new LuaTable(LuaAPI.luaL_ref(luaState, LuaAPI.LUA_REGISTRYINDEX), interpreter);
        }
        /*
         * Gets the function in the index positon of the Lua stack.
         */
        internal LuaFunction getFunction(IntPtr luaState, int index)
        {
            LuaAPI.lua_pushvalue(luaState, index);
            return new LuaFunction(LuaAPI.luaL_ref(luaState, LuaAPI.LUA_REGISTRYINDEX), interpreter);
        }
        /*
         * Gets the CLR object in the index positon of the Lua stack. Returns
         * delegates as Lua functions.
         */
        internal object getNetObject(IntPtr luaState, int index)
        {
            int idx = LuaAPI.luanet_tonetobject(luaState, index);

            if (idx != -1)
                return objects[idx];
            else
                return null;
        }
        /*
         * Gets the CLR object in the index positon of the Lua stack. Returns
         * delegates as is.
         */
        internal object getRawNetObject(IntPtr luaState, int index)
        {
            int udata = LuaAPI.luanet_rawnetobj(luaState, index);
            object obj = null;
            objects.TryGetValue(udata, out obj);
            return obj;
        }

        public void SetValueObject(IntPtr luaState, int stackPos, object obj)
        {
            int udata = LuaAPI.luanet_rawnetobj(luaState, stackPos);

            if (udata != -1)
            {
                objects[udata] = obj;
            }
        }

        /*
         * Pushes the entire array into the Lua stack and returns the number
         * of elements pushed.
         */
        internal int returnValues(IntPtr luaState, object[] returnValues)
        {
            if (LuaAPI.lua_checkstack(luaState, returnValues.Length + 5))
            {
                for (int i = 0; i < returnValues.Length; i++)
                {
                    push(luaState, returnValues[i]);
                }
                return returnValues.Length;
            }
            else
                return 0;
        }
        /*
         * Gets the values from the provided index to
         * the top of the stack and returns them in an array.
         */
        internal object[] popValues(IntPtr luaState, int oldTop)
        {
            int newTop = LuaAPI.lua_gettop(luaState);

            if (oldTop == newTop)
            {
                return null;
            }
            else
            {
                List<object> returnValues = new List<object>();

                for (int i = oldTop + 1; i <= newTop; i++)
                {
                    returnValues.Add(getObject(luaState, i));
                }

                LuaAPI.lua_settop(luaState, oldTop);
                return returnValues.ToArray();
            }
        }
        /*
         * Gets the values from the provided index to
         * the top of the stack and returns them in an array, casting
         * them to the provided types.
         */
        internal object[] popValues(IntPtr luaState, int oldTop, Type[] popTypes)
        {
            int newTop = LuaAPI.lua_gettop(luaState);
            if (oldTop == newTop)
            {
                return null;
            }
            else
            {
                int iTypes;
                List<object> returnValues = new List<object>();
                if (popTypes[0] == typeof(void))
                    iTypes = 1;
                else
                    iTypes = 0;
                for (int i = oldTop + 1; i <= newTop; i++)
                {
                    returnValues.Add(getAsType(luaState, i, popTypes[iTypes]));
                    iTypes++;
                }
                LuaAPI.lua_settop(luaState, oldTop);
                return returnValues.ToArray();
            }
        }

        static bool IsILua(object o)
        {
            if (o is ILuaGeneratedType)
            {
                Type typ = o.GetType();
                return (typ.GetInterface("ILuaGeneratedType") != null);
            }
            return false;
        }

        internal void push(IntPtr luaState, object o)
        {
            LuaScriptMgr.PushVarObject(luaState, o);
        }

        internal void PushValueResult(IntPtr lua, object o)
        {
            int index = addObject(o, true);
            PushNewValueObject(lua, o, index);
        }

        internal bool matchParameters(IntPtr luaState, MethodBase method, ref MethodCache methodCache)
        {
            return metaFunctions.matchParameters(luaState, method, ref methodCache);
        }

        internal Array tableToArray(object luaParamValue, Type paramArrayType)
        {
            return metaFunctions.TableToArray(luaParamValue, paramArrayType);
        }
    }
}