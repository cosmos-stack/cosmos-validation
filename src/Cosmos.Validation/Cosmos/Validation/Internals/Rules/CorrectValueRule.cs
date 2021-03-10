using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Tokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRule
    {
        public string MemberName { get; set; }

        public VerifiableMemberContract Contract { get; set; }

        public CorrectValueRuleMode Mode { get; set; }

        public List<IValueToken> Tokens { get; set; }

        public void Merge(CorrectValueRule rule)
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

        public IEnumerable<CorrectVerifyVal> ValidValue(VerifiableObjectContext context)
        {
            return Tokens.Select(token => token.Valid(context));
        }

        public IEnumerable<CorrectVerifyVal> ValidValue(VerifiableMemberContext context)
        {
            return Tokens.Select(token => token.Valid(context));
        }

        public VerifiableMemberContext GetValue(VerifiableObjectContext context)
        {
            return context.GetValue(MemberName);
        }
    }

    internal class CorrectValueRule<TVal> : CorrectValueRule { }
}