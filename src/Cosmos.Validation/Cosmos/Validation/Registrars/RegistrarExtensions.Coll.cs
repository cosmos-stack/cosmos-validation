using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Registrars
{
    public static class CollRegistrarExtensions
    {
        #region All/NotAll

        public static IPredicateValidationRegistrar All(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().All(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar NotAll(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().NotAll(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T, TItem[]> All<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return (IPredicateValidationRegistrar<T, TItem[]>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TItem[]> NotAll<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAll(func);
            return (IPredicateValidationRegistrar<T, TItem[]>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> All<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> NotAll<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAll(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion

        #region Any/NotAny

        public static IPredicateValidationRegistrar Any(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Any(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar NotAny(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().NotAny(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T, TItem[]> Any<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return (IPredicateValidationRegistrar<T, TItem[]>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TItem[]> NotAny<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAny(func);
            return (IPredicateValidationRegistrar<T, TItem[]>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Any<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> NotAny<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAny(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion

        #region In/NotIn

        public static IPredicateValidationRegistrar In(this IValueFluentValidationRegistrar registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collection);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar In(this IValueFluentValidationRegistrar registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().In(objects);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar NotIn(this IValueFluentValidationRegistrar registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collection);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar NotIn(this IValueFluentValidationRegistrar registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(objects);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> In<T>(this IValueFluentValidationRegistrar<T> registrar, ICollection<T> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collection);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> In<T>(this IValueFluentValidationRegistrar<T> registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().In(objects);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> NotIn<T>(this IValueFluentValidationRegistrar<T> registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collection);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> NotIn<T>(this IValueFluentValidationRegistrar<T> registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(objects);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> In<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, ICollection<TVal> collection)
        {
            registrar._impl().ExposeValueRuleBuilder2().In(collection);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> In<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params TVal[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder2().In(objects);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> NotIn<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, ICollection<TVal> collection)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotIn(collection);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> NotIn<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params TVal[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotIn(objects);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion

        #region None

        public static IPredicateValidationRegistrar None(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().None(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T, TItem[]> None<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().None(func);
            return (IPredicateValidationRegistrar<T, TItem[]>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> None<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().None(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        #endregion
    }
}