using System.Collections;
using System.Collections.Generic;

namespace Cosmos.Validation.Registrars
{
    public static class CompareRegistrarExtensions
    {
        #region Equal/NotEqual

        public static IPredicateValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value, comparer);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value, comparer);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value, comparer);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value, comparer);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().Equal(value, comparer);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(value, comparer);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion

        #region GreaterThan/GreaterThanOrEqual

        public static IPredicateValidationRegistrar GreaterThan(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar GreaterThanOrEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> GreaterThan<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> GreaterThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> GreaterThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThan(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> GreaterThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThanOrEqual(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion

        #region LessThan/LessThanOrEqual

        public static IPredicateValidationRegistrar LessThan(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar LessThanOrEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> LessThan<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> LessThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> LessThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThan(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> LessThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThanOrEqual(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion
    }
}