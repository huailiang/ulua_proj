using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Text;
using System;

namespace LuaEditor
{
    public class LuaTool
    {
        public static string lua_path = Application.dataPath + "/uLua/Resources/Lua/";

        private static List<FileInfo> g_files = new List<FileInfo>();

        [MenuItem("Lua/Trans-utf8encode")]
        public static void TransNoBom()
        {
            Handle(TransNoBomUtf8);
        }

        [MenuItem("Lua/Suffix-lua2txt")]
        public static void TransPostfixTxt()
        {
            Handle(TransFileSuffix);
        }

        private static void Handle(Action action)
        {
            g_files.Clear();
            SearchDirectoryFiles(lua_path, "*.txt");
            if (action != null) action();
            AssetDatabase.Refresh();
            g_files.Clear();
            EditorUtility.DisplayDialog("tip", "transfer all file finish!", "ok");
        }

        private static void SearchDirectoryFiles(string dir_path, string searchPattern)
        {
            DirectoryInfo dir = new DirectoryInfo(dir_path);
            var files = dir.GetFiles(searchPattern);
            foreach (var item in files)
            {
                g_files.Add(item);
            }
            var dirs = dir.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                SearchDirectoryFiles(dirs[i].FullName, searchPattern);
            }
        }

        private static void TransFileSuffix()
        {
            for (int i = 0; i < g_files.Count; i++)
            {
                FileInfo fi = g_files[i];
                if (fi.FullName.EndsWith(".lua.txt.txt"))
                {
                    string mofile = fi.FullName.Replace(".lua.txt.txt", ".lua.txt");
                    if (!File.Exists(mofile)) fi.MoveTo(mofile);
                }
                else if (fi.FullName.EndsWith(".lua"))
                {
                    string mofile = fi.FullName.Replace(".lua", ".lua.txt");
                    if (!File.Exists(mofile)) fi.MoveTo(mofile);
                }
            }
        }

        private static void TransNoBomUtf8()
        {
            for (int i = 0; i < g_files.Count; i++)
            {
                string file = g_files[i].FullName;
                string content = File.ReadAllText(file);
                using (var sw = new StreamWriter(file, false, new UTF8Encoding(false)))
                {
                    Debug.Log(file);
                    sw.Write(content);
                }
            }
        }

    }
}