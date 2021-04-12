// ReSharper disable once CheckNamespace
namespace Cosmos.Validation
{
    public static partial class CorrectValueRuleExtensions
    {
        #region WithMessage

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IValueRuleBuilder WithMessage(this IValueRuleBuilder builder, string message)
        {
            return builder.WithMessage(message, true);
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="message"></param>
        /// <param name="appendOrOverwrite"></param>
        /// <returns></returns>
        public static IValueRuleBuilder WithMessage(this IValueRuleBuilder builder, string message, bool appendOrOverwrite)
        {
            var current = builder._impl().State.CurrentToken;

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

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IValueRuleBuilder<T> WithMessage<T>(this IValueRuleBuilder<T> builder, string message)
        {
            return builder.WithMessage(message, true);
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="message"></param>
        /// <param name="appendOrOverwrite"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IValueRuleBuilder<T> WithMessage<T>(this IValueRuleBuilder<T> builder, string message, bool appendOrOverwrite)
        {
            var current = builder._impl().State.CurrentToken;

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

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IValueRuleBuilder<T, TVal> WithMessage<T, TVal>(this IValueRuleBuilder<T, TVal> builder, string message)
        {
            return builder.WithMessage(message, true);
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="message"></param>
        /// <param name="appendOrOverwrite"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IValueRuleBuilder<T, TVal> WithMessage<T, TVal>(this IValueRuleBuilder<T, TVal> builder, string message, bool appendOrOverwrite)
        {
            var current = builder._impl().State.CurrentToken;

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