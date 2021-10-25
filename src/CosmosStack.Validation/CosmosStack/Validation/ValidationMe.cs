using System;
using System.Collections.Generic;
using CosmosStack.Validation.Internals;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation
{
    /// <summary>
    /// The entry of the static validator component. <br />
    /// 验证组件静态入口
    /// </summary>
    public static class ValidationMe
    {
        private static IValidationProvider DefaultProvider { get; set; }
        private static IValidationProvider CustomMainProvider { get; set; }

        private static readonly Dictionary<string, IValidationProvider> ScopedProviders = new();
        private static readonly object _scopedProviderLockObj = new();

        private static IValidationProvider _currentProvider;
        private static ICorrectProvider InnerPtr => (ICorrectProvider) _currentProvider;

        static ValidationMe()
        {
            var manager = new BuildInProjectManager();
            var resolver = new DefaultVerifiableObjectResolver();
            DefaultProvider = new ValidationProvider(manager, resolver, new());

            _currentProvider = DefaultProvider;
        }

        #region Register & Unregister

        /// <summary>
        /// Register a new ValidationProvider. <br />
        /// 注册一个新的 ValidationProvider。
        /// </summary>
        /// <param name="validationProvider"></param>
        internal static void RegisterProvider(IValidationProvider validationProvider)
        {
            if (validationProvider is not null)
            {
                ((ICorrectProvider) validationProvider).Name = ValidationProvider.MainName;
                CustomMainProvider = validationProvider;
                _currentProvider = CustomMainProvider;
            }
        }

        /// <summary>
        /// Unregister <br />
        /// 反注册
        /// </summary>
        internal static void Unregister()
        {
            CustomMainProvider = null;
            _currentProvider = DefaultProvider;
        }

        /// <summary>
        /// Register a new ValidationProvider. <br />
        /// 注册一个新的 ValidationProvider。
        /// </summary>
        /// <param name="validationProvider"></param>
        /// <param name="name"></param>
        internal static void RegisterProvider(IValidationProvider validationProvider, string name)
        {
            lock (_scopedProviderLockObj)
            {
                if (validationProvider is not null
                 && !string.IsNullOrWhiteSpace(name)
                 && !ScopedProviders.ContainsKey(name))
                {
                    ((ICorrectProvider) validationProvider).Name = name;
                    ScopedProviders[name] = validationProvider;
                }
            }
        }

        /// <summary>
        /// Override a ValidationProvider. <br />
        /// 覆盖一个 ValidationProvider。
        /// </summary>
        /// <param name="validationProvider"></param>
        /// <param name="name"></param>
        internal static void OverrideProvider(IValidationProvider validationProvider, string name)
        {
            lock (_scopedProviderLockObj)
            {
                if (validationProvider is not null
                 && !string.IsNullOrWhiteSpace(name)
                 && ScopedProviders.ContainsKey(name))
                {
                    ((ICorrectProvider) validationProvider).Name = name;
                    ScopedProviders[name] = validationProvider;
                }
            }
        }

        #endregion

        #region Internal Expose

        internal static IValidationProvider ExposeDefaultProvider() => DefaultProvider;

        internal static IValidationProvider ExposeValidationProvider() => _currentProvider;

        internal static IValidationProvider ExposeValidationProvider(string name)
        {
            if (ValidationProvider.IsDefault(name))
                return ExposeValidationProvider();

            lock (_scopedProviderLockObj)
            {
                if (!string.IsNullOrWhiteSpace(name) && ScopedProviders.TryGetValue(name, out var provider))
                    return provider;
            }

            return default;
        }

        internal static IValidationProjectManager ExposeProjectManager() => InnerPtr.ExposeProjectManager();

        internal static IVerifiableObjectResolver ExposeObjectResolver() => InnerPtr.ExposeObjectResolver();

        #endregion

        #region Resolve

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IValidator Resolve(Type type) => _currentProvider.Resolve(type);

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IValidator Resolve(Type type, string name) => _currentProvider.Resolve(type, name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IValidator<T> Resolve<T>() => _currentProvider.Resolve<T>();

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IValidator<T> Resolve<T>(string name) => _currentProvider.Resolve<T>(name);

        #endregion

        #region Use Scope

        /// <summary>
        /// According to the given name, an instance of IValidationProvider is returned.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IValidationProvider Use(string name)
        {
            lock (_scopedProviderLockObj)
            {
                if (!string.IsNullOrWhiteSpace(name) && ScopedProviders.TryGetValue(name, out var provider))
                    return new ValidationScope(provider, name);
            }

            return new ValidationScope(_currentProvider, ((ICorrectProvider) _currentProvider).Name);
        }

        #endregion

        #region Update Options

        /// <summary>
        /// Override the default options.
        /// </summary>
        /// <param name="options"></param>
        public static void UpdateDefaultOptions(ValidationOptions options)
        {
            DefaultProvider.UpdateOptions(options);
        }

        /// <summary>
        /// Update the default options.
        /// </summary>
        /// <param name="optionAct"></param>
        public static void UpdateDefaultOptions(Action<ValidationOptions> optionAct)
        {
            DefaultProvider.UpdateOptions(optionAct);
        }

        /// <summary>
        /// Override the main validation provider's options
        /// </summary>
        /// <param name="options"></param>
        public static void UpdateMainOptions(ValidationOptions options)
        {
            _currentProvider.UpdateOptions(options);
        }

        /// <summary>
        /// Update the main validation provider's options
        /// </summary>
        /// <param name="optionAct"></param>
        public static void UpdateMainOptions(Action<ValidationOptions> optionAct)
        {
            _currentProvider.UpdateOptions(optionAct);
        }

        /// <summary>
        /// Override the validation provider's options with the given name.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="options"></param>
        public static void UpdateOptions(string providerName, ValidationOptions options)
        {
            lock (_scopedProviderLockObj)
            {
                if (string.IsNullOrWhiteSpace(providerName))
                    UpdateMainOptions(options);
                else if (ScopedProviders.TryGetValue(providerName, out var provider))
                    provider.UpdateOptions(options);
            }
        }

        /// <summary>
        /// Update the validation provider's options with the given name.
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="optionAct"></param>
        public static void UpdateOptions(string providerName, Action<ValidationOptions> optionAct)
        {
            lock (_scopedProviderLockObj)
            {
                if (string.IsNullOrWhiteSpace(providerName))
                    UpdateMainOptions(optionAct);
                else if (ScopedProviders.TryGetValue(providerName, out var provider))
                    provider.UpdateOptions(optionAct);
            }
        }

        #endregion
    }
}