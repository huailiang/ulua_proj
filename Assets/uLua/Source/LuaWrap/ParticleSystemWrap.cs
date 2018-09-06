using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;
using Object = UnityEngine.Object;

public class ParticleSystemWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetParticles", SetParticles),
			new LuaMethod("GetParticles", GetParticles),
			new LuaMethod("SetCustomParticleData", SetCustomParticleData),
			new LuaMethod("GetCustomParticleData", GetCustomParticleData),
			new LuaMethod("Simulate", Simulate),
			new LuaMethod("Play", Play),
			new LuaMethod("Pause", Pause),
			new LuaMethod("Stop", Stop),
			new LuaMethod("Clear", Clear),
			new LuaMethod("IsAlive", IsAlive),
			new LuaMethod("Emit", Emit),
			new LuaMethod("New", _CreateParticleSystem),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isPlaying", get_isPlaying, null),
			new LuaField("isEmitting", get_isEmitting, null),
			new LuaField("isStopped", get_isStopped, null),
			new LuaField("isPaused", get_isPaused, null),
			new LuaField("time", get_time, set_time),
			new LuaField("particleCount", get_particleCount, null),
			new LuaField("randomSeed", get_randomSeed, set_randomSeed),
			new LuaField("useAutoRandomSeed", get_useAutoRandomSeed, set_useAutoRandomSeed),
			new LuaField("main", get_main, null),
			new LuaField("emission", get_emission, null),
			new LuaField("shape", get_shape, null),
			new LuaField("velocityOverLifetime", get_velocityOverLifetime, null),
			new LuaField("limitVelocityOverLifetime", get_limitVelocityOverLifetime, null),
			new LuaField("inheritVelocity", get_inheritVelocity, null),
			new LuaField("forceOverLifetime", get_forceOverLifetime, null),
			new LuaField("colorOverLifetime", get_colorOverLifetime, null),
			new LuaField("colorBySpeed", get_colorBySpeed, null),
			new LuaField("sizeOverLifetime", get_sizeOverLifetime, null),
			new LuaField("sizeBySpeed", get_sizeBySpeed, null),
			new LuaField("rotationOverLifetime", get_rotationOverLifetime, null),
			new LuaField("rotationBySpeed", get_rotationBySpeed, null),
			new LuaField("externalForces", get_externalForces, null),
			new LuaField("noise", get_noise, null),
			new LuaField("collision", get_collision, null),
			new LuaField("trigger", get_trigger, null),
			new LuaField("subEmitters", get_subEmitters, null),
			new LuaField("textureSheetAnimation", get_textureSheetAnimation, null),
			new LuaField("lights", get_lights, null),
			new LuaField("trails", get_trails, null),
			new LuaField("customData", get_customData, null),
		};

		LuaScriptMgr.RegisterLib(L, "UnityEngine.ParticleSystem", typeof(ParticleSystem), regs, fields, typeof(Component));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateParticleSystem(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ParticleSystem obj = new ParticleSystem();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.New");
		}

		return 0;
	}

	static Type classType = typeof(ParticleSystem);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPlaying(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPlaying");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPlaying on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isPlaying);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isEmitting(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isEmitting");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isEmitting on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isEmitting);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isStopped(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isStopped");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isStopped on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isStopped);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPaused(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPaused");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPaused on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.isPaused);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_particleCount(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particleCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particleCount on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.particleCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_randomSeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name randomSeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index randomSeed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.randomSeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useAutoRandomSeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useAutoRandomSeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useAutoRandomSeed on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.useAutoRandomSeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_main(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name main");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index main on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.main);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_emission(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name emission");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index emission on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.emission);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shape(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shape");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shape on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.shape);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_velocityOverLifetime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityOverLifetime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityOverLifetime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.velocityOverLifetime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_limitVelocityOverLifetime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name limitVelocityOverLifetime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index limitVelocityOverLifetime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.limitVelocityOverLifetime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_inheritVelocity(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inheritVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inheritVelocity on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.inheritVelocity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_forceOverLifetime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forceOverLifetime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forceOverLifetime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.forceOverLifetime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_colorOverLifetime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colorOverLifetime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colorOverLifetime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.colorOverLifetime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_colorBySpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colorBySpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colorBySpeed on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.colorBySpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sizeOverLifetime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sizeOverLifetime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sizeOverLifetime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.sizeOverLifetime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sizeBySpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sizeBySpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sizeBySpeed on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.sizeBySpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotationOverLifetime(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotationOverLifetime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotationOverLifetime on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rotationOverLifetime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotationBySpeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotationBySpeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotationBySpeed on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.rotationBySpeed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_externalForces(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name externalForces");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index externalForces on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.externalForces);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_noise(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name noise");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index noise on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.noise);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_collision(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name collision");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index collision on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.collision);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_trigger(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trigger");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trigger on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.trigger);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_subEmitters(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name subEmitters");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index subEmitters on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.subEmitters);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_textureSheetAnimation(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name textureSheetAnimation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index textureSheetAnimation on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.textureSheetAnimation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lights(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lights");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lights on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.lights);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_trails(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name trails");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index trails on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.trails);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_customData(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name customData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index customData on a nil value");
			}
		}

		LuaScriptMgr.PushValue(L, obj.customData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_time(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}

		obj.time = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_randomSeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name randomSeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index randomSeed on a nil value");
			}
		}

		obj.randomSeed = (uint)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useAutoRandomSeed(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleSystem obj = (ParticleSystem)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useAutoRandomSeed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useAutoRandomSeed on a nil value");
			}
		}

		obj.useAutoRandomSeed = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetParticles(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
		ParticleSystem.Particle[] objs0 = LuaScriptMgr.GetArrayObject<ParticleSystem.Particle>(L, 2);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
		obj.SetParticles(objs0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetParticles(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
		ParticleSystem.Particle[] objs0 = LuaScriptMgr.GetArrayObject<ParticleSystem.Particle>(L, 2);
		int o = obj.GetParticles(objs0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCustomParticleData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
		List<Vector4> arg0 = (List<Vector4>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<Vector4>));
		ParticleSystemCustomData arg1 = (ParticleSystemCustomData)LuaScriptMgr.GetNetObject(L, 3, typeof(ParticleSystemCustomData));
		obj.SetCustomParticleData(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCustomParticleData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
		List<Vector4> arg0 = (List<Vector4>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<Vector4>));
		ParticleSystemCustomData arg1 = (ParticleSystemCustomData)LuaScriptMgr.GetNetObject(L, 3, typeof(ParticleSystemCustomData));
		int o = obj.GetCustomParticleData(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Simulate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			obj.Simulate(arg0);
			return 0;
		}
		else if (count == 3)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			obj.Simulate(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
			obj.Simulate(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 5)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
			bool arg1 = LuaScriptMgr.GetBoolean(L, 3);
			bool arg2 = LuaScriptMgr.GetBoolean(L, 4);
			bool arg3 = LuaScriptMgr.GetBoolean(L, 5);
			obj.Simulate(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.Simulate");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			obj.Play();
			return 0;
		}
		else if (count == 2)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			obj.Play(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.Play");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Pause(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			obj.Pause();
			return 0;
		}
		else if (count == 2)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			obj.Pause(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.Pause");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Stop(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			obj.Stop();
			return 0;
		}
		else if (count == 2)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			obj.Stop(arg0);
			return 0;
		}
		else if (count == 3)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			ParticleSystemStopBehavior arg1 = (ParticleSystemStopBehavior)LuaScriptMgr.GetNetObject(L, 3, typeof(ParticleSystemStopBehavior));
			obj.Stop(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.Stop");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			obj.Clear();
			return 0;
		}
		else if (count == 2)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			obj.Clear(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.Clear");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsAlive(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			bool o = obj.IsAlive();
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			bool arg0 = LuaScriptMgr.GetBoolean(L, 2);
			bool o = obj.IsAlive(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.IsAlive");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Emit(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 2);
			obj.Emit(arg0);
			return 0;
		}
		else if (count == 3)
		{
			ParticleSystem obj = (ParticleSystem)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleSystem");
			ParticleSystem.EmitParams arg0 = (ParticleSystem.EmitParams)LuaScriptMgr.GetNetObject(L, 2, typeof(ParticleSystem.EmitParams));
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 3);
			obj.Emit(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleSystem.Emit");
		}

		return 0;
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

