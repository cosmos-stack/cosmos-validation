using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    public static partial class ValidationRegistrarExtensions
    {
        public static IValidationRegistrar ForFluentValidator(this IValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator(FluentValidator.By(validator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator(this IFluentValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator(this IValueFluentValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IValueFluentValidationRegistrar<T> registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator(this IWaitForMessageValidationRegistrar registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IWaitForMessageValidationRegistrar<T> registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal>(this IWaitForMessageValidationRegistrar<T, TVal> registrar, FluentValidation.IValidator validator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(validator));
        }
    }
}
