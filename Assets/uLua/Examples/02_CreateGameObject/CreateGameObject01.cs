using LuaInterface;
using UnityEngine;


public class CreateGameObject01 : MonoBehaviour
{

    private string script = @"
            luanet.load_assembly('UnityEngine')
            GameObject = luanet.import_type('UnityEngine.GameObject')      
	        ParticleSystem = luanet.import_type('UnityEngine.ParticleSystem')         
            local newGameObj = GameObject('NewObj')
            local type = luanet.ctype(ParticleSystem)
            newGameObj:AddComponent(type)
        ";
    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "Exec"))
        {
            LuaState lua = new LuaState();
            lua.DoString(script);
            lua.Close();
        }
    }

}