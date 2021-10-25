namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Message registrar extensions <br />
    /// 消息注册扩展
    /// </summary>
    public static class MessageRegistrarExtensions
    {
        /// <summary>
        /// With message <br />
        /// 使用消息
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// With message <br />
        /// 使用消息
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="message"></param>
        /// <param name="appendOrOverwrite"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar WithMessage(this IValueFluentValidationRegistrar registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// With message <br />
        /// 使用消息
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// With message <br />
        /// 使用消息
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="message"></param>
        /// <param name="appendOrOverwrite"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> WithMessage<T>(this IValueFluentValidationRegistrar<T> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder().WithMessage(message, appendOrOverwrite);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// With message <br />
        /// 使用消息
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// With message <br />
        /// 使用消息
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="message"></param>
        /// <param name="appendOrOverwrite"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> WithMessage<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, string message, bool appendOrOverwrite)
        {
            registrar._impl().ExposeValueRuleBuilder2().WithMessage(message, appendOrOverwrite);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }
    }
}