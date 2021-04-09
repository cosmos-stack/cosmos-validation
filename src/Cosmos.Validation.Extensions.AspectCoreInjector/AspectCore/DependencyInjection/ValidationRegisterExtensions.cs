using Cosmos.Dependency;

namespace AspectCore.DependencyInjection
{
    public static class ValidationRegisterExtensions
    {
        public static IServiceContext AddCosmosValidation(this IServiceContext context)
        {
            using var register = new AspectCoreProxyRegister(context);
            register.RegisterValidation();

            return context;
        }
    }
}