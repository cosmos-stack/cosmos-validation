namespace CosmosStack.Validation
{
    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information. <br />
    /// 值验证规则构造器接口，此接口可用于期待一个错误消息
    /// </summary>
    public interface IWaitForMessageValueRuleBuilder
    {
        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValueRuleBuilder WithName(string operationName);

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information. <br />
    /// 值验证规则构造器接口，此接口可用于期待一个错误消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWaitForMessageValueRuleBuilder<T>
    {
        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValueRuleBuilder<T> WithName(string operationName);

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information. <br />
    /// 值验证规则构造器接口，此接口可用于期待一个错误消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IWaitForMessageValueRuleBuilder<T, TVal>
    {
        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValueRuleBuilder<T, TVal> WithName(string operationName);

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T, TVal> WithMessage(string message);
    }
}