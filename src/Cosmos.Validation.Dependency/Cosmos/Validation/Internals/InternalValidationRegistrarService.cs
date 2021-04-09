using Cosmos.Validation.Registrars;

namespace Cosmos.Validation.Internals
{
    internal class InternalValidationRegistrarService : IValidationRegistrarService
    {
        public IValidationRegistrar ResolveDefaultRegistrar() => ValidationRegistrar.DefaultRegistrar;

        public IValidationRegistrar ResolveRegistrar() => ValidationRegistrar.Continue();

        public IValidationRegistrar ResolveRegistrar(string providerName) => ValidationProvider.IsDefault(providerName) ? ValidationRegistrar.DefaultRegistrar : ValidationRegistrar.ContinueOrDefault(providerName);

        public IValidationRegistrar RequiredResolveRegistrar(string providerName) => ValidationProvider.IsDefault(providerName) ? ValidationRegistrar.DefaultRegistrar : ValidationRegistrar.Continue(providerName);
    }
}