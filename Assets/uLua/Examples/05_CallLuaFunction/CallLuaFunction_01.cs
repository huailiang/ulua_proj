using LuaInterface;
using UnityEngine;

public class CallLuaFunction_01 : MonoBehaviour
{

    private string script = @"
            function luaFunc(message)
                print(message)
                return 42
            end
        ";

    void Start()
    {
        LuaState l = new LuaState();
        l.DoString(script);
        LuaFunction f = l.GetFunction("luaFunc");
        object[] r = f.Call("I called a lua function!");
        print(r[0]);
    }
}
