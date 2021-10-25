namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May register rule package interface <br />
    /// 标记可为规则包注册
    /// </summary>
    public interface IMayRegisterRulePackage
    {
        /// <summary>
        /// Register for rule package <br />
        /// 注册规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValidationRegistrar ForRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for rule package <br />
        /// 注册规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValidationRegistrar ForRulePackage(VerifyRulePackage package, string name, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}