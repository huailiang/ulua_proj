using UnityEngine;
using System.Collections;
using LuaInterface;

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

        // Lua functions can have variable returns, so we again store those as a C# object array, and in this case print the first one
        print(r[0]);
    }

}
