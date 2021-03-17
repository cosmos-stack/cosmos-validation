using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Validation.Internals
{
    internal static class CorrectVerifyValExtensions
    {
        public static List<VerifyFailure> ConvertToFailures(this Dictionary<string, List<CorrectVerifyVal>> verifyValDictionary)
        {
            var failures = new List<VerifyFailure>();

            if (verifyValDictionary is null) return failures;

            foreach (var pair in verifyValDictionary)
            {
                var memberName = pair.Key;
                var verifyValSet = pair.Value.Where(x => x.IsSuccess == false).ToList();

                if (!verifyValSet.Any())
                    continue;

                var count = verifyValSet.Count;
                var memberValue = verifyValSet[0].VerifiedValue;

                var failure = new VerifyFailure(memberName,
                    $"Member {memberName} encountered {(count == 1 ? "an error" : "some errors")} during verification.",
                    memberValue)
                {
                    Details = verifyValSet.Select(verifyVal => new VerifyError {ErrorMessage = verifyVal.ErrorMessage}).ToList()
                };

                failures.Add(failure);
            }

            return failures;
        }
    }
}