namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May use rule conditions interface <br />
    /// 标记可使用条件
    /// </summary>
    public interface IMayUseRuleConditions
    {
        /// <summary>
        /// And <br />
        /// 与
        /// </summary>
        /// <returns></returns>
        IValueFluentValidationRegistrar And();

        /// <summary>
        /// Or <br />
        /// 或
        /// </summary>
        /// <returns></returns>
        IValueFluentValidationRegistrar Or();
    }

    /// <summary>
    /// May use rule conditions interface <br />
    /// 标记可使用条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMayUseRuleConditions<T>
    {
        /// <summary>
        /// And <br />
        /// 与
        /// </summary>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> And();

        /// <summary>
        /// Or <br />
        /// 或
        /// </summary>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> Or();
    }

    /// <summary>
    /// May use rule conditions interface <br />
    /// 标记可使用条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IMayUseRuleConditions<T, TVal>
    {
        /// <summary>
        /// And <br />
        /// 与
        /// </summary>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> And();

        /// <summary>
        /// Or <br />
        /// 或
        /// </summary>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> Or();
    }
}