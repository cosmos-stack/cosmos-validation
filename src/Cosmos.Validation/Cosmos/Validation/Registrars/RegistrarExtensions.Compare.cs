using System.Collections;
using System.Collections.Generic;

namespace Cosmos.Validation.Registrars
{
    public static class CompareRegistrarExtensions
    {
        #region Equal/NotEqual

        public static IValueFluentValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value, comparer);
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value, comparer);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value, comparer);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value, comparer);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().Equal(value, comparer);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(value, comparer);
            return registrar;
        }

        #endregion

        #region GreaterThan/GreaterThanOrEqual

        public static IValueFluentValidationRegistrar GreaterThan(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar GreaterThanOrEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> GreaterThan<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> GreaterThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> GreaterThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThan(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> GreaterThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThanOrEqual(value);
            return registrar;
        }

        #endregion

        #region LessThan/LessThanOrEqual

        public static IValueFluentValidationRegistrar LessThan(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar LessThanOrEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> LessThan<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> LessThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> LessThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThan(value);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> LessThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThanOrEqual(value);
            return registrar;
        }

        #endregion
    }
}