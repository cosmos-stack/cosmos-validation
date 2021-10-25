namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May use rule package for type interface <br />
    /// 标记可对类使用规则包
    /// </summary>
    public interface IMayUseRulePackageForType
    {
        /// <summary>
        /// With rule package <br />
        /// 使用规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IFluentValidationRegistrar WithRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}