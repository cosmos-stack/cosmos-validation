using System;
using Cosmos.Validation;
using Cosmos.Validation.Internals;

namespace Cosmos.Dependency
{
    public static class DependencyExtensions
    {
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