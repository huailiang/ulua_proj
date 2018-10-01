using UnityEngine;
using System;
using System.IO;

namespace LuaInterface
{
    public class Util
    {
        public static string uLuaPath { get { return Application.dataPath + "/uLua/"; } }

        public static string LuaResourcePath(string name)
        {
            string lowerName = name.ToLower();
            if (lowerName.EndsWith(".lua"))
            {
                int index = name.LastIndexOf('.');
                name = name.Substring(0, index);
            }
            name = name.Replace('.', '/');
            if (name.ToLower().StartsWith("hotfix"))
            {
                return "lua/Hotfix/" + name + ".lua";
            }
            else
            {
                return "lua/" + name + ".lua";
            }
        }

        public static void Log(string str)
        {
            Debug.Log(str);
        }

        public static void LogWarning(string str)
        {
            Debug.LogWarning(str);
        }

        public static void LogError(string str)
        {
            Debug.LogError(str);
        }

        public static void ClearMemory()
        {
            GC.Collect();
            Resources.UnloadUnusedAssets();
            LuaScriptMgr mgr = LuaScriptMgr.Instance;
            if (mgr != null && mgr.lua != null) mgr.LuaGC();
        }


        static int CheckRuntimeFile()
        {
            if (!Application.isEditor) return 0;
            string sourceDir = uLuaPath + "/LuaWrap/";
            if (!Directory.Exists(sourceDir))
            {
                return -2;
            }
            else
            {
                string[] files = Directory.GetFiles(sourceDir);
                if (files.Length < 3) return -2;
            }
            return 0;
        }


        public static bool CheckEnvironment()
        {
#if UNITY_EDITOR
            int resultId = CheckRuntimeFile();
            if (resultId == -2)
            {
                Debug.LogError("没有找到Wrap脚本缓存，单击Lua菜单下Gen Lua Wrap Files生成脚本！！");
                UnityEditor.EditorApplication.isPlaying = false;
                return false;
            }
#endif
            return true;
        }

        public static bool isApplePlatform
        {
            get
            {
                return Application.platform == RuntimePlatform.IPhonePlayer ||
                       Application.platform == RuntimePlatform.OSXEditor ||
                       Application.platform == RuntimePlatform.OSXPlayer;
            }
        }
    }
}