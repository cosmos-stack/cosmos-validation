using System;
using CosmosStack.Validation.Internals;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Validation registrar <br />
    /// 验证注册器
    /// </summary>
    public static class ValidationRegistrar
    {
        /// <summary>
        /// Gets default registrar <br />
        /// 获得默认注册器
        /// </summary>
        public static IValidationRegistrar DefaultRegistrar =>
            new InternalValidationRegistrar(ValidationMe.ExposeDefaultProvider(), RegisterMode.Direct, ValidationProvider.DefaultName);

        /// <summary>
        /// Get registrar for the given provider <br />
        /// 为给定的验证服务提供者程序提供一个注册器
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar ForProvider(IValidationProvider provider)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            ValidationMe.RegisterProvider(provider);

            return new InternalValidationRegistrar(provider, RegisterMode.Hosted, ValidationProvider.MainName);
        }

        /// <summary>
        /// Get registrar for the given provider <br />
        /// 为给定的验证服务提供者程序提供一个注册器
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar ForProvider(IValidationProvider provider, string name)
        {
            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            if (string.IsNullOrWhiteSpace(name))
                name = $"{provider.GetType().FullName}_{provider.GetHashCode()}";

            ValidationMe.RegisterProvider(provider, name);

            return new InternalValidationRegistrar(provider, RegisterMode.Hosted, name);
        }

        /// <summary>
        /// Get registrar for the given provider and continue to register <br />
        /// 为给定的验证服务提供者程序提供一个注册器，并继续注册
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IValidationRegistrar Continue()
        {
            var provider = ValidationMe.ExposeValidationProvider();

            if (provider is null)
                throw new ArgumentNullException(nameof(provider));

            var name = ((ICorrectProvider) provider).Name;
            var mode = ValidationProvider.IsDefault(name) ? RegisterMode.Direct : RegisterMode.Hosted;

            return new InternalValidationRegistrar(provider, mode, name);
        }

        /// <summary>
        /// Get registrar for the given provider and continue to register <br />
        /// 为给定的验证服务提供者程序提供一个注册器，并继续注册
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IValidationRegistrar Continue(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Continue();

            if (ValidationProvider.IsDefault(name))
                return DefaultRegistrar;

            var provider = ValidationMe.ExposeValidationProvider(name);

            if (provider is null)
                throw new InvalidOperationException($"There's no such name '{name}' for Validation Provider.");

            name = ((ICorrectProvider) provider).Name;

            return new InternalValidationRegistrar(provider, RegisterMode.Hosted, name);
        }
        
        internal static IValidationRegistrar ContinueOrDefault(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Continue();

            if (ValidationProvider.IsDefault(name))
                return DefaultRegistrar;

            var provider = ValidationMe.ExposeValidationProvider(name);

            if (provider is null)
                provider = ValidationMe.ExposeValidationProvider();

            name = ((ICorrectProvider) provider).Name;

            return new InternalValidationRegistrar(provider, RegisterMode.Hosted, name);
        }
    }
}