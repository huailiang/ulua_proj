using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using LuaInterface;

public class BindType
{
    public string name;                 //类名称
    public Type type;
    public bool IsStatic;
    public string baseName = null;      //继承的类名字
    public string wrapName = "";        //产生的wrap文件名字
    public string libName = "";         //注册到lua的名字

    string GetTypeStr(Type t)
    {
        string str = t.ToString();

        if (t.IsGenericType)
        {
            str = GetGenericName(t);
        }
        else if (str.Contains("+"))
        {
            str = str.Replace('+', '.');
        }

        return str;
    }

    private static string[] GetGenericName(Type[] types)
    {
        string[] results = new string[types.Length];

        for (int i = 0; i < types.Length; i++)
        {
            if (types[i].IsGenericType)
            {
                results[i] = GetGenericName(types[i]);
            }
            else
            {
                results[i] = LuaExport.GetTypeStr(types[i]);
            }

        }

        return results;
    }

    private static string GetGenericName(Type type)
    {
        if (type.GetGenericArguments().Length == 0)
        {
            return type.Name;
        }

        Type[] gArgs = type.GetGenericArguments();
        string typeName = type.Name;
        string pureTypeName = typeName.Substring(0, typeName.IndexOf('`'));

        return pureTypeName + "<" + string.Join(",", GetGenericName(gArgs)) + ">";
    }

    public BindType(Type t)
    {
        type = t;

        name = LuaExport.GetTypeStr(t);

        if (t.IsGenericType)
        {
            libName = LuaExport.GetGenericLibName(t);
            wrapName = LuaExport.GetGenericLibName(t);
        }
        else
        {
            libName = t.FullName.Replace("+", ".");
            wrapName = name.Replace('.', '_');

            if (name == "object")
            {
                wrapName = "System_Object";
            }
        }

        if (t.BaseType != null)
        {
            baseName = LuaExport.GetTypeStr(t.BaseType);

            if (baseName == "ValueType")
            {
                baseName = null;
            }
        }

        if (t.GetConstructor(Type.EmptyTypes) == null && t.IsAbstract && t.IsSealed)
        {
            baseName = null;
            IsStatic = true;
        }
    }

    public BindType SetBaseName(string str)
    {
        baseName = str;
        return this;
    }

    public BindType SetWrapName(string str)
    {
        wrapName = str;
        return this;
    }

    public BindType SetLibName(string str)
    {
        libName = str;
        return this;
    }
}

[InitializeOnLoad]
public static class LuaBinding
{
    static bool beAutoGen = false;

    [MenuItem("Lua/Gen Lua Wrap Files", false, 11)]
    public static void Binding()
    {
        if (!Application.isPlaying)
        {
            EditorApplication.isPlaying = true;
        }

        BindType[] list = WrapFile.binds;

        for (int i = 0; i < list.Length; i++)
        {
            LuaExport.Clear();
            LuaExport.className = list[i].name;
            LuaExport.type = list[i].type;
            LuaExport.isStaticClass = list[i].IsStatic;
            LuaExport.baseClassName = list[i].baseName;
            LuaExport.wrapClassName = list[i].wrapName;
            LuaExport.libClassName = list[i].libName;
            LuaExport.Generate(null);
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < list.Length; i++)
        {
            sb.AppendFormat("\t\t{0}Wrap.Register();\r\n", list[i].wrapName);
        }
        EditorApplication.isPlaying = false;

        GenLuaBinder();
        GenLuaDelegates();
        Debug.Log("Generate lua binding files over");
        AssetDatabase.Refresh();
    }

    static void GenLuaBinder()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();
        sb.AppendLine("public static class LuaBinder");
        sb.AppendLine("{");
        sb.AppendLine("\tpublic static List<string> wrapList = new List<string>();");
        sb.AppendLine("\tpublic static void Bind(IntPtr L, string type = null)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tif (type == null || wrapList.Contains(type)) return;");
        sb.AppendLine("\t\twrapList.Add(type); type += \"Wrap\";");
        sb.AppendLine("\t\tswitch (type) {");
        string[] files = Directory.GetFiles("Assets/uLua/LuaWrap/", "*.cs", SearchOption.TopDirectoryOnly);

