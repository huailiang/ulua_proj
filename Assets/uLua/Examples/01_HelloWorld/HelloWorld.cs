using LuaInterface;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    LuaState l;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "Hello"))
        {
            if (l == null) l = new LuaState();
            string str =
                @"
                  require('hotfix_Scripts')
                  print('hello world!')";
            PrintSearchers("lua_pushstdcallcfunction");
            l.DoString(str);
        }
        if (GUI.Button(new Rect(20, 120, 200, 80), "Close"))
        {
            if (l != null)
            {
                Debug.Log("close state");
                l.Close();
            }
        }
    }


    internal void PrintSearchers(string tag)
    {
        LuaAPI.lua_getglobal(l.L, "package");
        LuaAPI.lua_getfield(l.L, -1, "searchers");
        LuaAPI.lua_remove(l.L, -2); //remv table package
        int len = LuaAPI.lua_rawlen(l.L, -1);
        string stype = string.Empty;
        for (int i = 1; i <= len; i++)
        {
            LuaAPI.xlua_rawgeti(l.L, -1, i);
            stype += LuaAPI.lua_type(l.L, -1) + " ";
            LuaAPI.xlua_rawseti(l.L, -2, i);
        }
        UnityEngine.Debug.Log("===> searchers " + tag + " length: " + len + " type:" + stype);
        LuaAPI.lua_pop(l.L, 1);
    }
}
