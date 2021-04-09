using Cosmos.Dependency;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ValidationRegisterExtensions
    {
        public static IServiceCollection AddCosmosValidation(this IServiceCollection services)
        {
            using var register = new MicrosoftProxyRegister(services);
            register.RegisterValidation();

            return services;
        }
    }
}