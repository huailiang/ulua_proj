using System;

namespace LuaInterface
{
    public class LuaFunction : LuaBase
    {
        internal LuaCSFunction function;
        IntPtr L;

        public LuaFunction(int reference, LuaState interpreter)
        {
            _Reference = reference;
            this.function = null;
            _Interpreter = interpreter;
            L = _Interpreter.L;
            translator = _Interpreter.translator;
        }

        public LuaFunction(LuaCSFunction function, LuaState interpreter)
        {
            _Reference = 0;
            this.function = function;
            _Interpreter = interpreter;
            L = _Interpreter.L;
            translator = _Interpreter.translator;
        }

        public LuaFunction(int reference, IntPtr l)
        {
            _Reference = reference;
            this.function = null;
            L = l;
            translator = ObjectTranslator.FromState(L);
            _Interpreter = translator.interpreter;
        }

        /*
         * Calls the function casting return values to the types
         * in returnTypes
         */
        internal object[] call(object[] args, Type[] returnTypes)
        {
            int nArgs = 0;
            LuaScriptMgr.PushTraceBack(L);
            int oldTop = LuaAPI.lua_gettop(L);

            if (!LuaAPI.lua_checkstack(L, args.Length + 6))
            {
                LuaAPI.lua_pop(L, 1);
                throw new LuaException("Lua stack overflow");
            }

            push(L);

            if (args != null)
            {
                nArgs = args.Length;

                for (int i = 0; i < args.Length; i++)
                {
                    PushArgs(L, args[i]);
                }
            }

            int error = LuaAPI.lua_pcall(L, nArgs, -1, -nArgs - 2);

            if (error != 0)
            {
                string err = LuaAPI.lua_tostring(L, -1);
                LuaAPI.lua_settop(L, oldTop - 1);
                if (err == null) err = "Unknown Lua Error";
                throw new LuaScriptException(err, "");
            }

            object[] ret = returnTypes != null ? translator.popValues(L, oldTop, returnTypes) : translator.popValues(L, oldTop);
            LuaAPI.lua_settop(L, oldTop - 1);
            return ret;
        }

        public object[] Call(params object[] args)
        {
            return call(args, null);
        }

        public object[] Call()
        {
            int oldTop = BeginPCall();

            if (PCall(oldTop, 0))
            {
                object[] objs = PopValues(oldTop);
                EndPCall(oldTop);
                return objs;
            }

            LuaAPI.lua_settop(L, oldTop - 1);
            return null;
        }

        public object[] Call(double arg1)
        {
            int oldTop = BeginPCall();
            LuaScriptMgr.Push(L, arg1);

            if (PCall(oldTop, 1))
            {
                object[] objs = PopValues(oldTop);
                EndPCall(oldTop);
                return objs;
            }

            LuaAPI.lua_settop(L, oldTop - 1);
            return null;
        }

        int beginPos = -1;

        public int BeginPCall()
        {
            LuaScriptMgr.PushTraceBack(L);
            beginPos = LuaAPI.lua_gettop(L);
            push(L);
            return beginPos;
        }

        public bool PCall(int oldTop, int args)
        {
            if (LuaAPI.lua_pcall(L, args, -1, -args - 2) != 0)
            {
                string err = LuaAPI.lua_tostring(L, -1);
                LuaAPI.lua_settop(L, oldTop - 1);
                if (err == null) err = "Unknown Lua Error";
                throw new LuaScriptException(err, "");
            }
            return true;
        }

        public object[] PopValues(int oldTop)
        {
            return translator.popValues(L, oldTop);
        }

        public void EndPCall(int oldTop)
        {
            LuaAPI.lua_settop(L, oldTop - 1);
        }

        public IntPtr GetLuaState()
        {
            return L;
        }

        internal void push(IntPtr luaState)
        {
            if (_Reference != 0)
            {
                LuaAPI.ulua_rawgeti(luaState, LuaAPI.LUA_REGISTRYINDEX, _Reference);
            }
            else
            {
                _Interpreter.pushCSFunction(function);
            }
        }

        internal void push()
        {
            if (_Reference != 0)
            {
                LuaAPI.ulua_rawgeti(L, LuaAPI.LUA_REGISTRYINDEX, _Reference);
            }
            else
            {
                _Interpreter.pushCSFunction(function);
            }
        }

        public override string ToString()
        {
            return "function";
        }

        public override bool Equals(object o)
        {
            if (o is LuaFunction)
            {
                LuaFunction l = (LuaFunction)o;
                if (this._Reference != 0 && l._Reference != 0)
                    return _Interpreter.compareRef(l._Reference, this._Reference);
                else
                    return this.function == l.function;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            if (_Reference != 0)
                return _Reference;
            else
                return function.GetHashCode();
        }

        public int GetReference()
        {
            return _Reference;
        }
    }
}