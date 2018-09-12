using LuaInterface;
using UnityEngine;

public class ScriptsFromFile_01 : MonoBehaviour
{

    public TextAsset scriptFile;
    LuaState l;


    // Update is called once per frame
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "Hello"))
        {
            if (l == null) l = new LuaState();
            l.DoString(scriptFile.text);
        }

    }
}
