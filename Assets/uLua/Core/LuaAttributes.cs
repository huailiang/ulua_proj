using System;

namespace LuaInterface
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HotfixAttribute : System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class HotfixIgnoreAttribute : System.Attribute
    {
    }

    public class NoToLuaAttribute : System.Attribute
    {
        public NoToLuaAttribute() { }
    }

    public class OnlyGCAttribute : System.Attribute
    {
        public OnlyGCAttribute() { }
    }

    public class UseDefinedAttribute : System.Attribute
    {
        public UseDefinedAttribute() { }
    }

    /// <summary>
    /// Marks a method for global usage in Lua scripts
    /// </summary>
    /// <see cref="LuaRegistrationHelper.TaggedInstanceMethods"/>
    /// <see cref="LuaRegistrationHelper.TaggedStaticMethods"/>
    [AttributeUsage(AttributeTargets.Method)]
    // sealed
    public class LuaGlobalAttribute : Attribute
    {
        private string name, descript;
        /// <summary>
        /// An alternative name to use for calling the function in Lua - leave empty for CLR name
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        /// <summary>
        /// A description of the function
        /// </summary>
        public string Description { get { return descript; } set { descript = value; } }
    }

    /// <summary>
    /// Marks a method, field or property to be hidden from Lua auto-completion
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class LuaHideAttribute : Attribute { }

}