namespace Cosmos.Validation.Registrars
{
    public static class LengthRegistrarExtensions
    {
        public static IPredicateValidationRegistrar Length(this IValueFluentValidationRegistrar registrar, int min, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().Length(min, max);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar MinLength(this IValueFluentValidationRegistrar registrar, int min)
        {
            registrar._impl().ExposeValueRuleBuilder().MinLength(min);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar MaxLength(this IValueFluentValidationRegistrar registrar, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().MaxLength(max);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar AtLeast(this IValueFluentValidationRegistrar registrar, int count)
        {
            registrar._impl().ExposeValueRuleBuilder().AtLeast(count);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> Length<T>(this IValueFluentValidationRegistrar<T> registrar, int min, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().Length(min, max);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> MinLength<T>(this IValueFluentValidationRegistrar<T> registrar, int min)
        {
            registrar._impl().ExposeValueRuleBuilder().MinLength(min);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> MaxLength<T>(this IValueFluentValidationRegistrar<T> registrar, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().MaxLength(max);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> AtLeast<T>(this IValueFluentValidationRegistrar<T> registrar, int count)
        {
            registrar._impl().ExposeValueRuleBuilder().AtLeast(count);
            return (IPredicateValidationRegistrar<T>) registrar;
        }
        
        public static IPredicateValidationRegistrar<T, TVal> Length<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int min, int max)
        {
            registrar._impl().ExposeValueRuleBuilder2().Length(min, max);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> MinLength<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int min)
        {
            registrar._impl().ExposeValueRuleBuilder2().MinLength(min);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> MaxLength<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int max)
        {
            registrar._impl().ExposeValueRuleBuilder2().MaxLength(max);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> AtLeast<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int count)
        {
            registrar._impl().ExposeValueRuleBuilder2().AtLeast(count);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }
    }
}