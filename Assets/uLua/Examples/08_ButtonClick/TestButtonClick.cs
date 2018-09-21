using UnityEngine;
using UnityEngine.UI;
using LuaInterface;

public class TestButtonClick : MonoBehaviour
{
    public Button button;

    private string script = @"
            TestButtonClick = luanet.import_type('TestButtonClick')  

            function OnClicked(btn)
                print('lua clicked')
            end

            local go =  GameObject.Find('Canvas/Button')
            TestButtonClick.AttachListener(go,OnClicked)
        ";

    void Start()
    {
        LuaScriptMgr lua = new LuaScriptMgr();
        lua.Start();
        lua.DoString(script);
    }


    public static void AttachListener(GameObject go, LuaFunction cb)
    {
        Button btn = go.GetComponent<Button>();
        btn.onClick.AddListener(() => { Debug.Log("c# clicked"); cb.Call(btn); });
    }
}
