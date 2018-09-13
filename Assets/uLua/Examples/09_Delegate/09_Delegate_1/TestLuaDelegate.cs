using UnityEngine;

public class TestLuaDelegate
{
    public TestLuaDelegate() { }

    public delegate void VoidDelegate(GameObject go);

    public VoidDelegate onClick;

}