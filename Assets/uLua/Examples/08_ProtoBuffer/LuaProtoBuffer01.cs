using LuaInterface;
using UnityEngine;

public class LuaProtoBuffer01 : MonoBehaviour
{
    private string script = @"      
        function decoder()  
            local msg = login_pb.LoginRequest()
            msg:ParseFromString(TestProtol.data)
            print('id:'..msg.id..' name:'..msg.name..' email:'..msg.email)
        end

        function encoder()                           
            local msg = login_pb.LoginRequest()
            msg.id = 100
            msg.name = 'foo'
            msg.email = 'bar'
            local pb_data = msg:SerializeToString()
            TestProtol.data = pb_data
        end
        ";

    void Start()
    {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        TestProtolWrap.Register(mgr.GetL());
        mgr.DoFile("3rd/pblua/login_pb");
        mgr.DoString(script);

        LuaFunction func = mgr.GetLuaFunction("encoder");
        func.Call();
        func.Release();

        func = mgr.GetLuaFunction("decoder");
        func.Call();
        func.Release();
    }
}
