using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Internals.Tokens.ValueTokens;

namespace Cosmos.Validation
{
    public static class ValueRuleBuilderExtensions
    {
        private static CorrectValueRuleBuilder<T, TVal> _impl<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            return (CorrectValueRuleBuilder<T, TVal>) builder;
        }

        private static CorrectValueRuleBuilder<T> _impl<T>(this IValueRuleBuilder<T> builder)
        {
            return (CorrectValueRuleBuilder<T>) builder;
        }

        private static CorrectValueRuleBuilder _impl(this IValueRuleBuilder builder)
        {
            return (CorrectValueRuleBuilder) builder;
        }

        #region Any/All/NotAny/NotAll/None

        public static IValueRuleBuilder<T, TItem[]> Any<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current.CurrentToken = new ValueAnyToken<TItem[], TItem>(current.Contract, func);
            return builder;
        }

        public static IValueRuleBuilder<T, TVal> Any<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current.CurrentToken = new ValueAnyToken<TVal, TItem>(current.Contract, func);
            return builder;
        }

        public static IValueRuleBuilder<T, TItem[]> All<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current.CurrentToken = new ValueAllToken<TItem[], TItem>(current.Contract, func);
            return builder;
        }

        public static IValueRuleBuilder<T, TVal> All<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current.CurrentToken = new ValueAllToken<TVal, TItem>(current.Contract, func);
            return builder;
        }

        public static IValueRuleBuilder<T, TItem[]> NotAny<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
            => builder.All(func);

        public static IValueRuleBuilder<T, TVal> NotAny<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
            => builder.All(func);

        public static IValueRuleBuilder<T, TItem[]> NotAll<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
            => builder.Any(func);

        public static IValueRuleBuilder<T, TVal> NotAll<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
            => builder.Any(func);

        public static IValueRuleBuilder<T, TItem[]> None<T, TItem>(this IValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current.CurrentToken = new ValueNoneToken<TItem[], TItem>(current.Contract, func);
            return builder;
        }

        public static IValueRuleBuilder<T, TVal> None<T, TVal, TItem>(this IValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current.CurrentToken = new ValueNoneToken<TVal, TItem>(current.Contract, func);
            return builder;
        }

        #endregion
        
        #region WithMessage

        public static IValueRuleBuilder WithMessage(this IValueRuleBuilder builder, string message)
        {
            return builder.WithMessage(message, true);
        }

        public static IValueRuleBuilder WithMessage(this IValueRuleBuilder builder, string message, bool appendOrOverwrite)
        {
            var current = builder._impl().CurrentToken;

            if (current != null)
            {
                if (current.WithMessageMode)
                {
                    if (appendOrOverwrite)
                        current.CustomMessage += message;
                    else
                        current.CustomMessage = message;
                }
                else
                {
                    current.CustomMessage = message;
                    current.AppendOrOverwrite = appendOrOverwrite;
                    current.WithMessageMode = true;
                }
            }

            return builder;
        }

        public static IValueRuleBuilder<T> WithMessage<T>(this IValueRuleBuilder<T> builder, string message)
        {
            return builder.WithMessage(message, true);
        }

        public static IValueRuleBuilder<T> WithMessage<T>(this IValueRuleBuilder<T> builder, string message, bool appendOrOverwrite)
        {
            var current = builder._impl().CurrentToken;

            if (current != null)
            {
                if (current.WithMessageMode)
                {
                    if (appendOrOverwrite)
                        current.CustomMessage += message;
                    else
                        current.CustomMessage = message;
                }
                else
                {
                    current.CustomMessage = message;
                    current.AppendOrOverwrite = appendOrOverwrite;
                    current.WithMessageMode = true;
                }
            }

            return builder;
        }

        public static IValueRuleBuilder<T, TVal> WithMessage<T, TVal>(this IValueRuleBuilder<T, TVal> builder, string message)
        {
            return builder.WithMessage(message, true);
        }

        public static IValueRuleBuilder<T, TVal> WithMessage<T, TVal>(this IValueRuleBuilder<T, TVal> builder, string message, bool appendOrOverwrite)
        {
            var current = builder._impl().CurrentToken;

            if (current != null)
            {
                if (current.WithMessageMode)
                {
                    if (appendOrOverwrite)
                        current.CustomMessage += message;
                    else
                        current.CustomMessage = message;
                }
                else
                {
                    current.CustomMessage = message;
                    current.AppendOrOverwrite = appendOrOverwrite;
                    current.WithMessageMode = true;
                }
            }

            return builder;
        }

        #endregion
    }
}