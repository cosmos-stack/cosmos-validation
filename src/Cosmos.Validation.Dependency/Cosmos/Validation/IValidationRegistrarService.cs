using Cosmos.Validation.Registrars;

namespace Cosmos.Validation
{
    public interface IValidationRegistrarService
    {
        IValidationRegistrar ResolveDefaultRegistrar();

        IValidationRegistrar ResolveRegistrar();

        IValidationRegistrar ResolveRegistrar(string providerName);

        IValidationRegistrar RequiredResolveRegistrar(string providerName);
    }
}