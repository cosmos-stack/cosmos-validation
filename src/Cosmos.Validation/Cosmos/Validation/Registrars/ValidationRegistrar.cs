using System;
using Cosmos.Validation.Internals;

namespace Cosmos.Validation.Registrars
{
    public static class ValidationRegistrar
    {
        public static IValidationRegistrar DefaultRegistrar =>
            new InternalValidationRegistrar(ValidationMe.ExposeDefaultProvider(), RegisterMode.Direct, ValidationProvider.DefaultName);

        public static IValidationRegistrar ForProvider(IValidationProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            ValidationMe.RegisterProvider(provider);

            return new InternalValidationRegistrar(provider, RegisterMode.Hosted, ValidationProvider.MainName);
        }

        public static IValidationRegistrar ForProvider(IValidationProvider provider, string name)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            if (string.IsNullOrWhiteSpace(name))
                name = $"{provider.GetType().FullName}_{provider.GetHashCode()}";

            ValidationMe.RegisterProvider(provider, name);

            return new InternalValidationRegistrar(provider, RegisterMode.Hosted, name);
        }

        public static IValidationRegistrar Continue()
        {
            var provider = ValidationMe.ExposeValidationProvider();

            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            var name = ((ICorrectProvider) provider).Name;
            var mode = ValidationProvider.IsDefault(name) ? RegisterMode.Direct : RegisterMode.Hosted;

            return new InternalValidationRegistrar(provider, mode, name);
        }

        public static IValidationRegistrar Continue(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Continue();

            var provider = ValidationMe.ExposeValidationProvider(name);

            if (provider is null)
                throw new InvalidOperationException($"There's no such name '{name}' for Validation Provider.");

            name = ((ICorrectProvider) provider).Name;

            return new InternalValidationRegistrar(provider, RegisterMode.Hosted, name);
        }
    }
}