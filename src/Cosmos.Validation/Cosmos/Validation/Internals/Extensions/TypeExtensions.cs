using System;
using System.Runtime.CompilerServices;
#if NETFRAMEWORK
using Cosmos.Reflection;

#endif

namespace Cosmos.Validation.Internals.Extensions
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