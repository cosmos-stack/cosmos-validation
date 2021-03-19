using Cosmos.Validation.Internals.Conditions;

namespace Cosmos.Validation.Internals.Tokens
{
    /// <summary>
    /// Interface of grouped value token
    /// </summary>
    internal interface IGroupedValueToken : IValueToken
    {
        string MemberName { get; }
        
        ConditionOps OpsForNext { get; set; }
        
        void Next(VerifiableOpsContext context);

        // CorrectValueRuleMode Mode { get; }
        //
        // bool Verify(VerifiableOpsContext context, out List<CorrectVerifyVal> correctVerifyValSet);
    }
}