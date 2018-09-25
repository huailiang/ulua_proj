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
