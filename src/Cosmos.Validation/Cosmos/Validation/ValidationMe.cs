using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    public static class ValidationMe
    {
        private static IValidationProvider DefaultProvider { get; set; }
        private static IValidationProvider CustomMainProvider { get; set; }

        private static readonly Dictionary<string, IValidationProvider> ScopedProviders = new();

        private static IValidationProvider _currentProvider;
        private static ICorrectProvider InnerPtr => (ICorrectProvider) _currentProvider;

        static ValidationMe()
        {
            var manager = new BuildInProjectManager();
            var resolver = new BuildInObjectResolver();
            DefaultProvider = new ValidationProvider(manager, resolver);

            _currentProvider = DefaultProvider;
        }

        internal static void RegisterProvider(IValidationProvider validationProvider)
        {
            if (validationProvider is not null)
            {
                CustomMainProvider = validationProvider;
                _currentProvider = CustomMainProvider;
            }
        }

        internal static void Unregister()
        {
            CustomMainProvider = null;
            _currentProvider = DefaultProvider;
        }

        internal static void RegisterProvider(IValidationProvider validationProvider, string name)
        {
            if (validationProvider is not null
             && !string.IsNullOrWhiteSpace(name)
             && !ScopedProviders.ContainsKey(name))
            {
                ScopedProviders[name] = validationProvider;
            }
        }

        internal static void OverrideProvider(IValidationProvider validationProvider, string name)
        {
            if (validationProvider is not null
             && !string.IsNullOrWhiteSpace(name)
             && ScopedProviders.ContainsKey(name))
            {
                ScopedProviders[name] = validationProvider;
            }
        }

        internal static IValidationProvider ExposeDefaultProvider() => DefaultProvider;

        internal static IValidationProvider ExposeValidationProvider() => _currentProvider;

        internal static IValidationProvider ExposeValidationProvider(string name)
        {
            if (ValidationProvider.IsDefault(name))
                return ExposeValidationProvider();

            if (!string.IsNullOrWhiteSpace(name) && ScopedProviders.TryGetValue(name, out var provider))
                return provider;

            return default;
        }

        internal static IValidationProjectManager ExposeProjectManager() => InnerPtr.ExposeProjectManager();

        internal static IValidationObjectResolver ExposeObjectResolver() => InnerPtr.ExposeObjectResolver();

        public static IValidator Resolve(Type type) => _currentProvider.Resolve(type);

        public static IValidator Resolve(Type type, string name) => _currentProvider.Resolve(type, name);

        public static IValidator Resolve<T>() => _currentProvider.Resolve<T>();

        public static IValidator Resolve<T>(string name) => _currentProvider.Resolve<T>(name);

        public static IValidationProvider Use(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && ScopedProviders.TryGetValue(name, out var provider))
                return new ValidationScope(provider, name);

            return new ValidationScope(_currentProvider, ValidationProvider.DefaultName);
        }
    }
}