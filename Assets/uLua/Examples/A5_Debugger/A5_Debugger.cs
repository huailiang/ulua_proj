using UnityEngine;
using LuaInterface;

public class A5_Debugger : MonoBehaviour
{
    
    void Start()
    {

        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.DoFile("debugger");
    }

}