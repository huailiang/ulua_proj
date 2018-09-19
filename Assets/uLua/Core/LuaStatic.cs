namespace LuaInterface
{
    using System;
    using UnityEngine;

    public delegate byte[] ReadLuaFile(string name);

    public static class LuaStatic
    {
        public static ReadLuaFile Load = null;

        static LuaStatic()
        {
            Load = DefaultLoader;
        }

        static byte[] DefaultLoader(string name)
        {
            byte[] str = null;
            string luapath = Util.LuaResourcePath(name);
            TextAsset luaCode = Resources.Load<TextAsset>(luapath);
            if (luaCode != null) str = luaCode.bytes;
            return str;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int panic(IntPtr L)
        {
            string reason = String.Format("unprotected error in call to Lua API ({0})", LuaDLL.lua_tostring(L, -1));
            LuaDLL.lua_pop(L, 1);
            throw new LuaException(reason);
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int traceback(IntPtr L)
        {
            LuaDLL.lua_getglobal(L, "debug");
            LuaDLL.lua_getfield(L, -1, "traceback");
            LuaDLL.lua_pushvalue(L, 1);
            LuaDLL.lua_pushnumber(L, 2);
            LuaDLL.lua_pcall(L, 2, 1, 0);
            return 1;
        }


        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int print(IntPtr L)
        {
            // For each argument we'll 'tostring' it
            int n = LuaDLL.lua_gettop(L);
            string s = String.Empty;
            LuaDLL.lua_getglobal(L, "tostring");

            for (int i = 1; i <= n; i++)
            {
                LuaDLL.lua_pushvalue(L, -1);  /* function to be called */
                LuaDLL.lua_pushvalue(L, i);   /* value to print */
                LuaDLL.lua_pcall(L, 1, 1, 0);

                if (i > 1)
                {
                    s += "\t";
                }
                s += LuaDLL.lua_tostring(L, -1);

                LuaDLL.lua_pop(L, 1);  /* pop result */

            }
            Debug.Log("LUA: " + s);

            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int loader(IntPtr L)
        {
            // Get script to load
            string fileName = string.Empty;
            fileName = LuaDLL.lua_tostring(L, 1);

            string lowerName = fileName.ToLower();
            if (lowerName.EndsWith(".lua"))
            {
                int index = fileName.LastIndexOf('.');
                fileName = fileName.Substring(0, index);
            }
            fileName = fileName.Replace('.', '/');

            LuaScriptMgr mgr = LuaScriptMgr.GetMgrFromLuaState(L);
            int oldTop = LuaDLL.lua_gettop(L);
            if (mgr != null)
            {
                LuaDLL.lua_pushstdcallcfunction(L, mgr.lua.tracebackFunction);
            }

            byte[] text = LuaStatic.Load(fileName);
            if (text == null)
            {
                if (!fileName.Contains("mobdebug"))
                {
                    Debugger.LogError("Loader lua file failed: {0}", fileName);
                }
                LuaDLL.lua_pop(L, 1);
                return 0;
            }
            if (LuaDLL.luaL_loadbuffer(L, text, text.Length, fileName) != 0)
            {
                mgr.lua.ThrowExceptionFromError(oldTop + 1);
                LuaDLL.lua_pop(L, 1);
            }
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int dofile(IntPtr L)
        {
            string fileName = String.Empty;
            fileName = LuaDLL.lua_tostring(L, 1);

            string lowerName = fileName.ToLower();
            if (lowerName.EndsWith(".lua"))
            {
                int index = fileName.LastIndexOf('.');
                fileName = fileName.Substring(0, index);
            }
            fileName = fileName.Replace('.', '/') + ".lua";

            int n = LuaDLL.lua_gettop(L);

            byte[] text = Load(fileName);

            if (text == null)
            {
                return LuaDLL.lua_gettop(L) - n;
            }

            if (LuaDLL.luaL_loadbuffer(L, text, text.Length, fileName) == 0)
            {
                LuaDLL.lua_pcall(L, 0, LuaDLL.LUA_MULTRET, 0);
            }

            return LuaDLL.lua_gettop(L) - n;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int loadfile(IntPtr L)
        {
            return loader(L);
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int importWrap(IntPtr L)
        {
            string fileName = String.Empty;
            fileName = LuaDLL.lua_tostring(L, 1);
            fileName = fileName.Replace('.', '_');
            if (!string.IsNullOrEmpty(fileName))
            {
                LuaBinder.Bind(L, fileName);
            }
            return 0;
        }
    }
        
}
