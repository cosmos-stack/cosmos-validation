﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cosmos.Text;
using Cosmos.Validation.Internals.Standards;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal class ChinaId18Assist : ChinaIdAssist
    {
        public override bool ValidLength(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            if (idNumber.Length == 18)
                return true;
            failures.Add(new(options.ParamName, "The length of the instance must be 18."));
            return false;
        }

        protected override bool ValidBirthdayImpl(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out DateTime date)
        {
            var s = idNumber.Substring(6, 8);
            return DateTime.TryParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                && s.All(c => c.IsNumber());
        }
        
        private static readonly int[] WeightFactors = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        private static readonly char[] CheckBits = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        protected override bool ValidCheckBitImpl(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out char rightBit)
        {
            var mod = ISO7064_1983.MOD11_2(idNumber.Select(c => (int)c - 48).Take(17).ToArray(), WeightFactors, 11);
            rightBit = CheckBits[mod];
            return rightBit == idNumber[17];
        }

        protected override string GetSequenceImpl(string idNumber)
        {
            return idNumber.Substring(14, 3);
        }
    }
}