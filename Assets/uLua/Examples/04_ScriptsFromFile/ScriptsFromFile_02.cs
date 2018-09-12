using LuaInterface;
using UnityEngine;

public class ScriptsFromFile_02 : MonoBehaviour
{
    void Start()
    {
        //LuaScriptMgr mgr = new LuaScriptMgr();
        //mgr.Start();
        //mgr.lua.DoFile("hotfix_hello");
        //mgr.Destroy();
    }


    LuaState l;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "Hello"))
        {
            if (l == null) l = new LuaState();
            l.DoFile("hotfix_hello");
        }
        if (GUI.Button(new Rect(20, 120, 200, 80), "Close"))
        {
            if (l != null)
            {
                l.Close();
            }
        }
    }
}