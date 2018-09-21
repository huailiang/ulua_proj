using Mono.Cecil;

namespace LuaEditor
{
    public static class DefinitionEx
    {

        public static bool HasCustomAttribute<T>(this MethodDefinition method)
        {
            if (method.HasCustomAttributes)
            {
                foreach (var customAttribute in method.CustomAttributes)
                {
                    if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool HasCustomAttribute<T>(this TypeDefinition type)
        {
            if (type.HasCustomAttributes)
            {
                foreach (var customAttribute in type.CustomAttributes)
                {
                    if (customAttribute.AttributeType.FullName.Equals(typeof(T).FullName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}