using System;
using System.Collections;

namespace LuaInterface
{

    public class LuaTable : LuaBase
    {
        public LuaTable(int reference, LuaState interpreter)
        {
            _Reference = reference;
            _Interpreter = interpreter;
            translator = interpreter.translator;
        }

        public object this[string field]
        {
            get
            {
                return _Interpreter.getObject(_Reference, field);
            }
            set
            {
                _Interpreter.setObject(_Reference, field, value);
            }
        }

        public System.Collections.IDictionaryEnumerator GetEnumerator()
        {
            return _Interpreter.GetTableDict(this).GetEnumerator();
        }
        public int Count
        {
            get
            {
                return _Interpreter.GetTableDict(this).Count;
            }
        }

        public ICollection Keys
        {
            get { return _Interpreter.GetTableDict(this).Keys; }
        }

        public ICollection Values
        {
            get { return _Interpreter.GetTableDict(this).Values; }
        }

        public T[] ToArray<T>()
        {
            IntPtr L = _Interpreter.L;
            push(L);
            return LuaScriptMgr.GetArrayObject<T>(L, -1);
        }

        internal object rawget(string field)
        {
            return _Interpreter.rawGetObject(_Reference, field);
        }

        internal void push(IntPtr luaState)
        {
            LuaAPI.ulua_rawgeti(luaState, LuaAPI.LUA_REGISTRYINDEX, _Reference);
        }

        public override string ToString()
        {
            return "table";
        }
    }
}
