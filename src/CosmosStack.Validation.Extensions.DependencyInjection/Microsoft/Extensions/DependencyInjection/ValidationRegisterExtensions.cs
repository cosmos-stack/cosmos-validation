using CosmosStack.Dependency;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Cosmos Stack Validation Register Extensions
    /// </summary>
    public static class ValidationRegisterExtensions
    {
        /// <summary>
        /// Add Cosmos Stack Validation Support
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCosmosValidation(this IServiceCollection services)
        {
            using var register = new MicrosoftProxyRegister(services);
            register.RegisterValidation();

            return services;
        }
    }
}