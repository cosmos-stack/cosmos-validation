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
        private readonly LogicCorrectValueRule _correctValueRule;

        private List<IValueToken> WorkingList { get; set; }

        public GroupedValueToken(VerifiableMemberContract contract, bool internalLogic = true)
        {
            _contract = contract;
            WorkingList = new List<IValueToken>();
            _correctValueRule = new LogicCorrectValueRule {Contract = contract, MemberName = MemberName, Mode = CorrectValueRuleMode.Append, Tokens = WorkingList};
            TokenName = CreateName(_contract);
            Relationship = ConditionOps.Break;
            LogicFlag = internalLogic;
        }

        public GroupedValueToken(VerifiableMemberContract contract, List<IValueToken> tokens, bool internalLogic = true)
        {
            _contract = contract;
            WorkingList = new List<IValueToken>();
            WorkingList.AddRange(tokens);
            _correctValueRule = new LogicCorrectValueRule {Contract = contract, MemberName = MemberName, Mode = CorrectValueRuleMode.Append, Tokens = WorkingList};
            TokenName = CreateName(_contract);
            Relationship = ConditionOps.Break;
            LogicFlag = internalLogic;
        }

        public string TokenName { get; }

        public TokenClass TokenClass => TokenClass.GroupedToken;

        public bool MutuallyExclusive => false;

        public int[] MutuallyExclusiveFlags => ValueToken.NoMutuallyExclusiveFlags;

        public string MemberName => _contract.MemberName;

        private ConditionOps _relationship;

        public ConditionOps Relationship
        {
            get => NextNode is null ? ConditionOps.Break : _relationship;
            set => _relationship = value;
        }

        public bool LogicFlag
        {
            get => _correctValueRule.InternalLogic;
            set => _correctValueRule.InternalLogic = value;
        }

        public IGroupedValueToken NextNode { get; set; }

        public void Next(VerifiableOpsContext context, out bool valid)
        {
            // Avoid getting stuck in an infinite loop.
            if (context.RouteGroupNames.Contains(TokenName))
            {
                valid = Relationship != ConditionOps.Or; // 返回一个无关最终结果的值
                return;
            }

            _correctValueRule.Verify(context);

            context.RouteGroupNames.Add(TokenName);

            valid = !context.IncludeFailures;

            if (Relationship == ConditionOps.Break)
                return;

            //false && ... -> false
            if (!valid && Relationship == ConditionOps.And)
                return;

            //true || ... -> true
            if (valid && Relationship == ConditionOps.Or)
                return;

            //current node is the tail of the validation chain
            if (NextNode is null)
                return;

            if (ConditionOpsHelper.IsHigherPriority(context.ConditionMode, Relationship))
                context.RaiseScope(MemberName, Relationship);

            context.ConditionMode = Relationship == ConditionOps.Or ? ConditionOps.Or : ConditionOps.And;

            valid = NextNode.Verify(context);
        }

        public bool Verify(VerifiableOpsContext context)
        {
            Next(context, out var valid);
            return valid;
        }

        public void AppendToken(IValueToken token)
        {
            if (token is not null)
            {
                WorkingList.Add(token);
            }
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