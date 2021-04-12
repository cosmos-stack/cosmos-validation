using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Tokens.ValueTokens;

// ReSharper disable once CheckNamespace
namespace Cosmos.Validation
{
    public static partial class CorrectValueRuleExtensions
    {
        #region Any/All/NotAny/NotAll/None
        
        //`0

        public static IPredicateValueRuleBuilder Any(this IValueRuleBuilder builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAnyToken(current._contract, func);
            return current;
        }

        public static IPredicateValueRuleBuilder All(this IValueRuleBuilder builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAllToken(current._contract, func);
            return current;
        }

        public static IPredicateValueRuleBuilder NotAny(this IValueRuleBuilder builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAllToken(current._contract, func);
            return current;
        }

        public static IPredicateValueRuleBuilder NotAll(this IValueRuleBuilder builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAnyToken(current._contract, func);
            return current;
        }

        public static IPredicateValueRuleBuilder None(this IValueRuleBuilder builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNoneToken(current._contract, func);
            return current;
        }
        
        //`1

         public static IPredicateValueRuleBuilder<T> Any<T>(this IValueRuleBuilder<T> builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAnyToken(current._contract, func);
            return current;
        }
        
         public static IPredicateValueRuleBuilder<T> All<T>(this IValueRuleBuilder<T> builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAllToken(current._contract, func);
            return current;
        }
        
         public static IPredicateValueRuleBuilder<T> NotAny<T>(this IValueRuleBuilder<T> builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAllToken(current._contract, func);
            return current;
        }
        
         public static IPredicateValueRuleBuilder<T> NotAll<T>(this IValueRuleBuilder<T> builder,Func<object, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAnyToken(current._contract, func);
            return current;
        }
        
         public static IPredicateValueRuleBuilder<T> None<T>(this IValueRuleBuilder<T> builder,Func<object, bool> func)
         {
             var current = builder._impl();
             current.State.CurrentToken = new ValueNoneToken(current._contract, func);
             return current;
         }
         
         //`2

        /// <summary>
        /// Any
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TItem[]> Any<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAnyToken<TItem[], TItem>(current._contract, func);
            return current;
        }

        /// <summary>
        /// Any
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> Any<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAnyToken<TVal, TItem>(current._contract, func);
            return current;
        }

        /// <summary>
        /// All
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TItem[]> All<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAllToken<TItem[], TItem>(current._contract, func);
            return current;
        }

        /// <summary>
        /// All
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> All<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueAllToken<TVal, TItem>(current._contract, func);
            return current;
        }

        /// <summary>
        /// Not any
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TItem[]> NotAny<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            return builder.All(func);
        }

        /// <summary>
        /// Not any
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotAny<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            return builder.All(func);
        }

        /// <summary>
        /// Not all
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TItem[]> NotAll<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            return builder.Any(func);
        }

        /// <summary>
        /// Not all
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotAll<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            return builder.Any(func);
        }

        /// <summary>
        /// None
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TItem[]> None<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNoneToken<TItem[], TItem>(current._contract, func);
            return current;
        }

        /// <summary>
        /// None
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> None<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNoneToken<TVal, TItem>(current._contract, func);
            return current;
        }

        #endregion

        #region In/NotIn

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder In(this IValueRuleBuilder builder, ICollection<object> collection)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder In(this IValueRuleBuilder builder, Func<ICollection<object>> collectionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder In(this IValueRuleBuilder builder, params object[] objects)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken(current._contract, objects);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> In<T>(this IValueRuleBuilder<T> builder, ICollection<object> collection)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> In<T>(this IValueRuleBuilder<T> builder, Func<ICollection<object>> collectionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> In<T>(this IValueRuleBuilder<T> builder, params object[] objects)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken(current._contract, objects);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> In<T, TVal>(this IValueRuleBuilder<T, TVal> builder, ICollection<TVal> collection)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken<TVal>(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> In<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<ICollection<TVal>> collectionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken<TVal>(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> In<T, TVal>(this IValueRuleBuilder<T, TVal> builder, params TVal[] objects)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken<TVal>(current._contract, objects);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> In<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, ICollection<TItem> collection)
            where TVal : IEnumerable<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken<TVal, TItem>(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> In<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<ICollection<TItem>> collectionFunc)
            where TVal : IEnumerable<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken<TVal, TItem>(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> In<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, params TItem[] objects)
            where TVal : IEnumerable<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken<TVal, TItem>(current._contract, objects);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder NotIn(this IValueRuleBuilder builder, ICollection<object> collection)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder NotIn(this IValueRuleBuilder builder, Func<ICollection<object>> collectionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder NotIn(this IValueRuleBuilder builder, params object[] objects)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken(current._contract, objects);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> NotIn<T>(this IValueRuleBuilder<T> builder, Func<ICollection<object>> collectionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> NotIn<T>(this IValueRuleBuilder<T> builder, ICollection<object> collection)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> NotIn<T>(this IValueRuleBuilder<T> builder, params object[] objects)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken(current._contract, objects);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotIn<T, TVal>(this IValueRuleBuilder<T, TVal> builder, ICollection<TVal> collection)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken<TVal>(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotIn<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<ICollection<TVal>> collectionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken<TVal>(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotIn<T, TVal>(this IValueRuleBuilder<T, TVal> builder, params TVal[] objects)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueInToken<TVal>(current._contract, objects);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collectionFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotIn<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<ICollection<TItem>> collectionFunc)
            where TVal : IEnumerable<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken<TVal, TItem>(current._contract, collectionFunc);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotIn<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, ICollection<TItem> collection)
            where TVal : IEnumerable<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken<TVal, TItem>(current._contract, collection);
            return current;
        }

        /// <summary>
        /// Determine whether the value is not included in the given set.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> NotIn<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, params TItem[] objects)
            where TVal : IEnumerable<TItem>
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotInToken<TVal, TItem>(current._contract, objects);
            return current;
        }

        #endregion
    }
}