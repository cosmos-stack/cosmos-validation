namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May use member rule package interface <br />
    /// 标记可用于成员规则包
    /// </summary>
    public interface IMayUseMemberRulePackage
    {
        /// <summary>
        /// With member rule package <br />
        /// 使用成员规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar WithMemberRulePackage(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
    
    /// <summary>
    /// May use member rule package interface <br />
    /// 标记可用于成员规则包
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMayUseMemberRulePackage<T>
    {
        /// <summary>
        /// With member rule package <br />
        /// 使用成员规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> WithMemberRulePackage(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
    
    /// <summary>
    /// May use member rule package interface <br />
    /// 标记可用于成员规则包
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IMayUseMemberRulePackage<T, TVal>
    {
        /// <summary>
        /// With member rule package <br />
        /// 使用成员规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> WithMemberRulePackage(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}