using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using UnityEngine;
using System.IO;
using LuaInterface;

using MethodBody = Mono.Cecil.Cil.MethodBody;
using OpCodes = Mono.Cecil.Cil.OpCodes;

namespace LuaEditor
{

    public class CodeInjector
    {
        private readonly List<string> assemblys = new List<string>();

        public void AddAssembly(string scriptPath)
        {
            assemblys.Add(scriptPath);
        }

        public void Run()
        {
            foreach (var path in assemblys)
            {
                var assembly = ReadAssembly(path);
                if (DoInjector(assembly))
                {
                    SaveAssembly(path, assembly);
                }
            }
        }

        private AssemblyDefinition ReadAssembly(string path)
        {
            Debug.Log(string.Format("ReadAssembly: {0}", path));
            var assemblyResolver = new DefaultAssemblyResolver();
            if (Directory.Exists(InjectEditor.ExternalLibFolder))
            {
                assemblyResolver.AddSearchDirectory(InjectEditor.ExternalLibFolder);
            }
            var readerParameters = new ReaderParameters
            {
                AssemblyResolver = assemblyResolver,
            };
            return AssemblyDefinition.ReadAssembly(path, readerParameters);
        }

        private void SaveAssembly(string path, AssemblyDefinition assembly)
        {
            Debug.Log(string.Format("WriteAssembly: {0}", path));
            assembly.Write(path);
        }

        private static bool DoInjector(AssemblyDefinition assembly)
        {
            var modified = false;
            foreach (var type in assembly.MainModule.Types)
            {
                if (type.HasCustomAttribute<HotfixAttribute>())
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.HasCustomAttribute<HotfixIgnoreAttribute>()) continue;
                        DoInject(assembly, method, type);
                        modified = true;
                    }
                }
                else
                {
                    foreach (var method in type.Methods)
                    {
                        if (!method.HasCustomAttribute<HotfixAttribute>()) continue;
                        DoInject(assembly, method, type);
                        modified = true;
                    }
                }
            }
            return modified;
        }

        private static bool IngoreMethod(MethodDefinition method)
        {
            int cnt = method.Parameters.Count;
            for (int i = 0; i < cnt; i++)
            {
                if (method.Parameters[i].ParameterType.IsByReference)
                    return true;
            }
            return false;
        }

        private static void DoInject(AssemblyDefinition assembly, MethodDefinition method, TypeDefinition type)
        {
            if (method.Name.Equals(".ctor") || !method.HasBody || IngoreMethod(method)) return;

            var instruction = method.Body.Instructions[0];
            var processor = method.Body.GetILProcessor();

            // bool result = HotfixPatch.IsRegist(type.Name)
            var hasPatchRef = assembly.MainModule.Import(typeof(HotfixPatch).GetMethod("IsRegist"));
            var current = InsertBefore(processor, instruction, processor.Create(OpCodes.Ldstr, type.Name));
            current = InsertAfter(processor, current, processor.Create(OpCodes.Ldstr, method.Name));
            current = InsertAfter(processor, current, processor.Create(OpCodes.Call, hasPatchRef));

            // if(result == false) jump to the under code
            current = InsertAfter(processor, current, processor.Create(OpCodes.Brfalse, instruction));

            // else HotfixPatch.Execute(type.Name, method.Name, args)
            var callPatchMethod = typeof(HotfixPatch).GetMethod("Execute");
            var callPatchRef = assembly.MainModule.Import(callPatchMethod);
            current = InsertAfter(processor, current, processor.Create(OpCodes.Ldstr, type.Name));
            current = InsertAfter(processor, current, processor.Create(OpCodes.Ldstr, method.Name));
            current = InsertAfter(processor, current, processor.Create(OpCodes.Ldstr, method.ReturnType.Name));
            var paramsCount = method.Parameters.Count;
            // 创建 args参数 object[] 集合
            current = InsertAfter(processor, current, processor.Create(OpCodes.Ldc_I4, paramsCount));
            current = InsertAfter(processor, current, processor.Create(OpCodes.Newarr, assembly.MainModule.Import(typeof(object))));
            for (int index = 0; index < paramsCount; index++)
            {
                var argIndex = method.IsStatic ? index : index + 1;
                // 压入参数
                current = InsertAfter(processor, current, processor.Create(OpCodes.Dup));
                current = InsertAfter(processor, current, processor.Create(OpCodes.Ldc_I4, index));
                var paramType = method.Parameters[index].ParameterType;

                current = InsertAfter(processor, current, processor.Create(OpCodes.Ldarg, argIndex));
                current = InsertAfter(processor, current, processor.Create(OpCodes.Box, paramType));
                current = InsertAfter(processor, current, processor.Create(OpCodes.Stelem_Ref));
            }
            current = InsertAfter(processor, current, processor.Create(OpCodes.Call, callPatchRef));
            var methodReturnVoid = method.ReturnType.FullName.Equals("System.Void");
            var patchCallReturnVoid = callPatchMethod.ReturnType.FullName.Equals("System.Void");
            // HotfixPatch.Execute()有返回值时
            if (!patchCallReturnVoid)
            {
                // 方法无返回值, 则需先Pop出栈区中Execute()返回的结果
                if (methodReturnVoid) current = InsertAfter(processor, current, processor.Create(OpCodes.Pop));
                // 方法有返回值时, 返回值进行拆箱
                else current = InsertAfter(processor, current, processor.Create(OpCodes.Unbox_Any, method.ReturnType));
            }
            // return
            InsertAfter(processor, current, processor.Create(OpCodes.Ret));

            // 重新计算语句位置偏移值
            ComputeOffsets(method.Body);
        }

        private static Instruction InsertBefore(ILProcessor processor, Instruction target, Instruction instruction)
        {
            processor.InsertBefore(target, instruction);
            return instruction;
        }

        private static Instruction InsertAfter(ILProcessor processor, Instruction target, Instruction instruction)
        {
            processor.InsertAfter(target, instruction);
            return instruction;
        }

        private static void ComputeOffsets(MethodBody body)
        {
            var offset = 0;
            foreach (var instruction in body.Instructions)
            {
                instruction.Offset = offset;
                offset += instruction.GetSize();
            }
        }
    }
}