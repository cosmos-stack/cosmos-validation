using System.Collections.Generic;
using System.Linq;
using CosmosStack.Reflection;
using CosmosStack.Validation.Internals.Rules;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Validators;

// ReSharper disable PossibleMultipleEnumeration

namespace CosmosStack.Validation.Internals
{
    internal static class CorrectEngine
    {
        public static VerifyResult Valid(VerifiableOpsContext context, List<CorrectValueRule> rules)
        {
            var len = rules.Count;

            for (var i = 0; i < len; i++)
            {
                rules[i].Verify(context);
            }

            return MakeMainResult(context.VerifyValDictionary.ConvertToFailures(), context.NameOfExecutedRuleList);
        }

        public static VerifyResult ValidOne(VerifiableOpsContext context, List<CorrectValueRule> rules)
        {
            return Valid(context, rules);
        }

        public static VerifyResult ValidMany(IDictionary<string, VerifiableMemberContext> keyValueCollections, List<CorrectValueRule> rules)
        {
            var len = rules.Count;
            var failures = new List<VerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];

                if (keyValueCollections.TryGetValue(valueRule.MemberName, out var context))
                {
                    var ctx = VerifiableOpsContext.Create(context);

                    valueRule.Verify(ctx);

                    if (ctx.IncludeFailures)
                    {
                        failures.AddRange(ctx.VerifyValDictionary.ConvertToFailures());
                        nameOfExecutedRules.AddRange(ctx.NameOfExecutedRuleList);
                    }
                }
            }

            return MakeMainResult(failures, nameOfExecutedRules);
        }

        public static VerifyResult ValidViaCustomValidators(VerifiableObjectContext context, IEnumerable<CustomValidator> validators)
        {
            return VerifyResult.MakeTogether(validators.Select(validator => validator.VerifyViaContext(context)).ToList());
        }

        public static VerifyResult ValidViaCustomValidators(VerifiableMemberContext context, IEnumerable<CustomValidator> validators)
        {
            return VerifyResult.MakeTogether(validators.Select(validator => validator.VerifyOneViaContext(context)).ToList());
        }

        public static VerifyResult ValidViaCustomValidators(IDictionary<string, VerifiableMemberContext> keyValueCollections, IEnumerable<CustomValidator> validators)
        {
            var slaveResults = new List<VerifyResult>();
            foreach (var context in keyValueCollections.Values)
            {
                slaveResults.AddRange(validators.Select(validator => validator.VerifyOneViaContext(context)));
            }

            return VerifyResult.MakeTogether(slaveResults);
        }

        private static VerifyResult MakeMainResult(List<VerifyFailure> failures, List<string> nameOfExecutedRules)
        {
            if (failures.Any())
            {
                return new VerifyResult(failures)
                {
                    NameOfExecutedRules = nameOfExecutedRules.Distinct().ToArray().DeepCopy(),
                };
            }

            return VerifyResult.Success;
        }
    }
}