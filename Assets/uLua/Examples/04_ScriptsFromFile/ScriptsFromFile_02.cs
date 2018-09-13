using UnityEngine;
using LuaInterface;

public class ScriptsFromFile_02 : MonoBehaviour
{
    LuaScriptMgr mgr;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "ReadFile"))
        {
            if (mgr == null)
            {
                mgr = new LuaScriptMgr();
                mgr.Start();
            }
            mgr.lua.DoFile("hotfix_hello");
        }
        if (GUI.Button(new Rect(20, 120, 200, 80), "Dispose"))
        {
            if (mgr != null)
            {
                mgr.Destroy();
                Debug.Log("destroy mgr");
            }
        }
    }
}