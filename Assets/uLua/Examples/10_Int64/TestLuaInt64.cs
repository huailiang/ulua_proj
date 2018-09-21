using UnityEngine;
using LuaInterface;

public class TestLuaInt64 : MonoBehaviour
{
    private string script = @"
             print(tostring(a))
             print(tostring(b))
             print(tostring(a+b))
        ";


    void Start()
    {
        print("################## csharp ##################");
        long a = (long)Mathf.Pow(2, 58);
        long b = (long)Mathf.Pow(2, 57);
        print(a);
        print(b);
        print(a + b);

        print("################## lua ##################");
        LuaState l = new LuaState();
        LuaAPI.lua_pushinteger(l.L, a);
        LuaAPI.lua_setglobal(l.L, "a");
        LuaAPI.lua_pushinteger(l.L, b);
        LuaAPI.lua_setglobal(l.L, "b");
        l.DoString(script);
    }
}
