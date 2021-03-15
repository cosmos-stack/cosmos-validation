using System;

namespace Cosmos.Validation.Registrars
{
    public static class RequiredRegistrarExtensions
    {
        #region Empty/NotEmpty

        public static IValueFluentValidationRegistrar Empty(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Empty();
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotEmpty(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEmpty();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> Empty<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Empty();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> NotEmpty<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEmpty();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> Empty<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().Empty();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotEmpty<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEmpty();
            return registrar;
        }

        #endregion

        #region Required

        public static IValueFluentValidationRegistrar Required(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Required();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> Required<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Required();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> Required<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().Required();
            return registrar;
        }

        #endregion

        #region Null/NotNull

        public static IValueFluentValidationRegistrar Null(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Null();
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotNull(this IValueFluentValidationRegistrar registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotNull();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> Null<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().Null();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> NotNull<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder().NotNull();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> Null<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().Null();
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotNull<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotNull();
            return registrar;
        }

        #endregion

        #region RequiredType/RequiredTypes

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IValueFluentValidationRegistrar RequiredType(this IValueFluentValidationRegistrar registrar, Type type)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredType(type);
            return registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IValueFluentValidationRegistrar RequiredTypes(this IValueFluentValidationRegistrar registrar, params Type[] types)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredTypes(types);
            return registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IValueFluentValidationRegistrar<T> RequiredType<T>(this IValueFluentValidationRegistrar<T> registrar, Type type)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredType(type);
            return registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="types"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IValueFluentValidationRegistrar<T> RequiredTypes<T>(this IValueFluentValidationRegistrar<T> registrar, params Type[] types)
        {
            registrar._impl().ExposeValueRuleBuilder().RequiredTypes(types);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> RequiredType<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Type type)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredType(type);
            return registrar;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="types"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params Type[] types)
        {
            registrar._impl().ExposeValueRuleBuilder2().RequiredTypes(types);
            return registrar;
        }

        #endregion
    }
}