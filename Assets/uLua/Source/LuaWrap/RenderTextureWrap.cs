using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class RenderTextureWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetTemporary", GetTemporary),
			new LuaMethod("ReleaseTemporary", ReleaseTemporary),
			new LuaMethod("ResolveAntiAliasedSurface", ResolveAntiAliasedSurface),
			new LuaMethod("Create", Create),
			new LuaMethod("Release", Release),
			new LuaMethod("IsCreated", IsCreated),
			new LuaMethod("DiscardContents", DiscardContents),
			new LuaMethod("MarkRestoreExpected", MarkRestoreExpected),
			new LuaMethod("GenerateMips", GenerateMips),
			new LuaMethod("GetNativeDepthBufferPtr", GetNativeDepthBufferPtr),
			new LuaMethod("SetGlobalShaderProperty", SetGlobalShaderProperty),
			new LuaMethod("SupportsStencil", SupportsStencil),
			new LuaMethod("New", _CreateRenderTexture),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("width", get_width, set_width),
			new LuaField("height", get_height, set_height),
			new LuaField("vrUsage", get_vrUsage, set_vrUsage),
			new LuaField("depth", get_depth, set_depth),
			new LuaField("isPowerOfTwo", get_isPowerOfTwo, set_isPowerOfTwo),
			new LuaField("sRGB", get_sRGB, null),
			new LuaField("format", get_format, set_format),
			new LuaField("useMipMap", get_useMipMap, set_useMipMap),
			new LuaField("autoGenerateMips", get_autoGenerateMips, set_autoGenerateMips),
			new LuaField("dimension", get_dimension, set_dimension),
			new LuaField("volumeDepth", get_volumeDepth, set_volumeDepth),
			new LuaField("memorylessMode", get_memorylessMode, set_memorylessMode),
			new LuaField("antiAliasing", get_antiAliasing, set_antiAliasing),
			new LuaField("bindTextureMS", get_bindTextureMS, set_bindTextureMS),
			new LuaField("enableRandomWrite", get_enableRandomWrite, set_enableRandomWrite),
			new LuaField("useDynamicScale", get_useDynamicScale, set_useDynamicScale),
			new LuaField("colorBuffer", get_colorBuffer, null),
			new LuaField("depthBuffer", get_depthBuffer, null),
			new LuaField("active", get_active, set_active),
			new LuaField("descriptor", get_descriptor, set_descriptor),
		};

		LuaScriptMgr.RegisterLib(L, "UnityEngine.RenderTexture", typeof(RenderTexture), regs, fields, typeof(Texture));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRenderTexture(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(RenderTextureDescriptor)))
		{
			RenderTextureDescriptor arg0 = (RenderTextureDescriptor)LuaScriptMgr.GetNetObject(L, 1, typeof(RenderTextureDescriptor));
			RenderTexture obj = new RenderTexture(arg0);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else if (count == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(RenderTexture)))
		{
			RenderTexture arg0 = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 1, typeof(RenderTexture));
			RenderTexture obj = new RenderTexture(arg0);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else if (count == 3)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTexture obj = new RenderTexture(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else if (count == 4)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTexture obj = new RenderTexture(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else if (count == 5)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite arg4 = (RenderTextureReadWrite)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			RenderTexture obj = new RenderTexture(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RenderTexture.New");
		}

		return 0;
	}

	static Type classType = typeof(RenderTexture);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_width(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index width on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.width);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_height(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.height);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vrUsage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vrUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vrUsage on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.vrUsage);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_depth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depth on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.depth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPowerOfTwo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPowerOfTwo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPowerOfTwo on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isPowerOfTwo);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sRGB(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sRGB");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sRGB on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.sRGB);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_format(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name format");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index format on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.format);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useMipMap(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useMipMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useMipMap on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.useMipMap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autoGenerateMips(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name autoGenerateMips");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index autoGenerateMips on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.autoGenerateMips);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dimension(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dimension");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dimension on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.dimension);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_volumeDepth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volumeDepth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volumeDepth on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.volumeDepth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_memorylessMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name memorylessMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index memorylessMode on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.memorylessMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_antiAliasing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name antiAliasing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index antiAliasing on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.antiAliasing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bindTextureMS(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bindTextureMS");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bindTextureMS on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.bindTextureMS);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enableRandomWrite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enableRandomWrite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enableRandomWrite on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.enableRandomWrite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useDynamicScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useDynamicScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useDynamicScale on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.useDynamicScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_colorBuffer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colorBuffer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colorBuffer on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.colorBuffer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_depthBuffer(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depthBuffer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depthBuffer on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.depthBuffer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_active(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderTexture.active);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_descriptor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name descriptor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index descriptor on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.descriptor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_width(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index width on a nil value");
			}
		}

		obj.width = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_height(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}

		obj.height = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vrUsage(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vrUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vrUsage on a nil value");
			}
		}

		obj.vrUsage = (VRTextureUsage)LuaScriptMgr.GetNetObject(L, 3, typeof(VRTextureUsage));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_depth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depth on a nil value");
			}
		}

		obj.depth = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isPowerOfTwo(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPowerOfTwo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPowerOfTwo on a nil value");
			}
		}

		obj.isPowerOfTwo = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_format(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name format");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index format on a nil value");
			}
		}

		obj.format = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 3, typeof(RenderTextureFormat));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useMipMap(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useMipMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useMipMap on a nil value");
			}
		}

		obj.useMipMap = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autoGenerateMips(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name autoGenerateMips");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index autoGenerateMips on a nil value");
			}
		}

		obj.autoGenerateMips = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dimension(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dimension");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dimension on a nil value");
			}
		}

		obj.dimension = (UnityEngine.Rendering.TextureDimension)LuaScriptMgr.GetNetObject(L, 3, typeof(UnityEngine.Rendering.TextureDimension));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_volumeDepth(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volumeDepth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volumeDepth on a nil value");
			}
		}

		obj.volumeDepth = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_memorylessMode(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name memorylessMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index memorylessMode on a nil value");
			}
		}

		obj.memorylessMode = (RenderTextureMemoryless)LuaScriptMgr.GetNetObject(L, 3, typeof(RenderTextureMemoryless));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_antiAliasing(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name antiAliasing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index antiAliasing on a nil value");
			}
		}

		obj.antiAliasing = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bindTextureMS(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bindTextureMS");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bindTextureMS on a nil value");
			}
		}

		obj.bindTextureMS = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enableRandomWrite(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enableRandomWrite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enableRandomWrite on a nil value");
			}
		}

		obj.enableRandomWrite = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useDynamicScale(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useDynamicScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useDynamicScale on a nil value");
			}
		}

		obj.useDynamicScale = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_active(IntPtr L)
	{
		RenderTexture.active = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 3, typeof(RenderTexture));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_descriptor(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture obj = (RenderTexture)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name descriptor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index descriptor on a nil value");
			}
		}

		obj.descriptor = (RenderTextureDescriptor)LuaScriptMgr.GetNetObject(L, 3, typeof(RenderTextureDescriptor));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTemporary(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			RenderTextureDescriptor arg0 = (RenderTextureDescriptor)LuaScriptMgr.GetNetObject(L, 1, typeof(RenderTextureDescriptor));
			RenderTexture o = RenderTexture.GetTemporary(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite arg4 = (RenderTextureReadWrite)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite arg4 = (RenderTextureReadWrite)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1,arg2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite arg4 = (RenderTextureReadWrite)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			RenderTextureMemoryless arg6 = (RenderTextureMemoryless)LuaScriptMgr.GetNetObject(L, 7, typeof(RenderTextureMemoryless));
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 8)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite arg4 = (RenderTextureReadWrite)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			RenderTextureMemoryless arg6 = (RenderTextureMemoryless)LuaScriptMgr.GetNetObject(L, 7, typeof(RenderTextureMemoryless));
			VRTextureUsage arg7 = (VRTextureUsage)LuaScriptMgr.GetNetObject(L, 8, typeof(VRTextureUsage));
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 9)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat arg3 = (RenderTextureFormat)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite arg4 = (RenderTextureReadWrite)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			RenderTextureMemoryless arg6 = (RenderTextureMemoryless)LuaScriptMgr.GetNetObject(L, 7, typeof(RenderTextureMemoryless));
			VRTextureUsage arg7 = (VRTextureUsage)LuaScriptMgr.GetNetObject(L, 8, typeof(VRTextureUsage));
			bool arg8 = LuaScriptMgr.GetBoolean(L, 9);
			RenderTexture o = RenderTexture.GetTemporary(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RenderTexture.GetTemporary");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReleaseTemporary(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture arg0 = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 1, typeof(RenderTexture));
		RenderTexture.ReleaseTemporary(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResolveAntiAliasedSurface(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
			obj.ResolveAntiAliasedSurface();
			return 0;
		}
		else if (count == 2)
		{
			RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
			RenderTexture arg0 = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 2, typeof(RenderTexture));
			obj.ResolveAntiAliasedSurface(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RenderTexture.ResolveAntiAliasedSurface");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Create(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		bool o = obj.Create();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Release(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		obj.Release();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsCreated(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		bool o = obj.IsCreated();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DiscardContents(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
			obj.DiscardContents();
			return 0;
		}
		else if (count == 3)
		{
			RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			obj.DiscardContents(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RenderTexture.DiscardContents");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MarkRestoreExpected(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		obj.MarkRestoreExpected();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GenerateMips(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		obj.GenerateMips();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNativeDepthBufferPtr(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		IntPtr o = obj.GetNativeDepthBufferPtr();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetGlobalShaderProperty(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RenderTexture obj = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.SetGlobalShaderProperty(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SupportsStencil(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture arg0 = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 1, typeof(RenderTexture));
		bool o = RenderTexture.SupportsStencil(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

