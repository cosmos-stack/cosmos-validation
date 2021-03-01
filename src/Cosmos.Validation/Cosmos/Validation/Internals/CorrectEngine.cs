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
        public static VerifyResult Valid(ObjectContext context, List<CorrectValueRule> rules)
        {
            var len = rules.Count;
            var failures = new List<VerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];

                ValidCore(context, valueRule, failures, nameOfExecutedRules);
            }

            return MakeMainResult(failures, nameOfExecutedRules);
        }

        public static VerifyResult ValidOne(ObjectValueContext context, List<CorrectValueRule> rules)
        {
            var len = rules.Count;
            var failures = new List<VerifyFailure>();
            var nameOfExecutedRules = new List<string>();

            for (var i = 0; i < len; i++)
            {
                var valueRule = rules[i];

                ValidCore(context, valueRule, failures, nameOfExecutedRules);
            }

            return MakeMainResult(failures, nameOfExecutedRules);
        }

        public static VerifyResult ValidMany(IDictionary<string, ObjectValueContext> keyValueCollections, List<CorrectValueRule> rules)
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

            return MakeMainResult(failures, nameOfExecutedRules);
        }

        public static VerifyResult ValidViaCustomValidators(ObjectContext context, IEnumerable<CustomValidator> validators)
        {
            return VerifyResult.MakeTogether(MakeSlaveResults(context, validators).ToList());
        }

        public static VerifyResult ValidViaCustomValidators(ObjectValueContext context, IEnumerable<CustomValidator> validators)
        {
            return VerifyResult.MakeTogether(MakeSlaveResults(context, validators).ToList());
        }

        public static VerifyResult ValidViaCustomValidators(IDictionary<string, ObjectValueContext> keyValueCollections, IEnumerable<CustomValidator> validators)
        {
            var slaveResults = new List<VerifyResult>();
            foreach (var context in keyValueCollections.Values)
            {
                slaveResults.AddRange(MakeSlaveResults(context, validators));
            }

            return VerifyResult.MakeTogether(slaveResults);
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
            foreach (var validator in validators.ToList())
                yield return validator.VerifyViaContext(context);
        }

        private static IEnumerable<VerifyResult> MakeSlaveResults(ObjectValueContext context, IEnumerable<CustomValidator> validators)
        {
            foreach (var validator in validators.ToList())
                yield return validator.VerifyOneViaContext(context);
        }
    }
}