using UnityEngine;
using System.Collections;
using LuaInterface;

public class HelloWorld : MonoBehaviour
{

    void Start()
    {
        LuaState l = new LuaState();
        string str = "print('hello world!')";
        l.DoString(str);
    }

}
