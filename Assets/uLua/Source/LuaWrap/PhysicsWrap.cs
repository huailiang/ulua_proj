using System;
using UnityEngine;
using LuaInterface;

public class PhysicsWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Raycast", Raycast),
			new LuaMethod("RaycastAll", RaycastAll),
			new LuaMethod("RaycastNonAlloc", RaycastNonAlloc),
			new LuaMethod("Linecast", Linecast),
			new LuaMethod("OverlapSphere", OverlapSphere),
			new LuaMethod("OverlapSphereNonAlloc", OverlapSphereNonAlloc),
			new LuaMethod("OverlapCapsule", OverlapCapsule),
			new LuaMethod("OverlapCapsuleNonAlloc", OverlapCapsuleNonAlloc),
			new LuaMethod("CapsuleCast", CapsuleCast),
			new LuaMethod("SphereCast", SphereCast),
			new LuaMethod("CapsuleCastAll", CapsuleCastAll),
			new LuaMethod("CapsuleCastNonAlloc", CapsuleCastNonAlloc),
			new LuaMethod("SphereCastAll", SphereCastAll),
			new LuaMethod("SphereCastNonAlloc", SphereCastNonAlloc),
			new LuaMethod("CheckSphere", CheckSphere),
			new LuaMethod("CheckCapsule", CheckCapsule),
			new LuaMethod("CheckBox", CheckBox),
			new LuaMethod("OverlapBox", OverlapBox),
			new LuaMethod("OverlapBoxNonAlloc", OverlapBoxNonAlloc),
			new LuaMethod("BoxCastAll", BoxCastAll),
			new LuaMethod("BoxCastNonAlloc", BoxCastNonAlloc),
			new LuaMethod("BoxCast", BoxCast),
			new LuaMethod("IgnoreCollision", IgnoreCollision),
			new LuaMethod("IgnoreLayerCollision", IgnoreLayerCollision),
			new LuaMethod("GetIgnoreLayerCollision", GetIgnoreLayerCollision),
			new LuaMethod("ComputePenetration", ComputePenetration),
			new LuaMethod("ClosestPoint", ClosestPoint),
			new LuaMethod("Simulate", Simulate),
			new LuaMethod("SyncTransforms", SyncTransforms),
			new LuaMethod("RebuildBroadphaseRegions", RebuildBroadphaseRegions),
			new LuaMethod("New", _CreatePhysics),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("IgnoreRaycastLayer", get_IgnoreRaycastLayer, null),
			new LuaField("DefaultRaycastLayers", get_DefaultRaycastLayers, null),
			new LuaField("AllLayers", get_AllLayers, null),
			new LuaField("gravity", get_gravity, set_gravity),
			new LuaField("defaultContactOffset", get_defaultContactOffset, set_defaultContactOffset),
			new LuaField("bounceThreshold", get_bounceThreshold, set_bounceThreshold),
			new LuaField("defaultSolverIterations", get_defaultSolverIterations, set_defaultSolverIterations),
			new LuaField("defaultSolverVelocityIterations", get_defaultSolverVelocityIterations, set_defaultSolverVelocityIterations),
			new LuaField("sleepThreshold", get_sleepThreshold, set_sleepThreshold),
			new LuaField("queriesHitTriggers", get_queriesHitTriggers, set_queriesHitTriggers),
			new LuaField("queriesHitBackfaces", get_queriesHitBackfaces, set_queriesHitBackfaces),
			new LuaField("interCollisionDistance", get_interCollisionDistance, set_interCollisionDistance),
			new LuaField("interCollisionStiffness", get_interCollisionStiffness, set_interCollisionStiffness),
			new LuaField("interCollisionSettingsToggle", get_interCollisionSettingsToggle, set_interCollisionSettingsToggle),
			new LuaField("autoSimulation", get_autoSimulation, set_autoSimulation),
			new LuaField("autoSyncTransforms", get_autoSyncTransforms, set_autoSyncTransforms),
		};

		LuaScriptMgr.RegisterLib(L, "UnityEngine.Physics", typeof(Physics), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePhysics(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Physics obj = new Physics();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.New");
		}

		return 0;
	}

	static Type classType = typeof(Physics);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IgnoreRaycastLayer(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.IgnoreRaycastLayer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DefaultRaycastLayers(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.DefaultRaycastLayers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AllLayers(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.AllLayers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gravity(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.gravity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultContactOffset(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultContactOffset);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bounceThreshold(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.bounceThreshold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultSolverIterations(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultSolverIterations);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultSolverVelocityIterations(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultSolverVelocityIterations);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sleepThreshold(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.sleepThreshold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_queriesHitTriggers(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.queriesHitTriggers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_queriesHitBackfaces(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.queriesHitBackfaces);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_interCollisionDistance(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.interCollisionDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_interCollisionStiffness(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.interCollisionStiffness);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_interCollisionSettingsToggle(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.interCollisionSettingsToggle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autoSimulation(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.autoSimulation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autoSyncTransforms(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.autoSyncTransforms);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gravity(IntPtr L)
	{
		Physics.gravity = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defaultContactOffset(IntPtr L)
	{
		Physics.defaultContactOffset = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bounceThreshold(IntPtr L)
	{
		Physics.bounceThreshold = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defaultSolverIterations(IntPtr L)
	{
		Physics.defaultSolverIterations = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defaultSolverVelocityIterations(IntPtr L)
	{
		Physics.defaultSolverVelocityIterations = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sleepThreshold(IntPtr L)
	{
		Physics.sleepThreshold = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_queriesHitTriggers(IntPtr L)
	{
		Physics.queriesHitTriggers = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_queriesHitBackfaces(IntPtr L)
	{
		Physics.queriesHitBackfaces = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_interCollisionDistance(IntPtr L)
	{
		Physics.interCollisionDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_interCollisionStiffness(IntPtr L)
	{
		Physics.interCollisionStiffness = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_interCollisionSettingsToggle(IntPtr L)
	{
		Physics.interCollisionSettingsToggle = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autoSimulation(IntPtr L)
	{
		Physics.autoSimulation = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autoSyncTransforms(IntPtr L)
	{
		Physics.autoSyncTransforms = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Raycast(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			bool o = Physics.Raycast(arg0);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit arg1;
			bool o = Physics.Raycast(arg0,out arg1);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			return 2;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			bool o = Physics.Raycast(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			bool o = Physics.Raycast(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
			bool o = Physics.Raycast(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit arg2;
			bool o = Physics.Raycast(arg0,arg1,out arg2);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			bool o = Physics.Raycast(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit arg1;
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			bool o = Physics.Raycast(arg0,out arg1,arg2);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			return 2;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction arg3 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 4);
			bool o = Physics.Raycast(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			bool o = Physics.Raycast(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit arg2;
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			bool o = Physics.Raycast(arg0,arg1,out arg2,arg3);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit arg1;
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			bool o = Physics.Raycast(arg0,out arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			return 2;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 5);
			bool o = Physics.Raycast(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit arg2;
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
			bool o = Physics.Raycast(arg0,arg1,out arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit arg1;
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 5);
			bool o = Physics.Raycast(arg0,out arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg1);
			return 2;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit arg2;
			float arg3 = (float)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction arg5 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction));
			bool o = Physics.Raycast(arg0,arg1,out arg2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.Raycast");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RaycastAll(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] o = Physics.RaycastAll(arg0);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] o = Physics.RaycastAll(arg0,arg1);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] o = Physics.RaycastAll(arg0,arg1);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o = Physics.RaycastAll(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o = Physics.RaycastAll(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o = Physics.RaycastAll(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction arg3 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 4);
			RaycastHit[] o = Physics.RaycastAll(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			RaycastHit[] o = Physics.RaycastAll(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.RaycastAll");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RaycastNonAlloc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] objs1 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			int o = Physics.RaycastNonAlloc(arg0,objs1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(RaycastHit[])))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			int o = Physics.RaycastNonAlloc(arg0,arg1,objs2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(RaycastHit[]), typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] objs1 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int o = Physics.RaycastNonAlloc(arg0,objs1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(RaycastHit[]), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int o = Physics.RaycastNonAlloc(arg0,arg1,objs2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] objs1 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			int o = Physics.RaycastNonAlloc(arg0,objs1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
			int o = Physics.RaycastNonAlloc(arg0,arg1,objs2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] objs1 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 5);
			int o = Physics.RaycastNonAlloc(arg0,objs1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float arg3 = (float)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction arg5 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction));
			int o = Physics.RaycastNonAlloc(arg0,arg1,objs2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.RaycastNonAlloc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Linecast(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			bool o = Physics.Linecast(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit arg2;
			bool o = Physics.Linecast(arg0,arg1,out arg2);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
			bool o = Physics.Linecast(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit arg2;
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			bool o = Physics.Linecast(arg0,arg1,out arg2,arg3);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			int arg2 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction arg3 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 4);
			bool o = Physics.Linecast(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit arg2;
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			bool o = Physics.Linecast(arg0,arg1,out arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.Linecast");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapSphere(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] o = Physics.OverlapSphere(arg0,arg1);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			Collider[] o = Physics.OverlapSphere(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			QueryTriggerInteraction arg3 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 4, typeof(QueryTriggerInteraction));
			Collider[] o = Physics.OverlapSphere(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapSphere");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapSphereNonAlloc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] objs2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int o = Physics.OverlapSphereNonAlloc(arg0,arg1,objs2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] objs2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			int o = Physics.OverlapSphereNonAlloc(arg0,arg1,objs2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] objs2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			int o = Physics.OverlapSphereNonAlloc(arg0,arg1,objs2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapSphereNonAlloc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapCapsule(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] o = Physics.OverlapCapsule(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Collider[] o = Physics.OverlapCapsule(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			Collider[] o = Physics.OverlapCapsule(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapCapsule");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapCapsuleNonAlloc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] objs3 = LuaScriptMgr.GetArrayObject<Collider>(L, 4);
			int o = Physics.OverlapCapsuleNonAlloc(arg0,arg1,arg2,objs3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] objs3 = LuaScriptMgr.GetArrayObject<Collider>(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int o = Physics.OverlapCapsuleNonAlloc(arg0,arg1,arg2,objs3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Collider[] objs3 = LuaScriptMgr.GetArrayObject<Collider>(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction arg5 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction));
			int o = Physics.OverlapCapsuleNonAlloc(arg0,arg1,arg2,objs3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapCapsuleNonAlloc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CapsuleCast(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit arg4;
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3,out arg4);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg4);
			return 2;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit arg4;
			float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3,out arg4,arg5);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg4);
			return 2;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
			QueryTriggerInteraction arg6 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 7);
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit arg4;
			float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
			int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3,out arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg4);
			return 2;
		}
		else if (count == 8)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit arg4;
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction arg7 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction));
			bool o = Physics.CapsuleCast(arg0,arg1,arg2,arg3,out arg4,arg5,arg6,arg7);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg4);
			return 2;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCast");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SphereCast(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			bool o = Physics.SphereCast(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			bool o = Physics.SphereCast(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit arg2;
			bool o = Physics.SphereCast(arg0,arg1,out arg2);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			bool o = Physics.SphereCast(arg0,arg1,arg2,out arg3);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			bool o = Physics.SphereCast(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit arg2;
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			bool o = Physics.SphereCast(arg0,arg1,out arg2,arg3);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit arg2;
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
			bool o = Physics.SphereCast(arg0,arg1,out arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 5);
			bool o = Physics.SphereCast(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			bool o = Physics.SphereCast(arg0,arg1,arg2,out arg3,arg4);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
			bool o = Physics.SphereCast(arg0,arg1,arg2,out arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit arg2;
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
			QueryTriggerInteraction arg5 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 6);
			bool o = Physics.SphereCast(arg0,arg1,out arg2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg2);
			return 2;
		}
		else if (count == 7)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction arg6 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction));
			bool o = Physics.SphereCast(arg0,arg1,arg2,out arg3,arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCast");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CapsuleCastAll(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] o = Physics.CapsuleCastAll(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			RaycastHit[] o = Physics.CapsuleCastAll(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			RaycastHit[] o = Physics.CapsuleCastAll(arg0,arg1,arg2,arg3,arg4,arg5);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 7)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction arg6 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction));
			RaycastHit[] o = Physics.CapsuleCastAll(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCastAll");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CapsuleCastNonAlloc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] objs4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			int o = Physics.CapsuleCastNonAlloc(arg0,arg1,arg2,arg3,objs4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] objs4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int o = Physics.CapsuleCastNonAlloc(arg0,arg1,arg2,arg3,objs4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] objs4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			int o = Physics.CapsuleCastNonAlloc(arg0,arg1,arg2,arg3,objs4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 8)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 arg3 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] objs4 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 5);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction arg7 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction));
			int o = Physics.CapsuleCastNonAlloc(arg0,arg1,arg2,arg3,objs4,arg5,arg6,arg7);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCastNonAlloc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SphereCastAll(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			float arg2 = (float)LuaDLL.lua_tonumber(L, 3);
			int arg3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 5);
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			float arg3 = (float)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction arg5 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction));
			RaycastHit[] o = Physics.SphereCastAll(arg0,arg1,arg2,arg3,arg4,arg5);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCastAll");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SphereCastNonAlloc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			int o = Physics.SphereCastNonAlloc(arg0,arg1,objs2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(RaycastHit[]), typeof(float)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int o = Physics.SphereCastNonAlloc(arg0,arg1,objs2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(RaycastHit[])))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			int o = Physics.SphereCastNonAlloc(arg0,arg1,arg2,objs3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
			int o = Physics.SphereCastNonAlloc(arg0,arg1,objs2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(RaycastHit[]), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			int o = Physics.SphereCastNonAlloc(arg0,arg1,arg2,objs3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(RaycastHit[]), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
			int o = Physics.SphereCastNonAlloc(arg0,arg1,arg2,objs3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(RaycastHit[]), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray arg0 = LuaScriptMgr.GetRay(L, 1);
			float arg1 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] objs2 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 3);
			float arg3 = (float)LuaDLL.lua_tonumber(L, 4);
			int arg4 = (int)LuaDLL.lua_tonumber(L, 5);
			QueryTriggerInteraction arg5 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 6);
			int o = Physics.SphereCastNonAlloc(arg0,arg1,objs2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction arg6 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction));
			int o = Physics.SphereCastNonAlloc(arg0,arg1,arg2,objs3,arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCastNonAlloc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckSphere(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			bool o = Physics.CheckSphere(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			bool o = Physics.CheckSphere(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			float arg1 = (float)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			QueryTriggerInteraction arg3 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 4, typeof(QueryTriggerInteraction));
			bool o = Physics.CheckSphere(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckSphere");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckCapsule(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			bool o = Physics.CheckCapsule(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			bool o = Physics.CheckCapsule(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			float arg2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			bool o = Physics.CheckCapsule(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckCapsule");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CheckBox(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			bool o = Physics.CheckBox(arg0,arg1);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			bool o = Physics.CheckBox(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			bool o = Physics.CheckBox(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			bool o = Physics.CheckBox(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckBox");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapBox(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] o = Physics.OverlapBox(arg0,arg1);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			Collider[] o = Physics.OverlapBox(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Collider[] o = Physics.OverlapBox(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction arg4 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			Collider[] o = Physics.OverlapBox(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapBox");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OverlapBoxNonAlloc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] objs2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			int o = Physics.OverlapBoxNonAlloc(arg0,arg1,objs2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] objs2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			int o = Physics.OverlapBoxNonAlloc(arg0,arg1,objs2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] objs2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int o = Physics.OverlapBoxNonAlloc(arg0,arg1,objs2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Collider[] objs2 = LuaScriptMgr.GetArrayObject<Collider>(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction arg5 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction));
			int o = Physics.OverlapBoxNonAlloc(arg0,arg1,objs2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapBoxNonAlloc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BoxCastAll(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] o = Physics.BoxCastAll(arg0,arg1,arg2);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			RaycastHit[] o = Physics.BoxCastAll(arg0,arg1,arg2,arg3);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			RaycastHit[] o = Physics.BoxCastAll(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			RaycastHit[] o = Physics.BoxCastAll(arg0,arg1,arg2,arg3,arg4,arg5);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else if (count == 7)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			float arg4 = (float)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction arg6 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction));
			RaycastHit[] o = Physics.BoxCastAll(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.BoxCastAll");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BoxCastNonAlloc(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			int o = Physics.BoxCastNonAlloc(arg0,arg1,arg2,objs3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			int o = Physics.BoxCastNonAlloc(arg0,arg1,arg2,objs3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 6)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int o = Physics.BoxCastNonAlloc(arg0,arg1,arg2,objs3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			int o = Physics.BoxCastNonAlloc(arg0,arg1,arg2,objs3,arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 8)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] objs3 = LuaScriptMgr.GetArrayObject<RaycastHit>(L, 4);
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction arg7 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction));
			int o = Physics.BoxCastNonAlloc(arg0,arg1,arg2,objs3,arg4,arg5,arg6,arg7);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.BoxCastNonAlloc");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BoxCast(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			bool o = Physics.BoxCast(arg0,arg1,arg2);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			bool o = Physics.BoxCast(arg0,arg1,arg2,out arg3);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else if (count == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			bool o = Physics.BoxCast(arg0,arg1,arg2,arg3);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			bool o = Physics.BoxCast(arg0,arg1,arg2,arg3,arg4);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null, typeof(LuaTable)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			bool o = Physics.BoxCast(arg0,arg1,arg2,out arg3,arg4);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null, typeof(LuaTable), typeof(float)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
			bool o = Physics.BoxCast(arg0,arg1,arg2,out arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
			bool o = Physics.BoxCast(arg0,arg1,arg2,arg3,arg4,arg5);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
			float arg4 = (float)LuaDLL.lua_tonumber(L, 5);
			int arg5 = (int)LuaDLL.lua_tonumber(L, 6);
			QueryTriggerInteraction arg6 = (QueryTriggerInteraction)LuaScriptMgr.GetLuaObject(L, 7);
			bool o = Physics.BoxCast(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			return 1;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(LuaTable), null, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			float arg5 = (float)LuaDLL.lua_tonumber(L, 6);
			int arg6 = (int)LuaDLL.lua_tonumber(L, 7);
			bool o = Physics.BoxCast(arg0,arg1,arg2,out arg3,arg4,arg5,arg6);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else if (count == 8)
		{
			Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit arg3;
			Quaternion arg4 = LuaScriptMgr.GetQuaternion(L, 5);
			float arg5 = (float)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction arg7 = (QueryTriggerInteraction)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction));
			bool o = Physics.BoxCast(arg0,arg1,arg2,out arg3,arg4,arg5,arg6,arg7);
			LuaScriptMgr.Push(L, o);
			LuaScriptMgr.Push(L, arg3);
			return 2;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.BoxCast");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IgnoreCollision(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Collider arg0 = (Collider)LuaScriptMgr.GetUnityObject(L, 1, typeof(Collider));
			Collider arg1 = (Collider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Collider));
			Physics.IgnoreCollision(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			Collider arg0 = (Collider)LuaScriptMgr.GetUnityObject(L, 1, typeof(Collider));
			Collider arg1 = (Collider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Collider));
			bool arg2 = LuaScriptMgr.GetBoolean(L, 3);
			Physics.IgnoreCollision(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.IgnoreCollision");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IgnoreLayerCollision(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			Physics.IgnoreLayerCollision(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			bool arg2 = LuaScriptMgr.GetBoolean(L, 3);
			Physics.IgnoreLayerCollision(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.IgnoreLayerCollision");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIgnoreLayerCollision(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		bool o = Physics.GetIgnoreLayerCollision(arg0,arg1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ComputePenetration(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 8);
		Collider arg0 = (Collider)LuaScriptMgr.GetUnityObject(L, 1, typeof(Collider));
		Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
		Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
		Collider arg3 = (Collider)LuaScriptMgr.GetUnityObject(L, 4, typeof(Collider));
		Vector3 arg4 = LuaScriptMgr.GetVector3(L, 5);
		Quaternion arg5 = LuaScriptMgr.GetQuaternion(L, 6);
		Vector3 arg6;
		float arg7;
		bool o = Physics.ComputePenetration(arg0,arg1,arg2,arg3,arg4,arg5,out arg6,out arg7);
		LuaScriptMgr.Push(L, o);
		LuaScriptMgr.Push(L, arg6);
		LuaScriptMgr.Push(L, arg7);
		return 3;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClosestPoint(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Vector3 arg0 = LuaScriptMgr.GetVector3(L, 1);
		Collider arg1 = (Collider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Collider));
		Vector3 arg2 = LuaScriptMgr.GetVector3(L, 3);
		Quaternion arg3 = LuaScriptMgr.GetQuaternion(L, 4);
		Vector3 o = Physics.ClosestPoint(arg0,arg1,arg2,arg3);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Simulate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 1);
		Physics.Simulate(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SyncTransforms(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Physics.SyncTransforms();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RebuildBroadphaseRegions(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Bounds arg0 = LuaScriptMgr.GetBounds(L, 1);
		int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
		Physics.RebuildBroadphaseRegions(arg0,arg1);
		return 0;
	}
}

