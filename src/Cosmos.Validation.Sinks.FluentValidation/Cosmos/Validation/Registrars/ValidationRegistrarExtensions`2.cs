using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    public static partial class ValidationRegistrarExtensions
    {
        public static IValidationRegistrar ForFluentValidator<TValidator, T>(this IValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator<FluentValidator<TValidator, T>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<TValidator, T>(this IFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<TValidator, T>(this IValueFluentValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator, T2>(this IValueFluentValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator<T2>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T2>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<TValidator, T>(this IWaitForMessageValidationRegistrar registrar)
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TValidator, T2>(this IWaitForMessageValidationRegistrar<T> registrar)
            where TValidator : class, FluentValidation.IValidator<T2>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T2>>();
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal, TValidator, T2>(this IWaitForMessageValidationRegistrar<T, TVal> registrar)
            where TValidator : class, FluentValidation.IValidator<T2>, new()
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator<FluentValidator<TValidator, T2>>();
        }
    }
}
