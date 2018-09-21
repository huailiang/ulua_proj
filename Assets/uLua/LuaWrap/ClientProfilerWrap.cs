using System;
using LuaInterface;

public class ClientProfilerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("BeginSample", BeginSample),
			new LuaMethod("EndSample", EndSample),
			new LuaMethod("New", _CreateClientProfiler),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaScriptMgr.RegisterLib(L, "ClientProfiler", regs);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateClientProfiler(IntPtr L)
	{
		LuaAPI.luaL_error(L, "ClientProfiler class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(ClientProfiler);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BeginSample(IntPtr L)
	{
		int count = LuaAPI.lua_gettop(L);

		if (count == 1)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			ClientProfiler.BeginSample(arg0);
			return 0;
		}
		else if (count == 2)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			string arg1 = LuaScriptMgr.GetLuaString(L, 2);
			ClientProfiler.BeginSample(arg0,arg1);
			return 0;
		}
		else
		{
			LuaAPI.luaL_error(L, "invalid arguments to method: ClientProfiler.BeginSample");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int EndSample(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ClientProfiler.EndSample();
		return 0;
	}
}

