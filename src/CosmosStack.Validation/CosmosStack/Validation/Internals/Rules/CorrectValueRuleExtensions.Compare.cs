using System;
using System.Collections;
using System.Collections.Generic;
using CosmosStack.Validation.Internals.Tokens.ValueTokens;

// ReSharper disable once CheckNamespace
namespace CosmosStack.Validation
{
    public static partial class CorrectValueRuleExtensions
    {
        #region Equal/NotEqual

        //`0

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder Equal(this IValueRuleBuilder builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, value, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder Equal(this IValueRuleBuilder builder, object value, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, value, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder Equal(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, valueFunc, valueType, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder Equal(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, valueFunc, valueType, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder NotEqual(this IValueRuleBuilder builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, value, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder NotEqual(this IValueRuleBuilder builder, object value, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, value, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder NotEqual(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, valueFunc, valueType, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder NotEqual(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, valueFunc, valueType, comparer);
            return current;
        }

        //`1

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> Equal<T>(this IValueRuleBuilder<T> builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, value, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> Equal<T>(this IValueRuleBuilder<T> builder, object value, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, value, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> Equal<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, valueFunc, valueType, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> Equal<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken(current._contract, valueFunc, valueType, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> NotEqual<T>(this IValueRuleBuilder<T> builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, value, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> NotEqual<T>(this IValueRuleBuilder<T> builder, object value, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, value, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> NotEqual<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, valueFunc, valueType, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> NotEqual<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken(current._contract, valueFunc, valueType, comparer);
            return current;
        }

        //`2

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> Equal<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken<TVal>(current._contract, value, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> Equal<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value, IEqualityComparer<TVal> comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken<TVal>(current._contract, value, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> Equal<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken<TVal>(current._contract, valueFunc, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> Equal<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc, IEqualityComparer<TVal> comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEqualToken<TVal>(current._contract, valueFunc, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken<TVal>(current._contract, value, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value, IEqualityComparer<TVal> comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken<TVal>(current._contract, value, comparer);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken<TVal>(current._contract, valueFunc, null);
            return current;
        }

        /// <summary>
        /// Determine whether the two values are not equal.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc, IEqualityComparer<TVal> comparer)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEqualToken<TVal>(current._contract, valueFunc, comparer);
            return current;
        }

        #endregion

        #region LessThan/LessThanOrEqual/GreaterThan/GreaterThanOrEqual

        //`0

        /// <summary>
        /// Determine whether one value is less than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder LessThan(this IValueRuleBuilder builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder LessThan(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanToken(current._contract, valueFunc, valueType);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder LessThanOrEqual(this IValueRuleBuilder builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanOrEqualToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder LessThanOrEqual(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanOrEqualToken(current._contract, valueFunc, valueType);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder GreaterThan(this IValueRuleBuilder builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder GreaterThan(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanToken(current._contract, valueFunc, valueType);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder GreaterThanOrEqual(this IValueRuleBuilder builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanOrEqualToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder GreaterThanOrEqual(this IValueRuleBuilder builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanOrEqualToken(current._contract, valueFunc, valueType);
            return current;
        }

        //`1

        /// <summary>
        /// Determine whether one value is less than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> LessThan<T>(this IValueRuleBuilder<T> builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> LessThan<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanToken(current._contract, valueFunc, valueType);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> LessThanOrEqual<T>(this IValueRuleBuilder<T> builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanOrEqualToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> LessThanOrEqual<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanOrEqualToken(current._contract, valueFunc, valueType);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> GreaterThan<T>(this IValueRuleBuilder<T> builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> GreaterThan<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanToken(current._contract, valueFunc, valueType);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> GreaterThanOrEqual<T>(this IValueRuleBuilder<T> builder, object value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanOrEqualToken(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <param name="valueType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> GreaterThanOrEqual<T>(this IValueRuleBuilder<T> builder, Func<object> valueFunc, Type valueType)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanOrEqualToken(current._contract, valueFunc, valueType);
            return current;
        }

        //`2

        /// <summary>
        /// Determine whether one value is less than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> LessThan<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanToken<TVal>(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> LessThan<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanToken<TVal>(current._contract, valueFunc);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> LessThanOrEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanOrEqualToken<TVal>(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is less than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> LessThanOrEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLessThanOrEqualToken<TVal>(current._contract, valueFunc);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> GreaterThan<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanToken<TVal>(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> GreaterThan<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanToken<TVal>(current._contract, valueFunc);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> GreaterThanOrEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal value)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanOrEqualToken<TVal>(current._contract, value);
            return current;
        }

        /// <summary>
        /// Determine whether one value is greater than or equal to another value.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="valueFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> GreaterThanOrEqual<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal> valueFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueGreaterThanOrEqualToken<TVal>(current._contract, valueFunc);
            return current;
        }

        #endregion
    }
}