using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    public static class ValidationRegistrarExtensions
    {
        public static IValidationRegistrar ForDataAnnotationSupport(this IValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator<DataAnnotationValidator>();
        }

        public static IFluentValidationRegistrar AndForDataAnnotationSupport(this IFluentValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        public static IFluentValidationRegistrar AndForDataAnnotationSupport(this IValueFluentValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        public static IFluentValidationRegistrar AndForDataAnnotationSupport<T>(this IValueFluentValidationRegistrar<T> registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        public static IFluentValidationRegistrar AndForDataAnnotationSupport(this IWaitForMessageValidationRegistrar registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        public static IFluentValidationRegistrar AndForDataAnnotationSupport<T>(this IWaitForMessageValidationRegistrar<T> registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }

        public static IFluentValidationRegistrar AndForDataAnnotationSupport<T, TVal>(this IWaitForMessageValidationRegistrar<T, TVal> registrar)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<DataAnnotationValidator>();
        }
    }
}