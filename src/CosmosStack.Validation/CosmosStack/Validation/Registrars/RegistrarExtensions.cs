namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Register extensions <br />
    /// 注册扩展
    /// </summary>
    internal static class RegistrarExtensions
    {
        public static ValueValidationRegistrar<T, TVal> _impl<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> builder)
        {
            return (ValueValidationRegistrar<T, TVal>) builder;
        }

        public static ValueValidationRegistrar<T> _impl<T>(this IValueFluentValidationRegistrar<T> builder)
        {
            return (ValueValidationRegistrar<T>) builder;
        }

        public static ValueValidationRegistrar _impl(this IValueFluentValidationRegistrar builder)
        {
            return (ValueValidationRegistrar) builder;
        }
    }
}