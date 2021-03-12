using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Registrars
{
    public static class CollRegistrarExtensions
    {
        #region All/NotAll

        public static IValueFluentValidationRegistrar All(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().All(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotAll(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().NotAll(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> All<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> NotAll<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAll(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> All<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotAll<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAll(func);
            return registrar;
        }

        #endregion

        #region Any/NotAny

        public static IValueFluentValidationRegistrar Any(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Any(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotAny(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().NotAny(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> Any<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> NotAny<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAny(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> Any<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotAny<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAny(func);
            return registrar;
        }

        #endregion

        #region In/NotIn

        public static IValueFluentValidationRegistrar In(this IValueFluentValidationRegistrar registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collection);
            return registrar;
        }

        public static IValueFluentValidationRegistrar In(this IValueFluentValidationRegistrar registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().In(objects);
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotIn(this IValueFluentValidationRegistrar registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collection);
            return registrar;
        }

        public static IValueFluentValidationRegistrar NotIn(this IValueFluentValidationRegistrar registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(objects);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> In<T>(this IValueFluentValidationRegistrar<T> registrar, ICollection<T> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collection);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> In<T>(this IValueFluentValidationRegistrar<T> registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().In(objects);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> NotIn<T>(this IValueFluentValidationRegistrar<T> registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collection);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> NotIn<T>(this IValueFluentValidationRegistrar<T> registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(objects);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> In<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, ICollection<TVal> collection)
        {
            registrar._impl().ExposeValueRuleBuilder2().In(collection);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> In<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params TVal[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder2().In(objects);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotIn<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, ICollection<TVal> collection)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotIn(collection);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotIn<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params TVal[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotIn(objects);
            return registrar;
        }

        #endregion

        #region None

        public static IValueFluentValidationRegistrar None(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().None(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> None<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().None(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> None<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().None(func);
            return registrar;
        }

        #endregion
    }
}