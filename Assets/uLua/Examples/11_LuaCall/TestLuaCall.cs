using LuaInterface;
using UnityEngine;

public class TestLuaCall : MonoBehaviour
{

    const string script = @"
        TestLuaCall = luanet.import_type('TestLuaCall')  

        LuaClass = {}
        LuaClass.__index = LuaClass

        function LuaClass:New() 
            local self = {};   
            setmetatable(self, LuaClass); 
            return self;    
        end

        function LuaClass:test() 
            TestLuaCall.OnSharpCall(self, self.callback);
        end

        function LuaClass:callback()
            print('test--->>>');
        end

        LuaClass:New():test();
    ";

    // Use this for initialization
    void Start()
    {
        LuaState lua = new LuaState();
        lua.DoString(script);
    }

    public static void OnSharpCall(LuaTable self, LuaFunction func)
    {
        func.Call(self);
    }
}