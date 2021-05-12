namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayRegisterRulePackage
    {
        IValidationRegistrar ForRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);

        IValidationRegistrar ForRulePackage(VerifyRulePackage package, string name, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}