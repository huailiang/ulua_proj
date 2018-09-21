using System.Collections.Generic;
using UnityEngine;

namespace LuaInterface
{

    public class HotfixPatch
    {
        static List<string> list = new List<string>();
        static LuaFunction lua_dispacher_func;
        const string LUA_FILE = "HotfixPatch";

        public static void Init(LuaScriptMgr luamgr)
        {
            luamgr.lua.DoFile(LUA_FILE);
            var func = luamgr.lua.GetFunction("Fetch");
            object[] ret = func.Call();
            Debug.Log("lua regist func cnt:" + (ret.Length));
            list.Clear();
            for (int i = 0, cnt = ret.Length; i < cnt; i++)
            {
                list.Add((string)ret[i]);
            }
            lua_dispacher_func = luamgr.lua.GetFunction("Dispacher");
        }


        /// <summary>
        /// 方法由IL层-注入代码调用
        /// </summary>
        public static bool IsRegist(string class_name, string func_name)
        {
            string key = class_name + "|" + func_name;
            return list.Contains(key);
        }

        /// <summary>
        /// 方法由IL层-注入代码调用
        /// </summary>
        public static object Execute(string class_name, string func_name, string type, object[] args)
        {
            if (lua_dispacher_func != null)
            {
                Debug.Log("inject arg len: " + args.Length);
                object val = lua_dispacher_func.Call(class_name, func_name, args)[0];
                if (val != null && typeof(System.Double) == val.GetType()) //先转换成double,再拆箱
                {
                    switch (type)
                    {
                        case "int":
                        case "Int32":
                            return (int)(double)val;
                        case "uint":
                        case "UInt32":
                            return (uint)(double)val;
                        case "float":
                        case "Single":
                            return (float)(double)val;
                        case "short":
                        case "Int16":
                            return (short)(double)val;
                        case "UInt64":
                        case "ulong":
                            return (ulong)(double)val;
                    }
                    return val;
                }
                else
                {
                    return val;
                }
            }
            return null;
        }

    }
}