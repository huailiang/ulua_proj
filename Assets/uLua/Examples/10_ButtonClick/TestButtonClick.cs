using UnityEngine;
using UnityEngine.UI;
using LuaInterface;

public class TestButtonClick : MonoBehaviour
{

    public Button button;

    private string script = @"
            function OnClicked(btn)
                print('lua clicked')
            end

            local go =  GameObject.Find('Canvas/Button')
            -- 只有导出TestButtonClick， 这里才能被调到
            TestButtonClick.AttachListener(go,OnClicked)
        ";

    void Start()
    {
        LuaScriptMgr lua = new LuaScriptMgr();
        lua.Start();
        lua.DoString(script);
        lua.Destroy();
    }

    /// <summary>
    /// 需要事先将此类导出到wrap文件里
    /// </summary>
    static void AttachListener(GameObject go, LuaFunction cb)
    {
        Button btn = go.GetComponent<Button>();
        btn.onClick.AddListener(() => { Debug.Log("c# clicked"); cb.Call(btn); });
    }

}
