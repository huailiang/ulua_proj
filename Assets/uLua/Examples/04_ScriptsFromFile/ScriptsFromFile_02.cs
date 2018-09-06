using UnityEngine;

public class ScriptsFromFile_02 : MonoBehaviour
{
    void Start()
    {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.lua.DoFile("hotfix_hello");
        mgr.Destroy();
    }
}