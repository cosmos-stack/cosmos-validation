using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals
{
    internal class VerifiableOpsContext
    {
        public VerifiableOpsMode OpsMode { get; set; }

        public VerifiableObjectContext VerifiableObjectContext { get; set; }

        public VerifiableMemberContext VerifiableMemberContext { get; set; }

        public List<string> RouteGroupNames { get; } = new();

        public Dictionary<string, List<CorrectVerifyVal>> VerifyValDictionary { get; set; } = new();

        public List<string> NameOfExecutedRuleList { get; set; } = new();

        public void AppendVerifyVal(string memberName, CorrectVerifyVal val)
        {
            if (string.IsNullOrWhiteSpace(memberName) || val is null) return;

            if (VerifyValDictionary.TryGetValue(memberName, out var list))
            {
                list.Add(val);
            }
            else
            {
                list = new List<CorrectVerifyVal> {val};
                VerifyValDictionary[memberName] = list;
            }
        }

        public void AppendNameOfExecutedRule(string nameOfExecutedRule)
        {
            NameOfExecutedRuleList.Add(nameOfExecutedRule);
        }

        public bool IncludeFailures
        {
            get
            {
                return VerifyValDictionary.Any() &&
                       VerifyValDictionary.Values.Any(x => x.Any(y => y.IsSuccess == false));
            }
        }

        public static VerifiableOpsContext Create(VerifiableObjectContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            return new VerifiableOpsContext
            {
                OpsMode = VerifiableOpsMode.Object,
                VerifiableObjectContext = context
            };
        }

        public static VerifiableOpsContext Create(VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            return new VerifiableOpsContext
            {
                OpsMode = VerifiableOpsMode.Member,
                VerifiableMemberContext = context
            };
        }
    }
}