using LuaInterface;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    LuaState l;
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 80), "Hello"))
        {
            if (l == null) l = new LuaState();
            string str =
                @"
                  require('hotfix_Scripts')
                  print('hello world!')";
            l.DoString(str);
        }
        if (GUI.Button(new Rect(20, 120, 200, 80), "Close"))
        {
            if (l != null)
            {
                Debug.Log("close state");
                l.Close();
            }
        }
    }

}
