using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LuaEditor
{

    public static class InjectEditor
    {
        private static string _projectName
        {
            get { return Application.dataPath; }
        }

        public static bool CreateBackup
        {
            set { EditorPrefs.SetBool(_projectName + "CodeInjector CreateBackup", value); }
            get { return EditorPrefs.GetBool(_projectName + "CodeInjector CreateBackup", true); }
        }

        private static readonly string[] editorAssemblies =
        {
        "Assembly-CSharp-Editor.dll", "Assembly-CSharp-firstpass.dll","Assembly-CSharp-Editor-firstpass.dll"
    };

        public static string ExternalLibFolder { get { return string.Empty/* Application.dataPath + @"/Lib"*/; } }

        private static bool hasMidCodeInjectored;

        [MenuItem("Lua/IL-Injector/Inject")]
        private static void CodeInjectoring()
        {
            MidCodeInjectoring();
        }

        [MenuItem("Lua/IL-Injector/Clean")]
        private static void CleanInject()
        {
            string unity_path = EditorApplication.applicationContentsPath + "/Managed";
            string dll_path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Library");
            dll_path = Path.Combine(dll_path, "ScriptAssemblies");
            Debug.Log(unity_path + " " + dll_path);
            if (Directory.Exists(dll_path))
            {
                DirectoryInfo dir = new DirectoryInfo(dll_path);
                dir.Delete(true);
                Debug.Log("delete finish");
            }
            hasMidCodeInjectored = false;
            Directory.CreateDirectory(dll_path);
            RestartUnity();
        }


        [PostProcessScene]
        private static void MidCodeInjectoring()
        {
            if (hasMidCodeInjectored || Application.isPlaying || EditorApplication.isPlaying) return;

            Debug.Log("PostProcessBuild::OnPostProcessScene");
            string target = EditorUserBuildSettings.activeBuildTarget.ToString();
            if (DoCodeInjectorBuild(target))
            {
                hasMidCodeInjectored = true;
                Debug.Log("Inject Finish!");
            }
            else
            {
                Debug.LogWarning("CodeInjector: Failed to inject build!");
            }
        }

        private static bool DoCodeInjectorBuild(string plaformTag)
        {
            string scriptsPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Library");
            scriptsPath = Path.Combine(scriptsPath, "ScriptAssemblies");
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(scriptsPath, "*.dll"));
            string tmpPath = FileUtil.GetUniqueTempPathInProject() + "-Tmp" + plaformTag;
            Directory.CreateDirectory(tmpPath);
            foreach (string file in files)
            {
                File.Copy(file, Path.Combine(tmpPath, Path.GetFileName(file)), true);
            }
            if (!DoCodeInjector(tmpPath)) return false;
            string[] scripts = Directory.GetFiles(scriptsPath, "*.dll");
            foreach (string script in scripts)
            {
                var tmpFile = Path.Combine(tmpPath, Path.GetFileName(script));
                if (File.Exists(tmpFile))
                {
                    File.Copy(tmpFile, script, true);
                }
            }
            // Delete temporary files
            Directory.Delete(tmpPath, true);

            if (Directory.Exists(ExternalLibFolder))
            {
                if (!DoCodeInjector(ExternalLibFolder)) return false;
            }
            return true;
        }

        private static bool DoCodeInjector(string fromPath)
        {
            CodeInjector injector = new CodeInjector();
            DirectoryInfo dir = new DirectoryInfo(fromPath);
            FileInfo[] files = dir.GetFiles("*.dll");
            for (int index = 0; index < files.Length; index++)
            {
                if (!editorAssemblies.Contains(Path.GetFileName(files[index].FullName)))
                {
                    injector.AddAssembly(files[index].FullName);
                }
            }
            injector.Run();
            return true;
        }


        public static void DoCodeInjectorFolder(string folderPath)
        {
            DirectoryInfo assemblyDir = new DirectoryInfo(folderPath);
            string outputPath = assemblyDir.Parent.FullName + Path.DirectorySeparatorChar + "CodeInjectored";
            DoCodeInjector(folderPath);

            // Copy from CodeInjectored to Managed
            DirectoryInfo codeInjectoredDir = new DirectoryInfo(outputPath);
            CopyFilesFromDirectory(codeInjectoredDir, assemblyDir);

            // Delete CodeInjectored
            codeInjectoredDir.Delete(true);
            Debug.Log("CodeInjector: Finished injectoring and generating assemblies.");
        }

        // Post build
        [PostProcessBuild(1000)]
        private static void OnPostprocessBuildPlayer(BuildTarget buildTarget, string buildPath)
        {
            Debug.Log("PostProcessBuild::OnPostprocessBuildPlayer");
            bool windowsOrLinux = (buildTarget == BuildTarget.StandaloneWindows || buildTarget == BuildTarget.StandaloneWindows64);
            if (windowsOrLinux)
            {
                var buildDir = new FileInfo(buildPath).Directory;
                DirectoryInfo dataDir = buildDir.GetDirectories(Path.GetFileNameWithoutExtension(buildPath) + "_Data")[0];
                if (CreateBackup)
                {
                    // Create backup
                    DirectoryInfo backupDir = new DirectoryInfo(dataDir.FullName + " Backup");
                    if (!CopyFilesFromDirectory(dataDir, backupDir, true))
                    {
                        Debug.LogError("CodeInjector: Failed to create backup, stopping post-build injection and protection.");
                        return;
                    }
                }
                DirectoryInfo managedDir = new DirectoryInfo(dataDir.FullName + Path.DirectorySeparatorChar + "Managed");
                DoCodeInjectorFolder(managedDir.FullName);
            }
            else if (buildTarget == BuildTarget.StandaloneOSX)
            {
                FileInfo buildFileInfo = new FileInfo(buildPath);
                if (CreateBackup)
                {
                    // Create backup
                    DirectoryInfo appDir = new DirectoryInfo(buildFileInfo.FullName);
                    DirectoryInfo backupDir = new DirectoryInfo(buildFileInfo.FullName + " Backup");
                    if (!CopyFilesFromDirectory(appDir, backupDir, true))
                    {
                        Debug.LogError("CodeInjector: Failed to create backup, stopping post-build injection and protection.");
                        return;
                    }
                }
                DirectoryInfo dataDir = new DirectoryInfo(buildFileInfo.FullName + Path.DirectorySeparatorChar + "Contents" + Path.DirectorySeparatorChar + "Data");
                DirectoryInfo managedDir = new DirectoryInfo(dataDir.FullName + Path.DirectorySeparatorChar + "Managed");
                DoCodeInjectorFolder(managedDir.FullName);
            }
            else if (buildTarget == BuildTarget.Android || buildTarget == BuildTarget.iOS)
            {
                hasMidCodeInjectored = false;
            }
            else
            {
                Debug.LogWarning("CodeInjector: Post-build injection is not yet implemented for: " + buildTarget);
                return;
            }
            Debug.Log("CodeInjector: Post-build injection and protection finished.");
        }


        private static void RestartUnity()
        {
#if UNITY_EDITOR_WIN
            string install = Path.GetDirectoryName(EditorApplication.applicationContentsPath);
            string path = Path.Combine(install, "Unity.exe");
            string[] args = path.Split('\\');
            System.Diagnostics.Process po = new System.Diagnostics.Process();
            Debug.Log("install: " + install + " path: " + path);
            po.StartInfo.FileName = path;
            po.Start();

            System.Diagnostics.Process[] pro = System.Diagnostics.Process.GetProcessesByName(args[args.Length - 1].Split('.')[0]);//Unity
            foreach (var item in pro)
            {
                item.Kill();
            }
#endif
        }

        private static bool CopyFilesFromDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            return CopyFilesFromDirectory(source, destination, false, true);
        }

        private static bool CopyFilesFromDirectory(DirectoryInfo source, DirectoryInfo destination, bool copyDirectories)
        {
            return CopyFilesFromDirectory(source, destination, copyDirectories, true);
        }

        private static bool CopyFilesFromDirectory(DirectoryInfo source, DirectoryInfo destination, bool copyDirectories, bool replace)
        {
            if (!source.Exists)
            {
                Debug.LogError("CodeInjector: Cannot copy from " + source + " since it doesn't exists!");
                return false;
            }
            if (!destination.Exists) destination.Create();

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            for (int index = 0; index < files.Length; index++)
            {
                FileInfo file = files[index];
                file.CopyTo(Path.Combine(destination.FullName, file.Name), replace);
            }
            if (copyDirectories)
            {
                DirectoryInfo[] dirs = source.GetDirectories();
                for (int index = 0; index < dirs.Length; index++)
                {
                    DirectoryInfo directory = dirs[index];
                    string destinationDir = Path.Combine(destination.FullName, directory.Name);
                    CopyFilesFromDirectory(directory, new DirectoryInfo(destinationDir), true, replace);
                }
            }
            return true;
        }
    }
}