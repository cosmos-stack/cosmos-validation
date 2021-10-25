using System;
using System.Globalization;
using CosmosStack.Reflection;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Required registrar extensions
    /// 必要性验证注册扩展
    /// </summary>
    public static class RequiredRegistrarExtensions
    {
        #region Empty/NotEmpty

        /// <summary>
        /// Empty <br />
        /// 应为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Empty(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Empty();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Not empty <br />
        /// 应不为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotEmpty(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEmpty();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Empty <br />
        /// 应为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Empty<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Empty();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Not empty <br />
        /// 应不为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotEmpty<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEmpty();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Empty <br />
        /// 应为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Empty<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().Empty();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Not empty <br />
        /// 应不为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotEmpty<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEmpty();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion

        #region Required

        /// <summary>
        /// Required <br />
        /// 需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Required(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Required();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Required <br />
        /// 需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Required<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Required();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Required <br />
        /// 需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Required<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().Required();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Required String <br />
        /// 字符串需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredString(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredString();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Required String <br />
        /// 字符串需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredString<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredString();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Required String <br />
        /// 字符串需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredString<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredString();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Required Numeric <br />
        /// 数字需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="isOptions"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredNumeric(this IValueFluentValidationRegistrar registrar, TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredNumeric(isOptions);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Required Numeric <br />
        /// 数字需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="isOptions"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredNumeric<T>(this IValueFluentValidationRegistrar<T> registrar, TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredNumeric(isOptions);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Required Numeric <br />
        /// 数字需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="isOptions"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredNumeric<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredNumeric(isOptions);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Required Boolean <br />
        /// 布尔值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredBoolean(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredBoolean();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Required Boolean <br />
        /// 布尔值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredBoolean<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredBoolean();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Required Boolean <br />
        /// 布尔值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredBoolean<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredBoolean();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Required Guid <br />
        /// GUID 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredGuid(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredGuid();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Required Guid <br />
        /// GUID 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredGuid<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredGuid();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Required Guid <br />
        /// GUID 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredGuid<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredGuid();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Required DateTime <br />
        /// DateTime 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredDateTime(this IValueFluentValidationRegistrar registrar, DateTimeStyles style = DateTimeStyles.None)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredDateTime(style);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Required DateTime <br />
        /// DateTime 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="style"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredDateTime<T>(this IValueFluentValidationRegistrar<T> registrar, DateTimeStyles style = DateTimeStyles.None)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredDateTime(style);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Required DateTime <br />
        /// DateTime 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="style"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredDateTime<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, DateTimeStyles style = DateTimeStyles.None)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredDateTime(style);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Required DateInfo <br />
        /// DateInfo 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredDateInfo(this IValueFluentValidationRegistrar registrar, DateTimeStyles style = DateTimeStyles.None)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredDateInfo(style);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Required DateInfo <br />
        /// DateInfo 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="style"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredDateInfo<T>(this IValueFluentValidationRegistrar<T> registrar, DateTimeStyles style = DateTimeStyles.None)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredDateInfo(style);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Required DateInfo <br />
        /// DateInfo 值需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="style"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredDateInfo<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, DateTimeStyles style = DateTimeStyles.None)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredDateInfo(style);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion

        #region Null/NotNull

        /// <summary>
        /// Null <br />
        /// 应为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Null(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Null();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Not null <br />
        /// 应不为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotNull(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotNull();
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Null <br />
        /// 应为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Null<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Null();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Not null <br />
        /// 应不为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotNull<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotNull();
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Null <br />
        /// 应为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Null<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().Null();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Not null <br />
        /// 应不为空
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotNull<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotNull();
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion

        #region RequiredType/RequiredTypes

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredType(this IValueFluentValidationRegistrar registrar, Type type)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredType(type);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RequiredTypes(this IValueFluentValidationRegistrar registrar, params Type[] types)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredTypes(types);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredType<T>(this IValueFluentValidationRegistrar<T> registrar, Type type)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredType(type);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="types"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RequiredTypes<T>(this IValueFluentValidationRegistrar<T> registrar, params Type[] types)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredTypes(types);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredType<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Type type)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredType(type);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="types"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RequiredTypes<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params Type[] types)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredTypes(types);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion
    }
}