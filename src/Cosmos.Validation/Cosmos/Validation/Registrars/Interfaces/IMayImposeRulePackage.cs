namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayImposeRulePackage
    {
        IValidationRegistrar ForRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);

        IValidationRegistrar ForRulePackage(VerifyRulePackage package, string name, VerifyRuleMode mode = VerifyRuleMode.Append);
    }

    public interface IMayContinueImposeRulePackage
    {
        IFluentValidationRegistrar AndForRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
        
        IFluentValidationRegistrar AndForRulePackage(VerifyRulePackage package, string name, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}