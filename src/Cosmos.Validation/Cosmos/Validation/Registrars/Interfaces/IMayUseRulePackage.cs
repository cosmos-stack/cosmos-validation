namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayUseRulePackageForType
    {
        IFluentValidationRegistrar WithRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}