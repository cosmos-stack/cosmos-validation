namespace Cosmos.Validation.Registrars
{
    public static class MessageRegistrarExtensions
    {
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
    }
}