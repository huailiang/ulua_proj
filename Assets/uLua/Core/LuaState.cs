using System;
using System.Collections.Specialized;
using System.Text;

namespace LuaInterface
{
    public class LuaState : IDisposable
    {
        public IntPtr L;
        internal ObjectTranslator translator;
        internal LuaCSFunction tracebackFunction;
        internal LuaCSFunction panicCallback;

        // Overrides
        internal LuaCSFunction printFunction;
        internal LuaCSFunction loadfileFunction;
        internal LuaCSFunction loaderFunction;
        internal LuaCSFunction builtinFunction;
        internal LuaCSFunction dofileFunction;
        internal LuaCSFunction import_wrapFunction;

        public ObjectTranslator Translator { get { return translator; } }

        public LuaState()
        {
            L = LuaAPI.luaL_newstate();
            LuaAPI.luaL_openlibs(L);

            LuaAPI.lua_newtable(L);
            LuaAPI.lua_pushstring(L, "getmetatable");
            LuaAPI.lua_getglobal(L, "getmetatable");
            LuaAPI.lua_settable(L, -3);
            LuaAPI.lua_pushstring(L, "rawget");
            LuaAPI.lua_getglobal(L, "rawget");
            LuaAPI.lua_settable(L, -3);
            LuaAPI.lua_pushstring(L, "rawset");
            LuaAPI.lua_getglobal(L, "rawset");
            LuaAPI.lua_settable(L, -3);
            LuaAPI.lua_setglobal(L, "luanet");

            LuaAPI.lua_getglobal(L, "luanet");
            translator = new ObjectTranslator(this, L);
            translator.PushTranslator(L);

            panicCallback = new LuaCSFunction(LuaStatic.panic);
            LuaAPI.lua_atpanic(L, panicCallback);

            printFunction = new LuaCSFunction(LuaStatic.print);
            LuaAPI.lua_pushstdcallcfunction(L, printFunction);
            LuaAPI.lua_setglobal(L, "print");

            loadfileFunction = new LuaCSFunction(LuaStatic.loadfile);
            LuaAPI.lua_pushstdcallcfunction(L, loadfileFunction);
            LuaAPI.lua_setglobal(L, "loadfile");

            dofileFunction = new LuaCSFunction(LuaStatic.dofile);
            LuaAPI.lua_pushstdcallcfunction(L, dofileFunction);
            LuaAPI.lua_setglobal(L, "dofile");

            import_wrapFunction = new LuaCSFunction(LuaStatic.importWrap);
            LuaAPI.lua_pushstdcallcfunction(L, import_wrapFunction);
            LuaAPI.lua_setglobal(L, "import");

            loaderFunction = new LuaCSFunction(LuaStatic.loader);
            AddLoader(loaderFunction, 2);
            builtinFunction = new LuaCSFunction(LuaStatic.LoadBuiltinLib);
            AddLoader(builtinFunction, 3);

            LuaAPI.lua_settop(L, 0); //clear stack
            tracebackFunction = new LuaCSFunction(LuaStatic.traceback);
        }


        public void AddLoader(LuaCSFunction loader, int index)
        {
            LuaAPI.lua_getglobal(L, "package");
            LuaAPI.lua_getfield(L, -1, "searchers");
            LuaAPI.lua_remove(L, -2); //remv table package
            int len = LuaAPI.lua_rawlen(L, -1);
            for (int i = len + 1; i > index; i--)
            {
                LuaAPI.ulua_rawgeti(L, -1, i - 1);
                LuaAPI.ulua_rawseti(L, -2, i);
            }
            LuaAPI.lua_pushstdcallcfunction(L, loader);
            LuaAPI.ulua_rawseti(L, -2, index);
        }

        public void Close()
        {
            if (L != IntPtr.Zero)
            {
                translator.Destroy();
                LuaAPI.lua_close(L);
            }
        }

