namespace Cosmos.Validation.Registrars
{
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

    public static partial class ValueValidationRegistrarExtensions
    {
        #region WithMessage

        public static IValueFluentValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return registrar;
        }

        public static IValueFluentValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message, appendOrOverwrite);
            return registrar;
        }

        #endregion
    }
}