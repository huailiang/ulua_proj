namespace LuaInterface
{
    using System;
    using System.Collections.Specialized;
    using System.Text;

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
        internal LuaCSFunction dofileFunction;
        internal LuaCSFunction import_wrapFunction;

        public LuaState()
        {
            L = LuaDLL.luaL_newstate();
            LuaDLL.luaL_openlibs(L);

            LuaDLL.lua_newtable(L);
            LuaDLL.lua_pushstring(L, "getmetatable");
            LuaDLL.lua_getglobal(L, "getmetatable");
            LuaDLL.lua_settable(L, -3);
            LuaDLL.lua_pushstring(L, "rawget");
            LuaDLL.lua_getglobal(L, "rawget");
            LuaDLL.lua_settable(L, -3);
            LuaDLL.lua_pushstring(L, "rawset");
            LuaDLL.lua_getglobal(L, "rawset");
            LuaDLL.lua_settable(L, -3);
            LuaDLL.lua_setglobal(L, "luanet");

            LuaDLL.lua_getglobal(L, "luanet");
            translator = new ObjectTranslator(this, L);
            translator.PushTranslator(L);

            panicCallback = new LuaCSFunction(LuaStatic.panic);
            LuaDLL.lua_atpanic(L, panicCallback);

            printFunction = new LuaCSFunction(LuaStatic.print);
            LuaDLL.lua_pushstdcallcfunction(L, printFunction);
            LuaDLL.lua_setglobal(L, "print");

            loadfileFunction = new LuaCSFunction(LuaStatic.loadfile);
            LuaDLL.lua_pushstdcallcfunction(L, loadfileFunction);
            LuaDLL.lua_setglobal(L, "loadfile");

            dofileFunction = new LuaCSFunction(LuaStatic.dofile);
            LuaDLL.lua_pushstdcallcfunction(L, dofileFunction);
            LuaDLL.lua_setglobal(L, "dofile");

            import_wrapFunction = new LuaCSFunction(LuaStatic.importWrap);
            LuaDLL.lua_pushstdcallcfunction(L, import_wrapFunction);
            LuaDLL.lua_setglobal(L, "import");

            loaderFunction = new LuaCSFunction(LuaStatic.loader);
            LuaDLL.lua_pushstdcallcfunction(L, loaderFunction);

            LuaDLL.lua_getglobal(L, "package");
            LuaDLL.lua_getfield(L, -1, "searchers");
            LuaDLL.lua_remove(L, -2); //remv table package
            int len = LuaDLL.lua_rawlen(L, -1);
            IntPtr fn = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(loaderFunction);
            IntPtr fn2 = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(printFunction);
            UnityEngine.Debug.Log("fn: " + fn.ToString() + " fn2: " + fn2.ToString());
            LuaDLL.lua_pushstdcallcfunction(L, loaderFunction);
            LuaDLL.xlua_rawseti(L, -2, len + 1);
            LuaDLL.lua_settop(L, 0); //clear stack
            tracebackFunction = new LuaCSFunction(LuaStatic.traceback);
        }

        public void Close()
        {
            if (L != IntPtr.Zero)
            {
                translator.Destroy();
                LuaDLL.lua_close(L);
            }
        }

        public ObjectTranslator GetTranslator()
        {
            return translator;
        }

        internal void ThrowExceptionFromError(int oldTop)
        {
            string err = LuaDLL.lua_tostring(L, -1);
            LuaDLL.lua_settop(L, oldTop);
            if (err == null) err = "Unknown Lua Error";
            throw new LuaScriptException(err, "");
        }

        internal int SetPendingException(Exception e)
        {
            if (e != null)
            {
                translator.throwError(L, e.ToString());
                LuaDLL.lua_pushnil(L);
                return 1;
            }
            return 0;
        }

        public LuaFunction LoadString(string chunk, string name, LuaTable env)
        {
            int oldTop = LuaDLL.lua_gettop(L);
            byte[] bt = Encoding.UTF8.GetBytes(chunk);

            if (LuaDLL.luaL_loadbuffer(L, bt, bt.Length, name) != 0)
                ThrowExceptionFromError(oldTop);

            if (env != null)
            {
                env.push(L);
                LuaDLL.lua_setfenv(L, -2);
            }
            LuaFunction result = translator.getFunction(L, -1);
            translator.popValues(L, oldTop);
            return result;
        }

        public object[] DoString(string chunk)
        {
            return DoString(chunk, "chunk", null);
        }

        public object[] DoString(string chunk, string chunkName, LuaTable env)
        {
            int oldTop = LuaDLL.lua_gettop(L);
            byte[] bt = Encoding.UTF8.GetBytes(chunk);
            if (LuaDLL.luaL_loadbuffer(L, bt, bt.Length, chunkName) == 0)
            {
                if (env != null)
                {
                    env.push(L);
                    LuaDLL.lua_setfenv(L, -2);
                }

                if (LuaDLL.lua_pcall(L, 0, -1, 0) == 0)
                    return translator.popValues(L, oldTop);
                else
                    ThrowExceptionFromError(oldTop);
            }
            else
                ThrowExceptionFromError(oldTop);

            return null;      // Never reached - keeps compiler happy
        }

        public object[] DoFile(string fileName)
        {
            return DoFile(fileName, null);
        }

        public object[] DoFile(string fileName, LuaTable env)
        {
            LuaDLL.lua_pushstdcallcfunction(L, tracebackFunction);
            int oldTop = LuaDLL.lua_gettop(L);
            byte[] bytes = LuaStatic.Load(fileName);
            if (bytes == null)
            {
                LuaDLL.lua_pop(L, 1);
                return null;
            }
            if (LuaDLL.luaL_loadbuffer(L, bytes, bytes.Length, fileName) == 0)
            {
                if (env != null)
                {
                    env.push(L);
                    //LuaDLL.lua_setfenv(L, -1);
                    LuaDLL.lua_setfenv(L, -2);
                }
                if (LuaDLL.lua_pcall(L, 0, -1, -2) == 0)
                {
                    object[] results = translator.popValues(L, oldTop);
                    LuaDLL.lua_pop(L, 1);
                    return results;
                }
                else
                {
                    ThrowExceptionFromError(oldTop);
                    LuaDLL.lua_pop(L, 1);
                }
            }
            else
            {
                ThrowExceptionFromError(oldTop);
                LuaDLL.lua_pop(L, 1);
            }
            return null;
        }

        public object this[string fullPath]
        {
            get
            {
                object returnValue = null;
                int oldTop = LuaDLL.lua_gettop(L);
                string[] path = fullPath.Split(new char[] { '.' });
                LuaDLL.lua_getglobal(L, path[0]);
                returnValue = translator.getObject(L, -1);
                if (path.Length > 1)
                {
                    string[] remainingPath = new string[path.Length - 1];
                    Array.Copy(path, 1, remainingPath, 0, path.Length - 1);
                    returnValue = getObject(remainingPath);
                }
                LuaDLL.lua_settop(L, oldTop);
                return returnValue;
            }
            set
            {
                int oldTop = LuaDLL.lua_gettop(L);
                string[] path = fullPath.Split(new char[] { '.' });
                if (path.Length == 1)
                {
                    translator.push(L, value);
                    LuaDLL.lua_setglobal(L, fullPath);
                }
                else
                {
                    LuaDLL.lua_getglobal(L, path[0]);
                    LuaTypes type = LuaDLL.lua_type(L, -1);
                    if (type == LuaTypes.LUA_TNIL)
                    {
                        Debugger.LogError("Table {0} not exists", path[0]);
                        LuaDLL.lua_settop(L, oldTop);
                        return;
                    }

                    string[] remainingPath = new string[path.Length - 1];
                    Array.Copy(path, 1, remainingPath, 0, path.Length - 1);
                    setObject(remainingPath, value);
                }
                LuaDLL.lua_settop(L, oldTop);
            }
        }


        /*
         * Navigates a table in the top of the stack, returning
         * the value of the specified field
         */
        internal object getObject(string[] remainingPath)
        {
            object returnValue = null;
            for (int i = 0; i < remainingPath.Length; i++)
            {
                LuaDLL.lua_pushstring(L, remainingPath[i]);
                LuaDLL.lua_gettable(L, -2);
                returnValue = translator.getObject(L, -1);
                if (returnValue == null) break;
            }
            return returnValue;
        }


        /*
         * Gets a function global variable
         */
        public LuaFunction GetFunction(string fullPath)
        {
            object obj = this[fullPath];
            return (obj is LuaCSFunction ? new LuaFunction((LuaCSFunction)obj, this) : (LuaFunction)obj);
        }

        internal void setObject(string[] remainingPath, object val)
        {
            for (int i = 0; i < remainingPath.Length - 1; i++)
            {
                LuaDLL.lua_pushstring(L, remainingPath[i]);
                LuaDLL.lua_gettable(L, -2);
            }
            LuaDLL.lua_pushstring(L, remainingPath[remainingPath.Length - 1]);

            //可以释放先
            //if (val == null)
            //{
            //    LuaDLL.lua_gettable(L, -2);               
            //    LuaTypes type = LuaDLL.lua_type(L, -1);

            //    if (type == LuaTypes.LUA_TUSERDATA)
            //    {
            //        int udata = LuaDLL.luanet_tonetobject(L, -1);

            //        if (udata != -1)
            //        {
            //            translator.collectObject(udata);
            //        }
            //    }
            //}
            translator.push(L, val);
            LuaDLL.lua_settable(L, -3);
        }

        public ListDictionary GetTableDict(LuaTable table)
        {
            ListDictionary dict = new ListDictionary();

            int oldTop = LuaDLL.lua_gettop(L);
            translator.push(L, table);
            LuaDLL.lua_pushnil(L);
            while (LuaDLL.lua_next(L, -2) != 0)
            {
                dict[translator.getObject(L, -2)] = translator.getObject(L, -1);
                LuaDLL.lua_settop(L, -2);
            }
            LuaDLL.lua_settop(L, oldTop);

            return dict;
        }

        internal void dispose(int reference)
        {
            if (L != IntPtr.Zero)
            {
                LuaDLL.lua_unref(L, reference);
            }
        }
        /*
         * Gets a field of the table corresponding to the provided reference
         * using rawget (do not use metatables)
         */
        internal object rawGetObject(int reference, string field)
        {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.xlua_rawgeti(L, LuaIndexes.LUA_REGISTRYINDEX, reference);
            LuaDLL.lua_pushstring(L, field);
            LuaDLL.lua_rawget(L, -2);
            object obj = translator.getObject(L, -1);
            LuaDLL.lua_settop(L, oldTop);
            return obj;
        }
        /*
         * Gets a field of the table or userdata corresponding to the provided reference
         */
        internal object getObject(int reference, string field)
        {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.xlua_rawgeti(L, LuaIndexes.LUA_REGISTRYINDEX, reference);
            object returnValue = getObject(field.Split(new char[] { '.' }));
            LuaDLL.lua_settop(L, oldTop);
            return returnValue;
        }
        /*
         * Gets a numeric field of the table or userdata corresponding the the provided reference
         */
        internal object getObject(int reference, object field)
        {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.xlua_rawgeti(L, LuaIndexes.LUA_REGISTRYINDEX, reference);
            translator.push(L, field);
            LuaDLL.lua_gettable(L, -2);
            object returnValue = translator.getObject(L, -1);
            LuaDLL.lua_settop(L, oldTop);
            return returnValue;
        }
        /*
         * Sets a field of the table or userdata corresponding the the provided reference
         * to the provided value
         */
        internal void setObject(int reference, string field, object val)
        {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.xlua_rawgeti(L, LuaIndexes.LUA_REGISTRYINDEX, reference);
            setObject(field.Split(new char[] { '.' }), val);
            LuaDLL.lua_settop(L, oldTop);
        }
        /*
         * Sets a numeric field of the table or userdata corresponding the the provided reference
         * to the provided value
         */
        internal void setObject(int reference, object field, object val)
        {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.xlua_rawgeti(L, LuaIndexes.LUA_REGISTRYINDEX, reference);
            translator.push(L, field);
            translator.push(L, val);
            LuaDLL.lua_settable(L, -3);
            LuaDLL.lua_settop(L, oldTop);
        }

        /*
         * Compares the two values referenced by ref1 and ref2 for equality
         */
        internal bool compareRef(int ref1, int ref2)
        {
            return ref1 == ref2;
        }

        internal void pushCSFunction(LuaCSFunction function)
        {
            translator.pushFunction(L, function);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            L = IntPtr.Zero;
            GC.SuppressFinalize(this);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                if (translator != null)
                {
                    translator.pendingEvents.Dispose();
                    translator = null;
                }
            }
        }

        #endregion
    }
}