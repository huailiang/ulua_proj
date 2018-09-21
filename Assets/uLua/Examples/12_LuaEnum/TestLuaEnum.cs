using LuaInterface;
using UnityEngine;

public enum LuaEnumType
{
    AAA = 1,
    BBB = 2,
    CCC = 3,
    DDD = 4
}

public class TestLuaEnum : MonoBehaviour
{
    const string source = @"
        local type = LuaEnumType.IntToEnum(1);
        print(type == LuaEnumType.AAA);
    ";


    void Start()
    {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.lua.DoString(source);
    }
}
