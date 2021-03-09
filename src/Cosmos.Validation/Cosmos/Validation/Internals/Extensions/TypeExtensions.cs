using System;
#if NET452
using Cosmos.Reflection;

#endif

namespace Cosmos.Validation.Internals.Extensions
{
    internal static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
#if NET452
            return type.GetFullyQualifiedName();
#else
            return type.GetDevelopName();
#endif
        }
    }
}