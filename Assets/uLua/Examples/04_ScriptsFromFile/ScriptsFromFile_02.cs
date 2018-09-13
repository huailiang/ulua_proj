using LuaInterface;
using UnityEngine;

public class ScriptsFromFile_02 : MonoBehaviour
{
    LuaState l;

    LuaScriptMgr mgr;


    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "Hello"))
        {
            if (l == null) l = new LuaState();
            l.DoFile("hotfix_hello");
        }
        if (GUI.Button(new Rect(20, 120, 200, 80), "File"))
        {
             mgr = new LuaScriptMgr();
            mgr.Start();
            mgr.lua.DoFile("hotfix_hello");
          //  mgr.Destroy();
        }
    }
}