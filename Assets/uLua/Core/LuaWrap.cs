using System;
using LuaInterface;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine.Profiling;
using System.Collections.Generic;

public struct LuaMethod
{
    public string name;
    public LuaCSFunction func;

    public LuaMethod(string str, LuaCSFunction f)
    {
        name = str;
        func = f;
    }
};

public struct LuaField
{
    public string name;
    public LuaCSFunction getter;
    public LuaCSFunction setter;

    public LuaField(string str, LuaCSFunction g, LuaCSFunction s)
    {
        name = str;
        getter = g;
        setter = s;        
    }
};



public class LuaStringBuffer
{
    //从lua端读取协议数据
    public LuaStringBuffer(IntPtr source, int len)
    {
        buffer = new byte[len];
        Marshal.Copy(source, buffer, 0, len);
    }

    //c#端创建协议数据
    public LuaStringBuffer(byte[] buf)
    {
        this.buffer = buf;
    }

    public byte[] buffer = null;
}

public class LuaRef
{
    public IntPtr L;
    public int reference;

    public LuaRef(IntPtr L, int reference)
    {
        this.L = L;
        this.reference = reference;
    }
}
public static class ClientProfiler
{
    [Conditional("UNITY_EDITOR")]
    public static void BeginSample(int id)
    {
        {
            string name;
            _showNames.TryGetValue(id, out name);
            name = name ?? string.Empty;

            Profiler.BeginSample(name);
            ++_sampleDepth;
        }
    }

    [Conditional("UNITY_EDITOR")]
    public static void BeginSample(int id, string name)
    {
        {
            name = name ?? string.Empty;
            _showNames[id] = name;

            Profiler.BeginSample(name);
            ++_sampleDepth;
        }
    }

    [Conditional("UNITY_EDITOR")]
    internal static void BeginSample(string name)
    {
        name = name ?? string.Empty;
        Profiler.BeginSample(name);
        ++_sampleDepth;
    }

    [Conditional("UNITY_EDITOR")]
    public static void EndSample()
    {
        if (_sampleDepth > 0)
        {
            --_sampleDepth;
            Profiler.EndSample();
        }
    }

    private static int _sampleDepth;
    private static readonly Dictionary<int, string> _showNames = new Dictionary<int, string>();
}