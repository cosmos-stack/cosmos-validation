using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Conditions;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens
{
    /// <summary>
    /// Grouped value token.
    /// </summary>
    internal class GroupedValueToken : IGroupedValueToken
    {
        private readonly VerifiableMemberContract _contract;
        private readonly CorrectValueRule _correctValueRule;

        public GroupedValueToken(VerifiableMemberContract contract, List<IValueToken> tokens)
        {
            _contract = contract;
            _correctValueRule = new CorrectValueRule {Contract = contract, MemberName = MemberName, Mode = CorrectValueRuleMode.Append, Tokens = tokens};
            TokenName = CreateName(_contract);
            OpsForNext = ConditionOps.Break;
        }

        public string TokenName { get; }

        public TokenClass TokenClass => TokenClass.GroupedToken;

        public bool MutuallyExclusive => false;

        public int[] MutuallyExclusiveFlags => ValueToken.NoMutuallyExclusiveFlags;

        public string MemberName => _contract.MemberName;

        public ConditionOps OpsForNext { get; set; }

        public IGroupedValueToken NextNode { get; set; }

        public void Next(VerifiableOpsContext context)
        {
            // Avoid getting stuck in an infinite loop.
            if (context.RouteGroupNames.Contains(TokenName))
                return;

            _correctValueRule.Verify(context);

            context.RouteGroupNames.Add(TokenName);

            var valid = !context.IncludeFailures;

            if (OpsForNext == ConditionOps.Break)
                return;

            //false && ... -> false
            if (!valid && OpsForNext == ConditionOps.And)
                return;

            //true || ... -> true
            if (valid && OpsForNext == ConditionOps.Or)
                return;

            //current node is the tail of the validation chain
            if (NextNode is null)
                return;

            if (ConditionOpsHelper.IsHigherPriority(context.ConditionMode, OpsForNext))
                context.RaiseScope(MemberName, OpsForNext);

            context.ConditionMode = OpsForNext;

            NextNode.Verify(context);
        }

        public void Verify(VerifiableOpsContext context)
        {
            Next(context);
        }

        public string CustomMessage { get; set; }
        public bool WithMessageMode { get; set; }
        public bool AppendOrOverwrite { get; set; }

        private static string CreateName(VerifiableMemberContract contract)
        {
            return $"GroupedValueToken_[{contract.MemberName}]${HashCodeUtil.GetHashCode(new object[] {contract, DateTime.Now.Ticks})}";
        }
    }
}