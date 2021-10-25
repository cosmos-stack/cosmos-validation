using System;
using System.Text.RegularExpressions;
using CosmosStack.Validation.Internals.Rules;
using CosmosStack.Validation.Internals.Tokens.ValueTokens;

// ReSharper disable once CheckNamespace
namespace CosmosStack.Validation
{
    public static partial class CorrectValueRuleExtensions
    {
        #region Matches

        //`0

        public static IPredicateValueRuleBuilder Matches(this IValueRuleBuilder builder, Regex regex)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regex);
            return current;
        }

        public static IPredicateValueRuleBuilder Matches(this IValueRuleBuilder builder, string regexExpression)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regexExpression);
            return current;
        }

        public static IPredicateValueRuleBuilder Matches(this IValueRuleBuilder builder, string regexExpression, RegexOptions options)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regexExpression, options);
            return current;
        }

        public static IPredicateValueRuleBuilder Matches(this IValueRuleBuilder builder, Func<object, Regex> regexFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regexFunc);
            return current;
        }

        public static IPredicateValueRuleBuilder Matches(this IValueRuleBuilder builder, Func<object, string> regexExpressionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regexExpressionFunc);
            return current;
        }

        public static IPredicateValueRuleBuilder Matches(this IValueRuleBuilder builder, Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regexExpressionFunc, options);
            return current;
        }

        //`1

        public static IPredicateValueRuleBuilder<T> Matches<T>(this IValueRuleBuilder<T> builder, Regex regex)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regex);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> Matches<T>(this IValueRuleBuilder<T> builder, string regexExpression)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexExpression);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> Matches<T>(this IValueRuleBuilder<T> builder, string regexExpression, RegexOptions options)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexExpression, options);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> Matches<T>(this IValueRuleBuilder<T> builder, Func<T, Regex> regexFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexFunc);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> Matches<T>(this IValueRuleBuilder<T> builder, Func<T, string> regexExpressionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexExpressionFunc);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> Matches<T>(this IValueRuleBuilder<T> builder, Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexExpressionFunc, options);
            return current;
        }

        //`2

        public static IPredicateValueRuleBuilder<T, TVal> Matches<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Regex regex)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regex);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> Matches<T, TVal>(this IValueRuleBuilder<T, TVal> builder, string regexExpression)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regexExpression);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> Matches<T, TVal>(this IValueRuleBuilder<T, TVal> builder, string regexExpression, RegexOptions options)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken(current._contract, regexExpression, options);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> Matches<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<T, Regex> regexFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexFunc);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> Matches<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<T, string> regexExpressionFunc)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexExpressionFunc);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> Matches<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRegularExpressionToken<T>(current._contract, regexExpressionFunc, options);
            return current;
        }

        #endregion

        #region Func

        //`0

        public static IPredicateValueRuleBuilder Func(this IValueRuleBuilder builder, Func<object, CustomVerifyResult> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueFuncToken(current._contract, func);
            return current;
        }

        public static IWaitForMessageValueRuleBuilder Func(this IValueRuleBuilder builder, Func<object, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder(current, func);
        }

        //`1

        public static IPredicateValueRuleBuilder<T> Func<T>(this IValueRuleBuilder<T> builder, Func<object, CustomVerifyResult> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueFuncToken(current._contract, func);
            return current;
        }

        public static IWaitForMessageValueRuleBuilder<T> Func<T>(this IValueRuleBuilder<T> builder, Func<object, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T>(current, func);
        }

        //`2

        public static IPredicateValueRuleBuilder<T, TVal> Func<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal, CustomVerifyResult> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueFuncToken<TVal>(current._contract, func);
            return current;
        }

        public static IWaitForMessageValueRuleBuilder<T, TVal> Func<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(current, func);
        }

        #endregion

        #region Predicate

        //`0

        public static IWaitForMessageValueRuleBuilder Predicate(this IValueRuleBuilder builder, Predicate<object> predicate)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder(current, predicate);
        }

        //`1

        public static IWaitForMessageValueRuleBuilder<T> Predicate<T>(this IValueRuleBuilder<T> builder, Predicate<object> predicate)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T>(current, predicate);
        }

        //`2

        public static IWaitForMessageValueRuleBuilder<T, TVal> Predicate<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Predicate<TVal> predicate)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(current, predicate);
        }

        #endregion

        #region Must

        //`0

        public static IPredicateValueRuleBuilder Must(this IValueRuleBuilder builder, Func<object, CustomVerifyResult> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueFuncToken(current._contract, func);
            return current;
        }

        public static IWaitForMessageValueRuleBuilder Must(this IValueRuleBuilder builder, Func<object, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder(current, func);
        }

        //`1

        public static IPredicateValueRuleBuilder<T> Must<T>(this IValueRuleBuilder<T> builder, Func<object, CustomVerifyResult> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueFuncToken(current._contract, func);
            return current;
        }

        public static IWaitForMessageValueRuleBuilder<T> Must<T>(this IValueRuleBuilder<T> builder, Func<object, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T>(current, func);
        }

        //`2

        public static IPredicateValueRuleBuilder<T, TVal> Must<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal, CustomVerifyResult> func)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueFuncToken<TVal>(current._contract, func);
            return current;
        }

        public static IWaitForMessageValueRuleBuilder<T, TVal> Must<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(current, func);
        }

        #endregion

        #region Satisfies

        //`0

        public static IWaitForMessageValueRuleBuilder Satisfies(this IValueRuleBuilder builder, Func<object, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder(current, func);
        }

        public static IPredicateValueRuleBuilder Satisfies(this IValueRuleBuilder builder, Func<object, bool> func, string message)
        {
            return Satisfies(builder, func).WithMessage(message);
        }

        //`1

        public static IWaitForMessageValueRuleBuilder<T> Satisfies<T>(this IValueRuleBuilder<T> builder, Func<object, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T>(current, func);
        }

        public static IPredicateValueRuleBuilder<T> Satisfies<T>(this IValueRuleBuilder<T> builder, Func<object, bool> func, string message)
        {
            return Satisfies(builder, func).WithMessage(message);
        }


        //`2

        public static IWaitForMessageValueRuleBuilder<T, TVal> Satisfies<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal, bool> func)
        {
            var current = builder._impl();
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(current, func);
        }

        public static IPredicateValueRuleBuilder<T, TVal> Satisfies<T, TVal>(this IValueRuleBuilder<T, TVal> builder, Func<TVal, bool> func, string message)
        {
            return Satisfies(builder, func).WithMessage(message);
        }

        #endregion
    }
}