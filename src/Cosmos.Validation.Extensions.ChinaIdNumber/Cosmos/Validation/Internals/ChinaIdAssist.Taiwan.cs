using System;
using System.Collections.Generic;
using System.Globalization;
using Cosmos.Text;
using Cosmos.Validation.Internals.Standards;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal class ChinaTaiwanIdAssist : IAssist
    {
        public bool ValidLength(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            //10 位：第一位为大写字母
            if (idNumber.Length == 10 && idNumber.IsAllNumber(9) && idNumber[0].IsLetter())
                return true;
            failures.Add(new(options.ParamName, "The length of the instance must be 10."));
            return false;
        }

        public bool ValidBirthday(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            return true;
        }

        public bool ValidArea(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var d = GBT2260_2013.Singleton.GetDictionary();
            info.AreaNumber = 71;
            info.RecognizableArea = new(71, d[71]);
            return true;
        }

        private static readonly Dictionary<char, int> AreaAka = new()
        {
            { 'A', 10 }, { 'B', 11 }, { 'C', 12 }, { 'D', 13 }, { 'E', 14 }, { 'F', 15 }, { 'G', 16 },
            { 'H', 17 }, { 'I', 34 }, { 'J', 18 }, { 'K', 19 }, { 'L', 20 }, { 'M', 21 }, { 'N', 22 },
            { 'O', 35 }, { 'P', 23 }, { 'Q', 24 }, { 'R', 25 }, { 'S', 26 }, { 'T', 27 }, { 'U', 28 },
            { 'V', 29 }, { 'W', 32 }, { 'X', 30 }, { 'Y', 31 }, { 'Z', 33 },
        };

        public bool ValidCheckBit(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var f = idNumber[0].ToUpper(CultureInfo.InvariantCulture);
            var s = AreaAka[f] / 10
                  + AreaAka[f] % 10 * 9
                  + (idNumber[1] - 48) * 8
                  + (idNumber[2] - 48) * 7
                  + (idNumber[3] - 48) * 6
                  + (idNumber[4] - 48) * 5
                  + (idNumber[5] - 48) * 4
                  + (idNumber[6] - 48) * 3
                  + (idNumber[7] - 48) * 2
                  + (idNumber[8] - 48) * 1;
            var checkBit = 10 - s % 100 % 10;
            var rightBit = idNumber[9] - 48;
            var @try = rightBit == checkBit;

            if (!@try)
            {
                failures.Add(new(options.ParamName, "Wrong check code."));
                return false;
            }

            info.CheckBit = (char)(checkBit + 48);

            return true;
        }

        public bool ValidTheRest(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var @try = ValidGender(idNumber, options, failures, info);
            if (!@try)
                return false;
            info.Sequence = idNumber.Slice(2, 7).GetString();
            return true;
        }

        private bool ValidGender(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            var strGender = idNumber[1];

            if (strGender == '1')
            {
                info.Gender = ChinaIdGender.Male;
                return true;
            }

            if (strGender == '2')
            {
                info.Gender = ChinaIdGender.Female;
                return true;
            }

            failures.Add(new(options.ParamName, "The gender value is wrong."));
            return false;
        }
    }
}