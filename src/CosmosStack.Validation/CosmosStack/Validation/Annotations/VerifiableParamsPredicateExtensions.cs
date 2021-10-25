using System;
using AspectCore.DynamicProxy.Parameters;
using CosmosStack.Exceptions;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Annotations
{
    internal static class VerifiableParamsPredicateExtensions
    {
        #region Is/IsNot

        public static (bool Valid, Type ParameterType, string Message) Is(this Type parameterType, Type targetType)
            => (parameterType == targetType, parameterType, string.Empty);

        public static (bool Valid, Type ParameterType, string Message) IsNot(this Type parameterType, Type targetType)
            => (parameterType != targetType, parameterType, string.Empty);

        public static (bool Valid, Type ParameterType, string Message) Is<T>(this Type parameterType)
            => (parameterType == typeof(T), parameterType, string.Empty);

        public static (bool Valid, Type ParameterType, string Message) IsNot<T>(this Type parameterType)
            => (parameterType != typeof(T), parameterType, string.Empty);

        public static (bool Valid, Type ParameterType, string Message) Is(this VerifiableMemberContext context, Type targetType)
            => (context.MemberType == targetType, context.MemberType, string.Empty);

        public static (bool Valid, Type ParameterType, string Message) Is<T>(this VerifiableMemberContext context)
            => (context.MemberType == typeof(T), context.MemberType, string.Empty);

        public static (bool Valid, Type ParameterType, string Message) IsNot(this VerifiableMemberContext context, Type targetType)
            => (context.MemberType != targetType, context.MemberType, string.Empty);

        public static (bool Valid, Type ParameterType, string Message) IsNot<T>(this VerifiableMemberContext context)
            => (context.MemberType != typeof(T), context.MemberType, string.Empty);

        #endregion

        #region Or/OrNot

        public static (bool Valid, Type ParameterType, string Message) Or(this (bool Valid, Type ParameterType, string Message) result, Type targetType)
        {
            return result.Valid ? result : result.ParameterType.Is(targetType);
        }

        public static (bool Valid, Type ParameterType, string Message) Or<T>(this (bool Valid, Type ParameterType, string Message) result)
        {
            return result.Valid ? result : result.ParameterType.Is<T>();
        }

        public static (bool Valid, Type ParameterType, string Message) OrNot(this (bool Valid, Type ParameterType, string Message) result, Type targetType)
        {
            return result.Valid ? result : result.ParameterType.IsNot(targetType);
        }

        public static (bool Valid, Type ParameterType, string Message) OrNot<T>(this (bool Valid, Type ParameterType, string Message) result)
        {
            return result.Valid ? result : result.ParameterType.IsNot<T>();
        }

        #endregion

        #region TryTo

        public static TValue TryTo<TValue>(this Parameter parameter)
        {
            return parameter.TryTo(default(TValue));
        }

        public static TValue TryTo<TValue>(this decimal numericValue)
        {
            return numericValue.TryTo(default(TValue));
        }

        public static TValue TryTo<TValue>(this Parameter parameter, TValue defaultValue)
        {
            try
            {
                return parameter.Value.As<TValue>();
            }
            catch
            {
                return defaultValue;
            }
        }

        public static TValue TryTo<TValue>(this decimal numericValue, TValue defaultValue)
        {
            try
            {
                return numericValue.As<TValue>();
            }
            catch
            {
                return defaultValue;
            }
        }

        public static TValue TryTo<TValue>(this double numericValue, TValue defaultValue)
        {
            try
            {
                return numericValue.As<TValue>();
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region Check

        public static (bool Valid, Type ParameterType, string Message) Check<T>(this object originalValue, Action<T> checker, Func<object, T> convertor = null)
        {
            var value = convertor is null
                ? originalValue._TryTo<T>()
                : convertor(originalValue);

            var @try = Try.Create(() =>
            {
                checker.Invoke(value);
                return true;
            });

            var message = string.Empty;

            @try = @try.Recover(ex =>
            {
                message = ex.Message;
                return false;
            });

            return (@try.Value, typeof(T), message);
        }

        public static (bool Valid, Type ParameterType, string Message) Check<T, TObj>(this TObj originalValue, Action<T> checker, Func<TObj, T> convertor = null)
        {
            var value = convertor is null
                ? originalValue._TryTo<T, TObj>()
                : convertor(originalValue);

            var @try = Try.Create(() =>
            {
                checker.Invoke(value);
                return true;
            });

            var message = string.Empty;

            @try = @try.Recover(ex =>
            {
                message = ex.Message;
                return false;
            });

            return (@try.Value, typeof(T), message);
        }

        internal static TValue _TryTo<TValue>(this object value, TValue defaultValue = default)
        {
            try
            {
                return value.As<TValue>();
            }
            catch
            {
                return defaultValue;
            }
        }

        internal static TValue _TryTo<TValue, TObj>(this TObj value, TValue defaultValue = default)
        {
            try
            {
                return value.As<TValue>();
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
    }
}