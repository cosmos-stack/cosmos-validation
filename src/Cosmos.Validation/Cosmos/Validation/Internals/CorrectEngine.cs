using System.Collections.Generic;
using System.Linq;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

// ReSharper disable PossibleMultipleEnumeration

namespace Cosmos.Validation.Internals
{
    internal static class CorrectEngine
    {
        public static VerifyResult Valid(ObjectContext context, List<CorrectValueRule> rules, IEnumerable<CustomValidator> validators)
        {
            var len = rules.Count;
            var failures = new List<VerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];

                ValidCore(context, valueRule, failures, nameOfExecutedRules);
            }

            var mainResult = MakeMainResult(failures, nameOfExecutedRules);

            var slaveResults = MakeSlaveResults(context, validators);

            return VerifyResult.Merge(mainResult, slaveResults.ToArray());
        }

        public static VerifyResult ValidOne(ObjectValueContext context, List<CorrectValueRule> rules, IEnumerable<CustomValidator> validators)
        {
            var len = rules.Count;
            var failures = new List<VerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];

                ValidCore(context, valueRule, failures, nameOfExecutedRules);
            }

            var mainResult = MakeMainResult(failures, nameOfExecutedRules);

            var slaveResults = MakeSlaveResults(context, validators);

            return VerifyResult.Merge(mainResult, slaveResults.ToArray());
        }

        public static VerifyResult ValidMany(IDictionary<string, ObjectValueContext> keyValueCollections, List<CorrectValueRule> rules, IEnumerable<CustomValidator> validators)
        {
            var len = rules.Count;
            var failures = new List<VerifyFailure>();
            var nameOfExecutedRules = new List<string>();
            var slaveResults = new List<VerifyResult>();

            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];
                if (keyValueCollections.TryGetValue(valueRule.MemberName, out var context))
                    ValidCore(context, valueRule, failures, nameOfExecutedRules);
            }

            var mainResult = MakeMainResult(failures, nameOfExecutedRules);

            foreach (var context in keyValueCollections.Values)
            {
                slaveResults.AddRange(MakeSlaveResults(context, validators));
            }

            return VerifyResult.Merge(mainResult, slaveResults.ToArray());
        }

        private static void ValidCore(ObjectContext context, CorrectValueRule valueRule, List<VerifyFailure> failures, List<string> nameOfExecutedRules)
        {
            var verifyValSet = valueRule
                               .ValidValue(context)
                               .Where(x => x.IsSuccess == false)
                               .ToList();

            if (verifyValSet.Any())
            {
                var count = verifyValSet.Count;

                var failure = new VerifyFailure(
                    valueRule.MemberName,
                    $"Member {valueRule.MemberName} encountered {(count == 1 ? "an error" : "some errors")} during verification.",
                    valueRule.GetValue(context).Value)
                {
                    Details = verifyValSet.Select(verifyVal => new VerifyError {ErrorMessage = verifyVal.ErrorMessage}).ToList()
                };

                failures.Add(failure);
                nameOfExecutedRules.AddRange(verifyValSet.Select(verifyVal => verifyVal.NameOfExecutedRule));
            }
        }

        private static void ValidCore(ObjectValueContext context, CorrectValueRule valueRule, List<VerifyFailure> failures, List<string> nameOfExecutedRules)
        {
            var verifyValSet = valueRule
                               .ValidValue(context)
                               .Where(x => x.IsSuccess == false)
                               .ToList();

            if (verifyValSet.Any())
            {
                var count = verifyValSet.Count;

                var failure = new VerifyFailure(
                    valueRule.MemberName,
                    $"Member {valueRule.MemberName} encountered {(count == 1 ? "an error" : "some errors")} during verification.",
                    context.Value)
                {
                    Details = verifyValSet.Select(verifyVal => new VerifyError {ErrorMessage = verifyVal.ErrorMessage}).ToList()
                };

                failures.Add(failure);
                nameOfExecutedRules.AddRange(verifyValSet.Select(verifyVal => verifyVal.NameOfExecutedRule));
            }
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

        private static IEnumerable<VerifyResult> MakeSlaveResults(ObjectContext context, IEnumerable<CustomValidator> validators)
        {
            foreach (var ctx in context.ToValueContexts())
            foreach (var result in MakeSlaveResults(ctx, validators))
                yield return result;
        }

        private static IEnumerable<VerifyResult> MakeSlaveResults(ObjectValueContext context, IEnumerable<CustomValidator> validators)
        {
            foreach (var validator in validators)
                yield return validator.VerifyOneViaContext(context);
        }
    }
}