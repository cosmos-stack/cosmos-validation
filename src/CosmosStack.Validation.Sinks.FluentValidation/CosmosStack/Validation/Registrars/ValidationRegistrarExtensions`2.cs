using System;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Validation registrar extensions <br />
    /// 验证注册器扩展
    /// </summary>
    public static partial class ValidationRegistrarExtensions
    {
        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar ForFluentValidator<TValidator, T>(this IValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator<FluentValidator<TValidator, T>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<TValidator, T>(this IFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<TValidator, T>(this IValueFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator, T2>(this IValueFluentValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator<T2>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T2>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<TValidator, T>(this IWaitForMessageValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator, T2>(this IWaitForMessageValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator<T2>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T2>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal, TValidator, T2>(this IWaitForMessageValidationRegistrar<T, TVal> registrar)
            where TValidator : class, FluentValidation.IValidator<T2>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T2>>();
        }
    }
}