        internal void ThrowExceptionFromError(int oldTop)
        {
            string err = LuaAPI.lua_tostring(L, -1);
            LuaAPI.lua_settop(L, oldTop);
            if (err == null) err = "Unknown Lua Error";
            throw new LuaScriptException(err, "");
        }

        internal int SetPendingException(Exception e)
        {
            if (e != null)
            {
                translator.throwError(L, e.ToString());
                LuaAPI.lua_pushnil(L);
                return 1;
            }
            return 0;
        }

        public LuaFunction LoadString(string chunk, string name)
        {
            int oldTop = LuaAPI.lua_gettop(L);
            byte[] bt = Encoding.UTF8.GetBytes(chunk);

            if (LuaAPI.luaL_loadbuffer(L, bt, bt.Length, name) != 0)
                ThrowExceptionFromError(oldTop);

            LuaFunction result = translator.getFunction(L, -1);
            translator.popValues(L, oldTop);
            return result;
        }

        public object[] DoString(string chunk)
        {
            int oldTop = LuaAPI.lua_gettop(L);
            byte[] bt = Encoding.UTF8.GetBytes(chunk);
            if (LuaAPI.luaL_loadbuffer(L, bt, bt.Length, "chunk") == 0)
            {
                if (LuaAPI.lua_pcall(L, 0, -1, 0) == 0)
                    return translator.popValues(L, oldTop);
                else
                    ThrowExceptionFromError(oldTop);
            }
            else
                ThrowExceptionFromError(oldTop);
            return null;
        }

        public object[] DoFile(string fileName)
        {
            LuaAPI.lua_pushstdcallcfunction(L, tracebackFunction);
            int oldTop = LuaAPI.lua_gettop(L);
            byte[] bytes = LuaStatic.Load(fileName);
            if (bytes == null)
            {
                LuaAPI.lua_pop(L, 1);
                return null;
            }
            if (LuaAPI.luaL_loadbuffer(L, bytes, bytes.Length, fileName) == 0)
            {
                if (LuaAPI.lua_pcall(L, 0, -1, 0) == 0)
                {
                    object[] results = translator.popValues(L, oldTop);
                    LuaAPI.lua_pop(L, 1);
                    return results;
                }
                else
                {
                    ThrowExceptionFromError(oldTop);
                    LuaAPI.lua_pop(L, 1);
                }
            }
            else
            {
                ThrowExceptionFromError(oldTop);
                LuaAPI.lua_pop(L, 1);
            }
            return null;
        }

        public object this[string fullPath]
        {
            get
            {
                object returnValue = null;
                int oldTop = LuaAPI.lua_gettop(L);
                string[] path = fullPath.Split(new char[] { '.' });
                LuaAPI.lua_getglobal(L, path[0]);
                returnValue = translator.getObject(L, -1);
                if (path.Length > 1)
                {
                    string[] remainingPath = new string[path.Length - 1];
                    Array.Copy(path, 1, remainingPath, 0, path.Length - 1);
                    returnValue = getObject(remainingPath);
                }
                LuaAPI.lua_settop(L, oldTop);
                return returnValue;
            }
            set
            {
                int oldTop = LuaAPI.lua_gettop(L);
                string[] path = fullPath.Split(new char[] { '.' });
                if (path.Length == 1)
                {
                    translator.push(L, value);
                    LuaAPI.lua_setglobal(L, fullPath);
                }
                else
                {
                    LuaAPI.lua_getglobal(L, path[0]);
                    LuaTypes type = LuaAPI.lua_type(L, -1);
                    if (type == LuaTypes.LUA_TNIL)
                    {
                        Debugger.LogError("Table {0} not exists", path[0]);
                        LuaAPI.lua_settop(L, oldTop);
                        return;
                    }

                    string[] remainingPath = new string[path.Length - 1];
                    Array.Copy(path, 1, remainingPath, 0, path.Length - 1);
                    setObject(remainingPath, value);
                }
                LuaAPI.lua_settop(L, oldTop);
            }
        }