        List<string> wrapfiles = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            string wrapName = Path.GetFileName(files[i]);
            int pos = wrapName.LastIndexOf(".");
            wrapName = wrapName.Substring(0, pos);
            sb.AppendFormat("\t\t\tcase \"{0}\": {0}.Register(L); break;\r\n", wrapName);

            string wrapfile = wrapName.Substring(0, wrapName.Length - 4);
            wrapfiles.Add("import '" + wrapfile + "'");
        }

        string wrap = Application.dataPath + "/Resources/Lua/System/Wrap.lua.txt";
        File.WriteAllLines(wrap, wrapfiles.ToArray());

        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string file = Util.uLuaPath + "/Core/LuaBinder.cs";
        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
    }

    [MenuItem("Lua/Clear LuaBinder File + Wrap Files", false, 13)]
    public static void ClearLuaBinder()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();
        sb.AppendLine("public static class LuaBinder");
        sb.AppendLine("{");
        sb.AppendLine("\tpublic static List<string> wrapList = new List<string>();");
        sb.AppendLine("\tpublic static void Bind(IntPtr L, string type = null)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        string file = Util.uLuaPath + "/Core/LuaBinder.cs";
        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
        string wrapfile = Application.dataPath + "/Resources/Lua/System/Wrap.lua.txt";
        File.WriteAllText(wrapfile, string.Empty);

        ClearFiles(Util.uLuaPath + "/LuaWrap/");
        AssetDatabase.Refresh();
    }

    static DelegateType _DT(Type t)
    {
        return new DelegateType(t);
    }

    static HashSet<Type> GetCustomDelegateTypes()
    {
        BindType[] list = WrapFile.binds;
        HashSet<Type> set = new HashSet<Type>();
        BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.Instance;

        for (int i = 0; i < list.Length; i++)
        {
            Type type = list[i].type;
            FieldInfo[] fields = type.GetFields(BindingFlags.GetField | BindingFlags.SetField | binding);
            PropertyInfo[] props = type.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | binding);
            MethodInfo[] methods = null;
            methods = type.IsInterface ? type.GetMethods() : type.GetMethods(BindingFlags.Instance | binding);
            for (int j = 0; j < fields.Length; j++)
            {
                Type t = fields[j].FieldType;
                if (typeof(System.Delegate).IsAssignableFrom(t))
                {
                    set.Add(t);
                }
            }

            for (int j = 0; j < props.Length; j++)
            {
                Type t = props[j].PropertyType;
                if (typeof(System.Delegate).IsAssignableFrom(t))
                {
                    set.Add(t);
                }
            }

            for (int j = 0; j < methods.Length; j++)
            {
                MethodInfo m = methods[j];
                if (m.IsGenericMethod) continue;
                ParameterInfo[] pifs = m.GetParameters();
                for (int k = 0; k < pifs.Length; k++)
                {
                    Type t = pifs[k].ParameterType;
                    if (typeof(System.MulticastDelegate).IsAssignableFrom(t))
                    {
                        set.Add(t);
                    }
                }
            }
        }
        return set;
    }

    static void ClearFiles(string path)
    {
        string[] names = Directory.GetFiles(path);
        foreach (var filename in names)
        {
            File.Delete(filename);
        }
    }

    [MenuItem("Lua/Gen Lua Delegates", false, 2)]
    static void GenLuaDelegates()
    {
        LuaExport.Clear();
        List<DelegateType> list = new List<DelegateType>();
        list.AddRange(new DelegateType[] {
            _DT(typeof(Action<GameObject>)),
            _DT(typeof(Action)),
            _DT(typeof(UnityEngine.Events.UnityAction)),
        });
        HashSet<Type> set = beAutoGen ? LuaExport.eventSet : GetCustomDelegateTypes();
        foreach (Type t in set)
        {
            if (null == list.Find((p) => { return p.type == t; }))
            {
                list.Add(_DT(t));
            }
        }
        LuaExport.GenDelegates(list.ToArray());
        set.Clear();
        LuaExport.Clear();
        AssetDatabase.Refresh();
        Debug.Log("Create lua delegate over");
    }
}