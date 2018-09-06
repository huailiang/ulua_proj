using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class QualitySettingsWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetQualityLevel", GetQualityLevel),
			new LuaMethod("SetQualityLevel", SetQualityLevel),
			new LuaMethod("IncreaseLevel", IncreaseLevel),
			new LuaMethod("DecreaseLevel", DecreaseLevel),
			new LuaMethod("New", _CreateQualitySettings),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("names", get_names, null),
			new LuaField("shadowCascade4Split", get_shadowCascade4Split, set_shadowCascade4Split),
			new LuaField("anisotropicFiltering", get_anisotropicFiltering, set_anisotropicFiltering),
			new LuaField("maxQueuedFrames", get_maxQueuedFrames, set_maxQueuedFrames),
			new LuaField("blendWeights", get_blendWeights, set_blendWeights),
			new LuaField("pixelLightCount", get_pixelLightCount, set_pixelLightCount),
			new LuaField("shadows", get_shadows, set_shadows),
			new LuaField("shadowProjection", get_shadowProjection, set_shadowProjection),
			new LuaField("shadowCascades", get_shadowCascades, set_shadowCascades),
			new LuaField("shadowDistance", get_shadowDistance, set_shadowDistance),
			new LuaField("shadowResolution", get_shadowResolution, set_shadowResolution),
			new LuaField("shadowmaskMode", get_shadowmaskMode, set_shadowmaskMode),
			new LuaField("shadowNearPlaneOffset", get_shadowNearPlaneOffset, set_shadowNearPlaneOffset),
			new LuaField("shadowCascade2Split", get_shadowCascade2Split, set_shadowCascade2Split),
			new LuaField("lodBias", get_lodBias, set_lodBias),
			new LuaField("masterTextureLimit", get_masterTextureLimit, set_masterTextureLimit),
			new LuaField("maximumLODLevel", get_maximumLODLevel, set_maximumLODLevel),
			new LuaField("particleRaycastBudget", get_particleRaycastBudget, set_particleRaycastBudget),
			new LuaField("softParticles", get_softParticles, set_softParticles),
			new LuaField("softVegetation", get_softVegetation, set_softVegetation),
			new LuaField("vSyncCount", get_vSyncCount, set_vSyncCount),
			new LuaField("antiAliasing", get_antiAliasing, set_antiAliasing),
			new LuaField("asyncUploadTimeSlice", get_asyncUploadTimeSlice, set_asyncUploadTimeSlice),
			new LuaField("asyncUploadBufferSize", get_asyncUploadBufferSize, set_asyncUploadBufferSize),
			new LuaField("realtimeReflectionProbes", get_realtimeReflectionProbes, set_realtimeReflectionProbes),
			new LuaField("billboardsFaceCameraPosition", get_billboardsFaceCameraPosition, set_billboardsFaceCameraPosition),
			new LuaField("resolutionScalingFixedDPIFactor", get_resolutionScalingFixedDPIFactor, set_resolutionScalingFixedDPIFactor),
			new LuaField("desiredColorSpace", get_desiredColorSpace, null),
			new LuaField("activeColorSpace", get_activeColorSpace, null),
		};

		LuaScriptMgr.RegisterLib(L, "UnityEngine.QualitySettings", typeof(QualitySettings), regs, fields, typeof(Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateQualitySettings(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			QualitySettings obj = new QualitySettings();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.New");
		}

		return 0;
	}

	static Type classType = typeof(QualitySettings);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_names(IntPtr L)
	{
		LuaScriptMgr.PushArray(L, QualitySettings.names);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowCascade4Split(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowCascade4Split);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_anisotropicFiltering(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.anisotropicFiltering);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxQueuedFrames(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.maxQueuedFrames);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_blendWeights(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.blendWeights);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelLightCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.pixelLightCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadows(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadows);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowProjection(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowProjection);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowCascades(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowCascades);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowDistance(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowResolution(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowResolution);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowmaskMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowmaskMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowNearPlaneOffset(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowNearPlaneOffset);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_shadowCascade2Split(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowCascade2Split);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lodBias(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.lodBias);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_masterTextureLimit(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.masterTextureLimit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maximumLODLevel(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.maximumLODLevel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_particleRaycastBudget(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.particleRaycastBudget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_softParticles(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.softParticles);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_softVegetation(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.softVegetation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vSyncCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.vSyncCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_antiAliasing(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.antiAliasing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_asyncUploadTimeSlice(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.asyncUploadTimeSlice);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_asyncUploadBufferSize(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.asyncUploadBufferSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_realtimeReflectionProbes(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.realtimeReflectionProbes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_billboardsFaceCameraPosition(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.billboardsFaceCameraPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_resolutionScalingFixedDPIFactor(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.resolutionScalingFixedDPIFactor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_desiredColorSpace(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.desiredColorSpace);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activeColorSpace(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.activeColorSpace);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowCascade4Split(IntPtr L)
	{
		QualitySettings.shadowCascade4Split = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_anisotropicFiltering(IntPtr L)
	{
		QualitySettings.anisotropicFiltering = (AnisotropicFiltering)LuaScriptMgr.GetNetObject(L, 3, typeof(AnisotropicFiltering));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxQueuedFrames(IntPtr L)
	{
		QualitySettings.maxQueuedFrames = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_blendWeights(IntPtr L)
	{
		QualitySettings.blendWeights = (BlendWeights)LuaScriptMgr.GetNetObject(L, 3, typeof(BlendWeights));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pixelLightCount(IntPtr L)
	{
		QualitySettings.pixelLightCount = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadows(IntPtr L)
	{
		QualitySettings.shadows = (ShadowQuality)LuaScriptMgr.GetNetObject(L, 3, typeof(ShadowQuality));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowProjection(IntPtr L)
	{
		QualitySettings.shadowProjection = (ShadowProjection)LuaScriptMgr.GetNetObject(L, 3, typeof(ShadowProjection));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowCascades(IntPtr L)
	{
		QualitySettings.shadowCascades = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowDistance(IntPtr L)
	{
		QualitySettings.shadowDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowResolution(IntPtr L)
	{
		QualitySettings.shadowResolution = (ShadowResolution)LuaScriptMgr.GetNetObject(L, 3, typeof(ShadowResolution));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowmaskMode(IntPtr L)
	{
		QualitySettings.shadowmaskMode = (ShadowmaskMode)LuaScriptMgr.GetNetObject(L, 3, typeof(ShadowmaskMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowNearPlaneOffset(IntPtr L)
	{
		QualitySettings.shadowNearPlaneOffset = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_shadowCascade2Split(IntPtr L)
	{
		QualitySettings.shadowCascade2Split = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_lodBias(IntPtr L)
	{
		QualitySettings.lodBias = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_masterTextureLimit(IntPtr L)
	{
		QualitySettings.masterTextureLimit = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maximumLODLevel(IntPtr L)
	{
		QualitySettings.maximumLODLevel = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_particleRaycastBudget(IntPtr L)
	{
		QualitySettings.particleRaycastBudget = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_softParticles(IntPtr L)
	{
		QualitySettings.softParticles = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_softVegetation(IntPtr L)
	{
		QualitySettings.softVegetation = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vSyncCount(IntPtr L)
	{
		QualitySettings.vSyncCount = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_antiAliasing(IntPtr L)
	{
		QualitySettings.antiAliasing = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_asyncUploadTimeSlice(IntPtr L)
	{
		QualitySettings.asyncUploadTimeSlice = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_asyncUploadBufferSize(IntPtr L)
	{
		QualitySettings.asyncUploadBufferSize = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_realtimeReflectionProbes(IntPtr L)
	{
		QualitySettings.realtimeReflectionProbes = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_billboardsFaceCameraPosition(IntPtr L)
	{
		QualitySettings.billboardsFaceCameraPosition = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_resolutionScalingFixedDPIFactor(IntPtr L)
	{
		QualitySettings.resolutionScalingFixedDPIFactor = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetQualityLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		int o = QualitySettings.GetQualityLevel();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetQualityLevel(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			QualitySettings.SetQualityLevel(arg0);
			return 0;
		}
		else if (count == 2)
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			bool arg1 = LuaScriptMgr.GetBoolean(L, 2);
			QualitySettings.SetQualityLevel(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.SetQualityLevel");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IncreaseLevel(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			QualitySettings.IncreaseLevel();
			return 0;
		}
		else if (count == 1)
		{
			bool arg0 = LuaScriptMgr.GetBoolean(L, 1);
			QualitySettings.IncreaseLevel(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.IncreaseLevel");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DecreaseLevel(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			QualitySettings.DecreaseLevel();
			return 0;
		}
		else if (count == 1)
		{
			bool arg0 = LuaScriptMgr.GetBoolean(L, 1);
			QualitySettings.DecreaseLevel(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.DecreaseLevel");
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

