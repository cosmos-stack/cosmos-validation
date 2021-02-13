using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Extensions
{
    internal static class DefaultObjValue
    {
        public static object Get(Type type, object value)
        {
            if (type.IsValueType)
            {
                return value switch
                {
                    byte _ => default,
                    short _ => default,
                    int _ => default,
                    long _ => default,
                    sbyte _ => default,
                    ushort _ => default,
                    uint _ => default,
                    ulong _ => default,
                    char _ => default,
                    float _ => default,
                    double _ => default,
                    decimal _ => default,
                    DateTime _ => default,
                    DateTimeOffset _ => default,
                    Enum _ => default,
                    _ => default
                };
            }

            return default;
        }
    }
    
    internal static class DefaultObjValueExtensions
    {
        public static object GetDefaultValue(this ObjectValueContext context, object value)
        {
            return DefaultObjValue.Get(context.MemberType, value);
        }
        
        public static object GetDefaultValue(this ObjectValueContract context, object value)
        {
            return DefaultObjValue.Get(context.MemberType, value);
        }
    }
}