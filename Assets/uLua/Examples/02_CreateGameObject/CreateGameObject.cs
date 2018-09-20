using LuaInterface;
using UnityEngine;


public class CreateGameObject : MonoBehaviour
{

    private string script = @"
            luanet.load_assembly('UnityEngine')
            GameObject = luanet.import_type('UnityEngine.GameObject')      
	        ParticleSystem = luanet.import_type('UnityEngine.ParticleSystem')         
            local newGameObj = GameObject('NewObj')
            local type = luanet.ctype(ParticleSystem)
            newGameObj:AddComponent(type)
        ";


    private string script2 = @"
            luanet.load_assembly('UnityEngine')
            ParticleSystem = UnityEngine.ParticleSystem
            local newGameObj = GameObject('NewObj2')
            newGameObj:AddComponent(ParticleSystem.GetClassType())
            newGameObj.transform.position = Vector3(4,0,1);
        ";

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "Reflect"))
        {
            LuaState lua = new LuaState();
            lua.DoString(script);
            lua.Close();
        }
        if (GUI.Button(new Rect(20, 120, 200, 80), "Wrap"))
        {
            LuaScriptMgr lua = new LuaScriptMgr();
            lua.Start();
            lua.DoString(script2);
            lua.Destroy();
        }
    }

}