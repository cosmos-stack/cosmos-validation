using Cosmos.Dependency;

namespace Autofac
{
    public static class ValidationRegisterExtensions
    {
        public static ContainerBuilder RegisterCosmosValidation(this ContainerBuilder builder)
        {
            using var register = new AutofacProxyRegister(builder);
            register.RegisterValidation();

            return builder;
        }
    }
}