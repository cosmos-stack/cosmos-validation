using System;

namespace Cosmos.Validation.Registrars
{
    public static class FuncRegistrarExtensions
    {
        public static IPredicateValidationRegistrar Func(this IValueFluentValidationRegistrar registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Func(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IWaitForMessageValidationRegistrar Func(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), func);
        }

        public static IPredicateValidationRegistrar<T> Func<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Func(func);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IWaitForMessageValidationRegistrar<T> Func<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), func);
        }

        public static IPredicateValidationRegistrar<T, TVal> Func<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Func(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IWaitForMessageValidationRegistrar<T, TVal> Func<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), func);
        }

        public static IWaitForMessageValidationRegistrar Predicate(this IValueFluentValidationRegistrar registrar, Predicate<object> predicate)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), predicate);
        }

        public static IWaitForMessageValidationRegistrar<T> Predicate<T>(this IValueFluentValidationRegistrar<T> registrar, Predicate<object> predicate)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), predicate);
        }

        public static IWaitForMessageValidationRegistrar<T, TVal> Predicate<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Predicate<TVal> predicate)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), predicate);
        }

        public static IPredicateValidationRegistrar Must(this IValueFluentValidationRegistrar registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Must(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IWaitForMessageValidationRegistrar Must(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), func);
        }
        
        public static IPredicateValidationRegistrar<T> Must<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Must(func);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IWaitForMessageValidationRegistrar<T> Must<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), func);
        }

        public static IPredicateValidationRegistrar<T, TVal> Must<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Must(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IWaitForMessageValidationRegistrar<T, TVal> Must<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), func);
        }

        public static IWaitForMessageValidationRegistrar Satisfies(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), func);
        }

        public static IPredicateValidationRegistrar Satisfies(this IValueFluentValidationRegistrar registrar, Func<object, bool> func, string message)
        {
            return Satisfies(registrar, func).WithMessage(message);
        }

        public static IWaitForMessageValidationRegistrar<T> Satisfies<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), func);
        }

        public static IPredicateValidationRegistrar<T> Satisfies<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func, string message)
        {
            return Satisfies(registrar, func).WithMessage(message);
        }
        
        public static IWaitForMessageValidationRegistrar<T, TVal> Satisfies<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), func);
        }

        public static IPredicateValidationRegistrar<T, TVal> Satisfies<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func, string message)
        {
            return Satisfies(registrar, func).WithMessage(message);
        }
    }
}