using System;
using System.Collections;
using System.Collections.Generic;

namespace Cosmos.Validation.Registrars
{
    public static class CompareRegistrarExtensions
    {
        #region Equal/NotEqual

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value, comparer);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(valueFunc, valueType);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Equal(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(valueFunc, valueType, comparer);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value, comparer);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(valueFunc, valueType);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotEqual(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(valueFunc, valueType, comparer);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value, comparer);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(valueFunc, valueType);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Equal<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(valueFunc, valueType, comparer);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(value, comparer);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(valueFunc, valueType);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotEqual<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            registrar._impl().ExposeValueRuleBuilder().NotEqual(valueFunc, valueType, comparer);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().Equal(value, comparer);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc)
        {
            registrar._impl().ExposeValueRuleBuilder().Equal(valueFunc);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Equal<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().Equal(valueFunc, comparer);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(value, comparer);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(valueFunc);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="valueFunc"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc, IEqualityComparer<TVal> comparer)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotEqual(valueFunc, comparer);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion

        #region GreaterThan/GreaterThanOrEqual

        public static IPredicateValidationRegistrar GreaterThan(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar GreaterThan(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(valueFunc, valueType);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar GreaterThanOrEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar GreaterThanOrEqual(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(valueFunc, valueType);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> GreaterThan<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> GreaterThan<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThan(valueFunc, valueType);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> GreaterThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> GreaterThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().GreaterThanOrEqual(valueFunc, valueType);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> GreaterThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThan(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> GreaterThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThan(valueFunc);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> GreaterThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThanOrEqual(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> GreaterThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc)
        {
            registrar._impl().ExposeValueRuleBuilder2().GreaterThanOrEqual(valueFunc);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion

        #region LessThan/LessThanOrEqual

        public static IPredicateValidationRegistrar LessThan(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar LessThan(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(valueFunc, valueType);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar LessThanOrEqual(this IValueFluentValidationRegistrar registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(value);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar LessThanOrEqual(this IValueFluentValidationRegistrar registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(valueFunc, valueType);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> LessThan<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> LessThan<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThan(valueFunc, valueType);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> LessThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, object value)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(value);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> LessThanOrEqual<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object> valueFunc, Type valueType)
        {
            registrar._impl().ExposeValueRuleBuilder().LessThanOrEqual(valueFunc, valueType);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> LessThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThan(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> LessThan<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThan(valueFunc);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> LessThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal value)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThanOrEqual(value);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> LessThanOrEqual<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal> valueFunc)
        {
            registrar._impl().ExposeValueRuleBuilder2().LessThanOrEqual(valueFunc);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion
    }
}