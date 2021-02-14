using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Annotations.Core
{
    /// <summary>
    /// Annotation verification engine extensions
    /// </summary>
    internal static class ObjectValueContextExtensions
    {
        public static ParameterTypeValidation Is(this ObjectValueContext context, Type targetType)
        {
            var parameterType = context.MemberType;
            return new ParameterTypeValidation(parameterType == targetType, parameterType);
        }

        public static ParameterTypeValidation IsNot(this ObjectValueContext context, Type targetType)
        {
            var parameterType = context.MemberType;
            return new ParameterTypeValidation(parameterType == targetType, parameterType);
        }

        public static ParameterTypeValidation Is<T>(this ObjectValueContext context)
        {
            return context.Is(typeof(T));
        }

        public static ParameterTypeValidation IsNot<T>(this ObjectValueContext context)
        {
            return context.IsNot(typeof(T));
        }

        public static ParameterTypeValidation Or(this ParameterTypeValidation result, Type targetType)
        {
            return result ? result : new ParameterTypeValidation(result.ParameterType == targetType, result.ParameterType);
        }

        public static ParameterTypeValidation Or<T>(this ParameterTypeValidation result)
        {
            return result.Or(typeof(T));
        }

        public static ParameterTypeValidation OrNot(this ParameterTypeValidation result, Type targetType)
        {
            return result ? result : new ParameterTypeValidation(result.ParameterType != targetType, result.ParameterType);
        }

        public static ParameterTypeValidation OrNot<T>(this ParameterTypeValidation result)
        {
            return result.OrNot(typeof(T));
        }
    }
}