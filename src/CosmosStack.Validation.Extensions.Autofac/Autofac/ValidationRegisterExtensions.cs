using CosmosStack.Dependency;

namespace Autofac
{
    /// <summary>
    /// Cosmos Stack Validation Register Extensions
    /// </summary>
    public static class ValidationRegisterExtensions
    {
        /// <summary>
        /// Add Cosmos Stack Validation Support
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ContainerBuilder RegisterCosmosValidation(this ContainerBuilder builder)
        {
            using var register = new AutofacProxyRegister(builder);
            register.RegisterValidation();

            return builder;
        }
    }
}