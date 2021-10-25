using System;
using System.Collections.Generic;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Collection register extensions <br />
    /// 注册扩展
    /// </summary>
    public static class CollRegistrarExtensions
    {
        #region All/NotAll

        /// <summary>
        /// All <br />
        /// 所有
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar All(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().All(func);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Not all <br />
        /// 不是所有
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotAll(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().NotAll(func);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// All <br />
        /// 所有
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TItem[]> All<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return (IPredicateValidationRegistrar<T, TItem[]>)registrar;
        }

        /// <summary>
        /// Not all <br />
        /// 不是所有
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TItem[]> NotAll<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAll(func);
            return (IPredicateValidationRegistrar<T, TItem[]>)registrar;
        }

        /// <summary>
        /// All <br />
        /// 所有
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> All<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Not all <br />
        /// 不是所有
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotAll<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAll(func);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion

        #region Any/NotAny

        /// <summary>
        /// Any <br />
        /// 任一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Any(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Any(func);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Not any <br />
        /// 无一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotAny(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().NotAny(func);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Any <br />
        /// 任一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TItem[]> Any<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return (IPredicateValidationRegistrar<T, TItem[]>)registrar;
        }

        /// <summary>
        /// Not any <br />
        /// 无一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TItem[]> NotAny<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAny(func);
            return (IPredicateValidationRegistrar<T, TItem[]>)registrar;
        }

        /// <summary>
        /// Any <br />
        /// 任一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Any<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Not any <br />
        /// 无一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotAny<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().NotAny(func);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion

        #region In/NotIn

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar In(this IValueFluentValidationRegistrar registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collection);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collectionFunc"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar In(this IValueFluentValidationRegistrar registrar, Func<ICollection<object>> collectionFunc)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collectionFunc);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar In(this IValueFluentValidationRegistrar registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().In(objects);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotIn(this IValueFluentValidationRegistrar registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collection);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collectionFunc"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotIn(this IValueFluentValidationRegistrar registrar, Func<ICollection<object>> collectionFunc)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collectionFunc);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar NotIn(this IValueFluentValidationRegistrar registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(objects);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> In<T>(this IValueFluentValidationRegistrar<T> registrar, ICollection<T> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collection);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> In<T>(this IValueFluentValidationRegistrar<T> registrar, Func<ICollection<object>> collectionFunc)
        {
            registrar._impl().ExposeValueRuleBuilder().In(collectionFunc);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> In<T>(this IValueFluentValidationRegistrar<T> registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().In(objects);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotIn<T>(this IValueFluentValidationRegistrar<T> registrar, ICollection<object> collection)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collection);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotIn<T>(this IValueFluentValidationRegistrar<T> registrar, Func<ICollection<object>> collectionFunc)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(collectionFunc);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> NotIn<T>(this IValueFluentValidationRegistrar<T> registrar, params object[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder().NotIn(objects);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> In<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, ICollection<TVal> collection)
        {
            registrar._impl().ExposeValueRuleBuilder2().In(collection);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> In<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<ICollection<object>> collectionFunc)
        {
            registrar._impl().ExposeValueRuleBuilder2().In(collectionFunc);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// In <br />
        /// 在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> In<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params TVal[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder2().In(objects);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotIn<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, ICollection<TVal> collection)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotIn(collection);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotIn<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<ICollection<object>> collectionFunc)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotIn(collectionFunc);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Not in <br />
        /// 不在内
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> NotIn<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, params TVal[] objects)
        {
            registrar._impl().ExposeValueRuleBuilder2().NotIn(objects);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion

        #region None

        /// <summary>
        /// None <br />
        /// 无一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar None(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder().None(func);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// None <br />
        /// 无一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TItem[]> None<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().None(func);
            return (IPredicateValidationRegistrar<T, TItem[]>)registrar;
        }

        /// <summary>
        /// None <br />
        /// 无一
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> None<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().None(func);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        #endregion
    }
}