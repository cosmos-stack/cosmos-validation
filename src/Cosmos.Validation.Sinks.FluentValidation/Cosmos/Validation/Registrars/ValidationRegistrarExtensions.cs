using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    public static partial class ValidationRegistrarExtensions
    {
        public static IValidationRegistrar ForFluentValidator(this IValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.ForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator(this IFluentValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator(this IValueFluentValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IValueFluentValidationRegistrar<T> registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator(this IWaitForMessageValidationRegistrar registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T>(this IWaitForMessageValidationRegistrar<T> registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }

        public static IFluentValidationRegistrar AndForFluentValidator<T, TVal>(this IWaitForMessageValidationRegistrar<T, TVal> registrar, Type typeOfValidator)
        {
            if (registrar is null) throw new ArgumentNullException(nameof(registrar));

            return registrar.AndForCustomValidator(FluentValidator.By(typeOfValidator));
        }
    }
}
