using System;
using System.Collections.Generic;

public static class LuaBinder
{
	public static List<string> wrapList = new List<string>();
	public static void Bind(IntPtr L, string type = null)
	{
		if (type == null || wrapList.Contains(type)) return;
		wrapList.Add(type); type += "Wrap";
		switch (type) {
			case "AnimationClipWrap": AnimationClipWrap.Register(L); break;
			case "AnimationWrap": AnimationWrap.Register(L); break;
			case "ApplicationWrap": ApplicationWrap.Register(L); break;
			case "AssetBundleWrap": AssetBundleWrap.Register(L); break;
			case "BehaviourWrap": BehaviourWrap.Register(L); break;
			case "BoxColliderWrap": BoxColliderWrap.Register(L); break;
			case "CameraWrap": CameraWrap.Register(L); break;
			case "CharacterControllerWrap": CharacterControllerWrap.Register(L); break;
			case "ColliderWrap": ColliderWrap.Register(L); break;
			case "ComponentWrap": ComponentWrap.Register(L); break;
			case "DelegateWrap": DelegateWrap.Register(L); break;
			case "EnumWrap": EnumWrap.Register(L); break;
			case "GameObjectWrap": GameObjectWrap.Register(L); break;
			case "IEnumeratorWrap": IEnumeratorWrap.Register(L); break;
			case "LuaEnumTypeWrap": LuaEnumTypeWrap.Register(L); break;
			case "LuaInterface_DebuggerWrap": LuaInterface_DebuggerWrap.Register(L); break;
			case "LuaInterface_UtilWrap": LuaInterface_UtilWrap.Register(L); break;
			case "MaterialWrap": MaterialWrap.Register(L); break;
			case "MonoBehaviourWrap": MonoBehaviourWrap.Register(L); break;
			case "ObjectWrap": ObjectWrap.Register(L); break;
			case "ParticleSystemWrap": ParticleSystemWrap.Register(L); break;
			case "RendererWrap": RendererWrap.Register(L); break;
			case "RenderTextureWrap": RenderTextureWrap.Register(L); break;
			case "ScreenWrap": ScreenWrap.Register(L); break;
			case "SkinnedMeshRendererWrap": SkinnedMeshRendererWrap.Register(L); break;
			case "System_ObjectWrap": System_ObjectWrap.Register(L); break;
			case "TextureWrap": TextureWrap.Register(L); break;
			case "TimeWrap": TimeWrap.Register(L); break;
			case "TransformWrap": TransformWrap.Register(L); break;
			case "TypeWrap": TypeWrap.Register(L); break;
		}
	}
}
