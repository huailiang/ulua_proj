using LuaInterface;
using UnityEngine;

public class CreateGameObject02 : MonoBehaviour
{

    private string script = @"
            luanet.load_assembly('UnityEngine')
            --GameObject = UnityEngine.GameObject
            ParticleSystem = UnityEngine.ParticleSystem
            print(ParticleSystem)
            print(GameObject)
            local newGameObj = GameObject('NewObj')
           -- print(newGameObj)
           newGameObj:AddComponent(ParticleSystem.GetClassType())
        ";


    void Start()
    {
        LuaScriptMgr lua = new LuaScriptMgr();
        lua.Start();
        lua.DoString(script);
        lua.Destroy();
    }

}
