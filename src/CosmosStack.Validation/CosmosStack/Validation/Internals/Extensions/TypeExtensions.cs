using System;
using System.Runtime.CompilerServices;
#if NETFRAMEWORK
using CosmosStack.Reflection;

#endif


namespace CosmosStack.Validation.Internals.Extensions
{
    internal static class TypeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetFriendlyName(this Type type)
        {
#if NETFRAMEWORK
            return type.GetFullyQualifiedName();
#else
            return type.GetDevelopName();
#endif
        }
    }
}