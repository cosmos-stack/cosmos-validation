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
        /// <param name="typeOfValidator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar ForFluentValidator(this IValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="typeOfValidator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator(this IFluentValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="typeOfValidator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator(this IValueFluentValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="typeOfValidator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IValueFluentValidationRegistrar<T> registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="typeOfValidator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator(this IWaitForMessageValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="typeOfValidator"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IWaitForMessageValidationRegistrar<T> registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        /// <summary>
        /// Register for Sink Validator for FluentValidation
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="typeOfValidator"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal>(this IWaitForMessageValidationRegistrar<T, TVal> registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }
    }
}