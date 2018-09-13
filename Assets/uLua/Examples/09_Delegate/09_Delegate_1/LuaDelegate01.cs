using LuaInterface;
using UnityEngine;

public class LuaDelegate01 : MonoBehaviour
{

    const string script = @"
        local func1 = function() print('测试委托1'); end
        local func2 = function(gameObj) print('测试委托2:>'..gameObj.name); end        
        
        function testDelegate(go) 
            local ev = go:AddComponent(TestDelegateListener.GetClassType());
        
            ---直接赋值模式---
            ev.onClick = func1;

            ---C#的加减模式---
            local delegate = DelegateFactory.TestLuaDelegate_VoidDelegate(func2);
            ev.onEvClick = ev.onEvClick + delegate;
            --ev.onEvClick = ev.onEvClick - delegate;
        end
    ";
    

    void Start()
    {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.DoString(script);
        
        LuaFunction f = mgr.GetLuaFunction("testDelegate");
        f.Call(gameObject);     //将自己对象传给lua
    }

}