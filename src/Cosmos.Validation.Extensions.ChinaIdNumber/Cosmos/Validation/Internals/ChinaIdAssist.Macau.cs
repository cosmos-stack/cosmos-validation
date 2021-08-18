using System;
using System.Collections.Generic;
using Cosmos.Text;
using Cosmos.Validation.Internals.Standards;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal class ChinaMacauIdAssist : IAssist
    {
        public bool ValidLength(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            // 8 位：不包含括号
            //10 位：第 8 10 位必须是括号
            if (idNumber.Length == 8 && idNumber.IsAllNumber())
                return true;
            if (idNumber.Length == 10 && idNumber.IsAllNumber(8) && idNumber.IndexOfShouldBe(7, '(') && idNumber.IndexOfShouldBe(9, ')'))
                return true;
            failures.Add(new(options.ParamName, "The length of the instance must be 8."));
            return false;
        }

        public bool ValidBirthday(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            return true;
        }

        public bool ValidArea(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var d = GBT2260_2013.Singleton.GetDictionary();
            info.AreaNumber = 82;
            info.RecognizableArea = new(82, d[82]);
            return true;
        }

        public bool ValidCheckBit(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            info.CheckBit = idNumber.Length switch
            {
                8 => idNumber[7],
                10 => idNumber[8],
                _ => char.MinValue
            };

            return true;
        }

        public bool ValidTheRest(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var f = idNumber[0];
            if (f.BeNotContainedIn('1', '5', '7'))
            {
                failures.Add(new(options.ParamName, "The first number is illegal."));
                return false;
            }

            info.Gender = ChinaIdGender.Undefined;
            info.Sequence = idNumber.Slice(1, 6).GetString();
            return true;
        }
    }
}