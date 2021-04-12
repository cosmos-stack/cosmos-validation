namespace Cosmos.Validation.Registrars
{
    public static class RangeRegistrarExtensions
    {
        public static IPredicateValidationRegistrar Range(this IValueFluentValidationRegistrar registrar, object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            registrar._impl().ExposeValueRuleBuilder().Range(from, to, options);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar RangeWithOpenInterval(this IValueFluentValidationRegistrar registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithOpenInterval(from, to);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar RangeWithCloseInterval(this IValueFluentValidationRegistrar registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithCloseInterval(from, to);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> Range<T>(this IValueFluentValidationRegistrar<T> registrar, object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            registrar._impl().ExposeValueRuleBuilder().Range(from, to, options);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> RangeWithOpenInterval<T>(this IValueFluentValidationRegistrar<T> registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithOpenInterval(from, to);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> RangeWithCloseInterval<T>(this IValueFluentValidationRegistrar<T> registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithCloseInterval(from, to);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Range<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            registrar._impl().ExposeValueRuleBuilder2().Range(from, to, options);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> RangeWithOpenInterval<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal from, TVal to)
        {
            registrar._impl().ExposeValueRuleBuilder2().RangeWithOpenInterval(from, to);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> RangeWithCloseInterval<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal from, TVal to)
        {
            registrar._impl().ExposeValueRuleBuilder2().RangeWithCloseInterval(from, to);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }
    }
}