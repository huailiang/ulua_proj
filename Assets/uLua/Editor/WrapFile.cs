using UnityEngine;
using System.Collections;
using System;
using LuaInterface;

namespace LuaEditor
{
    public static class WrapFile
    {

        public static BindType[] binds = new BindType[]
        {
        _GT(typeof(object)),
        _GT(typeof(System.Enum)),
        _GT(typeof(IEnumerator)),
        _GT(typeof(System.Delegate)),
        _GT(typeof(Type)).SetBaseName("System.Object"),
        _GT(typeof(UnityEngine.Object)),
        
        //custom    
		_GT(typeof(Util)),
        _GT(typeof(LuaEnumType)),
        _GT(typeof(Debugger)),
        _GT(typeof(DelegateFactory)),
        _GT(typeof(TestLuaDelegate)),
        _GT(typeof(TestDelegateListener)),
        _GT(typeof(TestEventListener)),
        
        //unity                        
        _GT(typeof(Component)),
        _GT(typeof(Behaviour)),
        _GT(typeof(MonoBehaviour)),
        _GT(typeof(GameObject)),
        _GT(typeof(Transform)),

        _GT(typeof(Camera)),
        _GT(typeof(Material)),
        _GT(typeof(Renderer)),
        _GT(typeof(SkinnedMeshRenderer)),

        _GT(typeof(Collider)),
        _GT(typeof(BoxCollider)),
        _GT(typeof(CharacterController)),

        _GT(typeof(Animation)),
        _GT(typeof(AnimationClip)).SetBaseName("UnityEngine.Object"),
        _GT(typeof(Application)),
        _GT(typeof(Screen)),
        _GT(typeof(Time)),

        _GT(typeof(AssetBundle)),
        _GT(typeof(Texture)),
        _GT(typeof(RenderTexture)),
        _GT(typeof(ParticleSystem)),
        };

        public static BindType _GT(Type t)
        {
            return new BindType(t);
        }
    }
}