        internal object getObject(string[] remainingPath)
        {
            object returnValue = null;
            for (int i = 0; i < remainingPath.Length; i++)
            {
                LuaAPI.lua_pushstring(L, remainingPath[i]);
                LuaAPI.lua_gettable(L, -2);
                returnValue = translator.getObject(L, -1);
                if (returnValue == null) break;
            }
            return returnValue;
        }


        public LuaFunction GetFunction(string fullPath)
        {
            object obj = this[fullPath];
            return (obj is LuaCSFunction ? new LuaFunction((LuaCSFunction)obj, this) : (LuaFunction)obj);
        }

        internal void setObject(string[] remainingPath, object val)
        {
            for (int i = 0; i < remainingPath.Length - 1; i++)
            {
                LuaAPI.lua_pushstring(L, remainingPath[i]);
                LuaAPI.lua_gettable(L, -2);
            }
            LuaAPI.lua_pushstring(L, remainingPath[remainingPath.Length - 1]);

            //可以释放先
            //if (val == null)
            //{
            //    LuaAPI.lua_gettable(L, -2);               
            //    LuaTypes type = LuaAPI.lua_type(L, -1);
            //    if (type == LuaTypes.LUA_TUSERDATA)
            //    {
            //        int udata = LuaAPI.luanet_tonetobject(L, -1);
            //        if (udata != -1)
            //        {
            //            translator.collectObject(udata);
            //        }
            //    }
            //}
            translator.push(L, val);
            LuaAPI.lua_settable(L, -3);
        }

        public ListDictionary GetTableDict(LuaTable table)
        {
            ListDictionary dict = new ListDictionary();

            int oldTop = LuaAPI.lua_gettop(L);
            translator.push(L, table);
            LuaAPI.lua_pushnil(L);
            while (LuaAPI.lua_next(L, -2) != 0)
            {
                dict[translator.getObject(L, -2)] = translator.getObject(L, -1);
                LuaAPI.lua_settop(L, -2);
            }
            LuaAPI.lua_settop(L, oldTop);

            return dict;
        }

        internal void dispose(int reference)
        {
            if (L != IntPtr.Zero)
            {
                LuaAPI.lua_unref(L, reference);
            }
        }

        internal object rawGetObject(int reference, string field)
        {
            int oldTop = LuaAPI.lua_gettop(L);
            LuaAPI.ulua_rawgeti(L, LuaAPI.LUA_REGISTRYINDEX, reference);
            LuaAPI.lua_pushstring(L, field);
            LuaAPI.lua_rawget(L, -2);
            object obj = translator.getObject(L, -1);
            LuaAPI.lua_settop(L, oldTop);
            return obj;
        }

        internal object getObject(int reference, string field)
        {
            int oldTop = LuaAPI.lua_gettop(L);
            LuaAPI.ulua_rawgeti(L, LuaAPI.LUA_REGISTRYINDEX, reference);
            object returnValue = getObject(field.Split(new char[] { '.' }));
            LuaAPI.lua_settop(L, oldTop);
            return returnValue;
        }

        internal object getObject(int reference, object field)
        {
            int oldTop = LuaAPI.lua_gettop(L);
            LuaAPI.ulua_rawgeti(L, LuaAPI.LUA_REGISTRYINDEX, reference);
            translator.push(L, field);
            LuaAPI.lua_gettable(L, -2);
            object returnValue = translator.getObject(L, -1);
            LuaAPI.lua_settop(L, oldTop);
            return returnValue;
        }

        internal void setObject(int reference, string field, object val)
        {
            int oldTop = LuaAPI.lua_gettop(L);
            LuaAPI.ulua_rawgeti(L, LuaAPI.LUA_REGISTRYINDEX, reference);
            setObject(field.Split(new char[] { '.' }), val);
            LuaAPI.lua_settop(L, oldTop);
        }


        internal bool compareRef(int ref1, int ref2)
        {
            return ref1 == ref2;
        }

        internal void pushCSFunction(LuaCSFunction function)
        {
            translator.pushFunction(L, function);
        }

        public void Dispose()
        {
            translator = null;
            L = IntPtr.Zero;
            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}