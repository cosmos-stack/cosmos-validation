namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayUseMemberRulePackage
    {
        IValueFluentValidationRegistrar WithMemberRulePackage(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
    
    public interface IMayUseMemberRulePackage<T>
    {
        IValueFluentValidationRegistrar<T> WithMemberRulePackage(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
    
    public interface IMayUseMemberRulePackage<T, TVal>
    {
        IValueFluentValidationRegistrar<T, TVal> WithMemberRulePackage(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}