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
        LuaDLL.lua_getglobal(l.L, "package");
        LuaDLL.lua_getfield(l.L, -1, "searchers");
        LuaDLL.lua_remove(l.L, -2); //remv table package
        int len = LuaDLL.lua_rawlen(l.L, -1);
        string stype = string.Empty;
        for (int i = 1; i <= len; i++)
        {
            LuaDLL.xlua_rawgeti(l.L, -1, i);
            stype += LuaDLL.lua_type(l.L, -1) + " ";
            LuaDLL.xlua_rawseti(l.L, -2, i);
        }
        UnityEngine.Debug.Log("===> searchers " + tag + " length: " + len + " type:" + stype);
        LuaDLL.lua_pop(l.L, 1);
    }
}
