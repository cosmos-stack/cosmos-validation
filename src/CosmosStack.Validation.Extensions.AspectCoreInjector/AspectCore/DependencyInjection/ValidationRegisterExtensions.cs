using CosmosStack.Dependency;

namespace AspectCore.DependencyInjection
{
    /// <summary>
    /// Cosmos Stack Validation Register Extensions
    /// </summary>
    public static class ValidationRegisterExtensions
    {
        /// <summary>
        /// Add Cosmos Stack Validation Support
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IServiceContext AddCosmosValidation(this IServiceContext context)
        {
            using var register = new AspectCoreProxyRegister(context);
            register.RegisterValidation();

            return context;
        }
    }
}