﻿using System.Collections.Generic;
using CosmosStack.Validation.Internals.Tokens;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Rules
{
    internal class CorrectValueRule
    {
        public string MemberName { get; set; }

        public VerifiableMemberContract Contract { get; set; }

        public CorrectValueRuleMode Mode { get; set; }

        public List<IValueToken> Tokens { get; set; }

        public virtual void Merge(CorrectValueRule rule)
        {
            if (rule is null)
                return;

            if (MemberName != rule.MemberName)
                return;

            if (rule.Mode != CorrectValueRuleMode.Append)
                return;

            foreach (var token in rule.Tokens)
            {
                if (token.MutuallyExclusive)
                    continue;

                if (!TokenMutexCalculator.Available(Tokens, token))
                    continue;

                Tokens.Add(token);
            }
        }

        public virtual void Verify(VerifiableOpsContext context)
        {
            Tokens.ForEach(token => token.Verify(context));
        }

        public CorrectValueRule Clone()
        {
            return new()
            {
                MemberName = MemberName,
                Contract = Contract,
                Mode = Mode,
                Tokens = Tokens
            };
        }
    }
}