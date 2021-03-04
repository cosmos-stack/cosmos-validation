using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    public static partial class ValidationRegistrarExtensions
    {
        public static IValidationRegistrar ForFluentValidator<TValidator>(this IValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator<FluentValidator<TValidator>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<TValidator>(this IFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<TValidator>(this IValueFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator>(this IValueFluentValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<TValidator>(this IWaitForMessageValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator>(this IWaitForMessageValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal, TValidator>(this IWaitForMessageValidationRegistrar<T, TVal> registrar)
            where TValidator : class, FluentValidation.IValidator, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator>>();
        }
    }
}
