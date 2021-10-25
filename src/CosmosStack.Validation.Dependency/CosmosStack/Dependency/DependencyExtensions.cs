using System;
using CosmosStack.Validation;
using CosmosStack.Validation.Internals;

namespace CosmosStack.Dependency
{
    /// <summary>
    /// Dependency extensions <br />
    /// 依赖扩展
    /// </summary>
    public static class DependencyExtensions
    {
        /// <summary>
        /// Register Cosmos Stack Validation
        /// </summary>
        /// <param name="register"></param>
        /// <typeparam name="TRegister"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TRegister RegisterValidation<TRegister>(this TRegister register) where TRegister : DependencyProxyRegister
        {
            if (register is null)
                throw new ArgumentNullException(nameof(register));

            register.AddSingleton<IValidationService, InternalValidationService>();
            register.AddSingleton<IValidationOptionsService, InternalValidationOptionsService>();
            register.AddSingleton<IValidationHandlerFactory, InternalValidationHandlerFactory>();
            register.AddSingleton<IValidationRegistrarService, InternalValidationRegistrarService>();

            return register;
        }
    }
}