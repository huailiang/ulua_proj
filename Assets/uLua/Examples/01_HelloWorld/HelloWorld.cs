using LuaInterface;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 50), "Hello"))
        {
            LuaState l = new LuaState();
            string str = "print('hello world!')";
            l.DoString(str);
            l.Close();
        }
    }

}
