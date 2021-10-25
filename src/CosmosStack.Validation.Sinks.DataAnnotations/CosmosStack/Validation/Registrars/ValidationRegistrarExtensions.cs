using System;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Validation registrar extensions <br />
    /// 验证注册器扩展
    /// </summary>
    public static class ValidationRegistrarExtensions
    {
        /// <summary>
        /// Add DataAnnotation support <br />
        /// 添加对数据注解的支持
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar ForDataAnnotationSupport(this IValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator<DataAnnotationValidator>();
        }

        /// <summary>
        /// Add DataAnnotation support <br />
        /// 添加对数据注解的支持
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForDataAnnotationSupport(this IFluentValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        /// <summary>
        /// Add DataAnnotation support <br />
        /// 添加对数据注解的支持
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForDataAnnotationSupport(this IValueFluentValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        /// <summary>
        /// Add DataAnnotation support <br />
        /// 添加对数据注解的支持
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForDataAnnotationSupport<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        /// <summary>
        /// Add DataAnnotation support <br />
        /// 添加对数据注解的支持
        /// </summary>
        /// <param name="registrar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForDataAnnotationSupport(this IWaitForMessageValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        /// <summary>
        /// Add DataAnnotation support <br />
        /// 添加对数据注解的支持
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForDataAnnotationSupport<T>(this IWaitForMessageValidationRegistrar<T> registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        /// <summary>
        /// Add DataAnnotation support <br />
        /// 添加对数据注解的支持
        /// </summary>
        /// <param name="registrar"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IFluentValidationRegistrar AndForDataAnnotationSupport<T, TVal>(this IWaitForMessageValidationRegistrar<T, TVal> registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }
    }
}