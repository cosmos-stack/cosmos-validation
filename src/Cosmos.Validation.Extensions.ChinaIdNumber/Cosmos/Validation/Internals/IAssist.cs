using System.Collections.Generic;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal interface IAssist
    {
        bool ValidLength(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidBirthday(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidArea(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidCheckBit(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidTheRest(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);
    }
}