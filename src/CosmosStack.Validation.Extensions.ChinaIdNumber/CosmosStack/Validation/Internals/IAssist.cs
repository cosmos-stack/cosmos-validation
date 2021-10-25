using System;
using System.Collections.Generic;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Internals
{
    internal interface IAssist
    {
        bool ValidLength(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidBirthday(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidArea(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidCheckBit(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);

        bool ValidTheRest(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info);
    }
}