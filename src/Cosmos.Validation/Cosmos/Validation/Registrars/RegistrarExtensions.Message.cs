namespace Cosmos.Validation.Registrars
{
    public static class MessageRegistrarExtensions
    {
        public static IPredicateValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message, appendOrOverwrite);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }
    }
}