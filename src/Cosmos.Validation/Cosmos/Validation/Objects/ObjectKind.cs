using System;
using Cosmos.Reflection;

namespace Cosmos.Validation.Objects
{
    public enum ObjectKind
    {
        BasicType,
        ReferenceType,
    }

    internal static class ObjectKindExtensions
    {
        public static ObjectKind GetObjectKind(this Type type)
        {
            return type.IsBasicType() ? ObjectKind.BasicType : ObjectKind.ReferenceType;
        }

        public static bool IsBasicType(this Type type)
        {
            if (type.IsPrimitive)
                return true;

            if (type == TypeClass.StringClazz)
                return true;

            if (type == TypeClass.DateTimeClazz || type == TypeClass.DateTimeNullableClazz)
                return true;

            if (type == TypeClass.DateTimeOffsetClazz || type == TypeClass.DateTimeOffsetNullableClazz)
                return true;

            if (type == TypeClass.GuidClazz || type == TypeClass.GuidNullableClazz)
                return true;

            return false;
        }
    }
}