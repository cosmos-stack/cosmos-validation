using System;
using System.Text.RegularExpressions;

namespace Cosmos.Validation.Registrars
{
    public static class MatchesRegistrarExtensions
    {
        public static IPredicateValidationRegistrar Matches(this IValueFluentValidationRegistrar registrar, Regex regex)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(regex);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar Matches(this IValueFluentValidationRegistrar registrar, string regexExpression)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(regexExpression);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar Matches(this IValueFluentValidationRegistrar registrar, string regexExpression, RegexOptions options)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(regexExpression, options);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar Matches(this IValueFluentValidationRegistrar registrar, Func<object, string> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar Matches(this IValueFluentValidationRegistrar registrar, Func<object, Regex> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(func);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar Matches(this IValueFluentValidationRegistrar registrar, Func<object, string> func, RegexOptions options)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(func, options);
            return (IPredicateValidationRegistrar) registrar;
        }

        public static IPredicateValidationRegistrar<T> Matches<T>(this IValueFluentValidationRegistrar<T> registrar, Regex regex)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(regex);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> Matches<T>(this IValueFluentValidationRegistrar<T> registrar, string regexExpression)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(regexExpression);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> Matches<T>(this IValueFluentValidationRegistrar<T> registrar, string regexExpression, RegexOptions options)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(regexExpression, options);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> Matches<T>(this IValueFluentValidationRegistrar<T> registrar, Func<T, string> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(func);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> Matches<T>(this IValueFluentValidationRegistrar<T> registrar, Func<T, Regex> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(func);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T> Matches<T>(this IValueFluentValidationRegistrar<T> registrar, Func<T, string> func, RegexOptions options)
        {
            registrar._impl().ExposeValueRuleBuilder().Matches(func, options);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Matches<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Regex regex)
        {
            registrar._impl().ExposeValueRuleBuilder2().Matches(regex);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Matches<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string regexExpression)
        {
            registrar._impl().ExposeValueRuleBuilder2().Matches(regexExpression);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Matches<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string regexExpression, RegexOptions options)
        {
            registrar._impl().ExposeValueRuleBuilder2().Matches(regexExpression, options);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Matches<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<T, string> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Matches(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Matches<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<T, Regex> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Matches(func);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }

        public static IPredicateValidationRegistrar<T, TVal> Matches<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<T, string> func, RegexOptions options)
        {
            registrar._impl().ExposeValueRuleBuilder2().Matches(func, options);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }
    }
}