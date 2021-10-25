using CosmosStack.Validation.Registrars;

namespace CosmosStack.Validation
{
    public interface IValidationRegistrarService
    {
        IValidationRegistrar ResolveDefaultRegistrar();

        IValidationRegistrar ResolveRegistrar();

        IValidationRegistrar ResolveRegistrar(string providerName);

        IValidationRegistrar RequiredResolveRegistrar(string providerName);
    }
}