using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Tokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRule
    {
        public string MemberName { get; set; }
        
        public ObjectValueContract Contract { get; set; }

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

        public IEnumerable<CorrectVerifyVal> ValidValue(ObjectContext context)
        {
            return Tokens.Select(token => token.ValidValue(context.GetValue(MemberName)));
        }
        
        public IEnumerable<CorrectVerifyVal> ValidValue(ObjectValueContext context)
        {
            return Tokens.Select(token => token.ValidValue(context));
        }

        public ObjectValueContext GetValue(ObjectContext context)
        {
            return context.GetValue(MemberName);
        }
    }

    internal class CorrectValueRule<TVal> : CorrectValueRule { }
}