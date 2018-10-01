using LuaInterface;
using UnityEngine;

public class TestProtol : MonoBehaviour
{

    const string script = @"
    local pb = require 'pb'
    local protoc = require 'protoc'

    assert(protoc:load [[
    syntax = 'proto3';

    message Phone {
        string name        = 1;
        int64  phonenumber = 2;
    }
    message Person {
        string name     = 1;
        int32  age      = 2;
        string address  = 3;
        repeated Phone  contacts = 4;
    } ]])

    local data = {
    name = 'ilse',
    age  = 18,
        contacts = {
            { name = 'alice', phonenumber = 12312341234 },
            { name = 'bob',   phonenumber = 45645674567 }
        }
    }

    local bytes = assert(pb.encode('Person', data))
    print(pb.tohex(bytes))

    local data2 = assert(pb.decode('Person', bytes))
    print(data2.name)
    print(data2.age)
    print(data2.address)
    print(data2.contacts[1].name)
    print(data2.contacts[1].phonenumber)
    print(data2.contacts[2].name)
    print(data2.contacts[2].phonenumber)
    ";


    void Start()
    {
        LuaState lua = new LuaState();
        lua.DoString(script);
    }

}


/*
下面的例子是lua和c#protobuf交互的例子 需要c#测有protobuf的库 
这里只是写下过程 若读者有兴趣的话，可以照着代码在自己本地实现一遍
 */
// local pb = require 'pb'
// local protoc = require 'protoc'
// assert(protoc:load[[
// syntax = 'proto3';
// message Student
// {
//     int32 age = 1;
//     int32 num = 2;
// }]])

// function test(script, protobuff )
// 	-- body
//     print("---------------lua test1--------------")
//     local data3 = assert(pb.decode('Student', protobuff))
//     print(data3.age)
//     print(data3.num)
//     print("---------------lua test2--------------")
//     local data = {
//     age  = 12,
//     num = 40961
//     }
//     local t = Parseproto("Student", data)
//     script:LuaCall(t)
// end

// function Parseproto(protoName, data )
// 	-- body
//     local bytes = assert(pb.encode('Student', data))
// 	t={}
// 	for i=1,string.len(bytes) do
// 		table.insert(t, string.byte (string.sub(bytes, i, i)))
// 	end
// 	return t
// end



// using LuaInterface;
// using System.IO;
// using UnityEngine;
// using Google.Protobuf;
// using System;
// using System.Collections.Specialized;
// using System.Collections;
// public class TestProtol : MonoBehaviour
// {
//     void Start()
//     {
//         WriteStudentBuf();

//         LuaScriptMgr lmr = new LuaScriptMgr();
//         lmr.Start();
//         lmr.lua.DoFile("prototest");

//         luaStringBuffer = new LuaStringBuffer(ms.ToArray());
//         LuaFunction func = lmr.GetLuaFunction("test");
//         func.Call(this, luaStringBuffer);
//         func.Release();
//     }

//     private MemoryStream ms;
//     private CodedOutputStream sharedOutStream;
//     private LuaStringBuffer luaStringBuffer;

//     private void WriteStudentBuf()
//     {
//         ms = new MemoryStream();
//         sharedOutStream = new CodedOutputStream(ms);
//         Student proto = new Student();
//         proto.Age = 18;
//         proto.Num = 4096;
//         ms.SetLength(0);
//         sharedOutStream.Set(ms);
//         proto.WriteToSharedStream(sharedOutStream);
//         Util.PrintBytes(ms.ToArray());
//         File.WriteAllBytes(Application.dataPath + "/student.bytes", ms.ToArray());
//     }

//     public void LuaCall(object param)
//     {
//         Debug.Log("unity engine func called by lua: " + param.GetType());
//         LuaTable table = param as LuaTable;
//         object[] bytes = table.ToArray<object>();
//         byte[] byteArr = new byte[table.Count];
//         for (int i = 0; i < bytes.Length; i++)
//         {
//             byteArr[i] = (byte)(double)(bytes[i]);
//         }
//         Util.PrintBytes(byteArr);
//         Student stu = Student.Parser.ParseFrom(byteArr);
//         Debug.Log("age: " + stu.Age + "  num: " + stu.Num);
//     }

// }
