using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Conditions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals
{
    internal class VerifiableOpsContext
    {
        public VerifiableOpsMode OpsMode { get; set; }

        public ConditionOps ConditionMode { get; set; }

        public VerifiableObjectContext VerifiableObjectContext { get; set; }

        public VerifiableMemberContext VerifiableMemberContext { get; set; }

        public List<string> RouteGroupNames { get; } = new();

        public Dictionary<string, CorrectVerifyValState> VerifyValDictionary { get; set; } = new();

        public List<string> NameOfExecutedRuleList { get; set; } = new();

        public void AppendVerifyVal(string memberName, CorrectVerifyVal val)
        {
            if (string.IsNullOrWhiteSpace(memberName) || val is null) return;
            
            if (VerifyValDictionary.TryGetValue(memberName, out var state))
            {
                state.Append(val);
            }
            else
            {
                state = new CorrectVerifyValState();
                state.Append(val);
                VerifyValDictionary[memberName] = state;
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
                       VerifyValDictionary.Values.Any(x => x.IncludeFailures);
            }
        }

        public void RaiseScope(string memberName, ConditionOps ops)
        {
            if (string.IsNullOrWhiteSpace(memberName)) return;

            if (VerifyValDictionary.TryGetValue(memberName, out var state))
            {
                var localOps = ops switch
                {
                    ConditionOps.Break => CorrectVerifyValOps.AndOp,
                    ConditionOps.And => CorrectVerifyValOps.AndOp,
                    ConditionOps.Or => CorrectVerifyValOps.OrOp,
                    _ => CorrectVerifyValOps.AndOp
                };

                state.Ops(localOps);
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