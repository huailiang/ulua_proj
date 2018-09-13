using LuaInterface;
using UnityEngine;

public class CreateGameObject02 : MonoBehaviour
{

    private string script = @"
            luanet.load_assembly('UnityEngine')
            GameObject = UnityEngine.GameObject
            ParticleSystem = UnityEngine.ParticleSystem
            local newGameObj = GameObject('NewObj')
            newGameObj:AddComponent(ParticleSystem.GetClassType())
        ";


    void Start()
    {
        LuaScriptMgr lua = new LuaScriptMgr();
        lua.Start();
        lua.DoString(script);
    }

}
