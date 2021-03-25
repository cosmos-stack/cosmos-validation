namespace Cosmos.Validation
{
    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information.
    /// </summary>
    public interface IWaitForMessageValueRuleBuilder
    {
        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWaitForMessageValueRuleBuilder<T>
    {
        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IWaitForMessageValueRuleBuilder<T, TVal>
    {
        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T, TVal> WithMessage(string message);
    }
}