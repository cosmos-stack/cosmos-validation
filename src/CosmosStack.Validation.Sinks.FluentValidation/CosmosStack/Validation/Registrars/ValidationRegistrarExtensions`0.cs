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
        /// <param name="validator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar ForFluentValidator(this IValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator(FluentValidator.By(validator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator(this IFluentValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator(this IValueFluentValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="validator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IValueFluentValidationRegistrar<T> registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator(this IWaitForMessageValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="validator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IWaitForMessageValidationRegistrar<T> registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="validator"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal>(this IWaitForMessageValidationRegistrar<T, TVal> registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }
    }
}