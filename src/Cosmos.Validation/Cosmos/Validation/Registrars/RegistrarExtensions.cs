using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Registrars
{
    internal static class RegistrarExtensions
    {
        public static ValueValidationRegistrar<T, TVal> _impl<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> builder)
        {
            return (ValueValidationRegistrar<T, TVal>) builder;
        }

        public static ValueValidationRegistrar<T> _impl<T>(this IValueFluentValidationRegistrar<T> builder)
        {
            return (ValueValidationRegistrar<T>) builder;
        }

        public static ValueValidationRegistrar _impl(this IValueFluentValidationRegistrar builder)
        {
            return (ValueValidationRegistrar) builder;
        }
    }
    
    public static partial class ValueValidationRegistrarExtensions
    {
        

        #region Any/All/NotAny/NotAll/None

        public static IValueFluentValidationRegistrar<T, TItem[]> Any<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> Any<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().Any(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> All<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> All<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            registrar._impl().ExposeValueRuleBuilder2().All(func);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> NotAny<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            return registrar.All(func);
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotAny<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            return registrar.All(func);
        }

        public static IValueFluentValidationRegistrar<T, TItem[]> NotAll<T, TItem>(this IValueFluentValidationRegistrar<T, TItem[]> registrar, Func<TItem, bool> func)
        {
            return registrar.Any(func);
        }

        public static IValueFluentValidationRegistrar<T, TVal> NotAll<T, TVal, TItem>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            return registrar.Any(func);
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

        #region WithMessage

        public static IValueFluentValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return registrar;
        }

        public static IValueFluentValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message);
            return registrar;
        }

        public static IValueFluentValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message, appendOrOverwrite);
            return registrar;
        }

        #endregion
    }
}