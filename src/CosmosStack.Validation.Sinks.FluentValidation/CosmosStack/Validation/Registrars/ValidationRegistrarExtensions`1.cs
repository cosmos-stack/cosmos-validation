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
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar ForFluentValidator<TValidator>(this IValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator<FluentValidator<TValidator>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<TValidator>(this IFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<TValidator>(this IValueFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator>(this IValueFluentValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<TValidator>(this IWaitForMessageValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator>(this IWaitForMessageValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal, TValidator>(this IWaitForMessageValidationRegistrar<T, TVal> registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }
    }
}