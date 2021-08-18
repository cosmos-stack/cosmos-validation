using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cosmos.Text;
using Cosmos.Validation.Internals.Standards;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal static class CharExtensions
    {
        public static bool IsNumberOrA(this char c)
        {
            if (c.IsNumber()) return true;
            if (c == 'A') return true;
            return false;
        }
    }

    internal class ChinaHongKongId03Assist : IAssist
    {
        public bool ValidLength(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            // 8 位：不包含括号
            //10 位：第 8 10 位必须是括号
            if (idNumber.Length == 8 && idNumber.Substring(1).All(c => c.IsNumberOrA()))
                return true;
            if (idNumber.Length == 10 && idNumber.Substring(1).Count(c => c.IsNumberOrA()) == 7 && idNumber[7] == '(' && idNumber[9] == ')')
                return true;
            failures.Add(new(options.ParamName, "The length of the instance must be 8."));
            return false;
        }

        public bool ValidBirthday(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            return true;
        }

        public bool ValidArea(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var d = GBT2260_2013.Singleton.GetDictionary();
            info.AreaNumber = 81;
            info.RecognizableArea = new(81, d[81]);
            return true;
        }

        public bool ValidCheckBit(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var f = idNumber[0].ToUpper(CultureInfo.InvariantCulture) - 64;
            var s = f * 8
                  + (idNumber[1] - 48) * 7
                  + (idNumber[2] - 48) * 6
                  + (idNumber[3] - 48) * 5
                  + (idNumber[4] - 48) * 4
                  + (idNumber[5] - 48) * 3
                  + (idNumber[6] - 48) * 2;
            var x = s % 11;
            char checkBit;
            if (x == 0)
                checkBit = '0';
            else if (x == 1)
                checkBit = 'A';
            else
                checkBit = (char)(11 - x + 48);

            var rightBit = idNumber.Length switch
            {
                8 => idNumber[7],
                10 => idNumber[8],
                _ => char.MinValue
            };

            var @try = rightBit == checkBit;

            if (!@try)
            {
                failures.Add(new(options.ParamName, "Wrong check code."));
                return false;
            }

            info.CheckBit = checkBit;

            return true;
        }

        public bool ValidTheRest(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            info.Gender = ChinaIdGender.Undefined;
            info.Sequence = idNumber.Substring(1);
            return true;
        }
    }
}