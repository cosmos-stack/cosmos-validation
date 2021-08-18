using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cosmos.Text;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal class ChinaId15Assist : ChinaIdAssist
    {
        public override bool ValidLength(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            if (idNumber.Length == 15)
                return true;
            failures.Add(new(options.ParamName, "The length of the instance must be 15."));
            return false;
        }

        protected override bool ValidBirthdayImpl(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out DateTime date)
        {
            var t = idNumber.Slice(6, 6);
            var s = t[0] == '0' ? $"20{t.GetString()}" : $"19{t.GetString()}";
            return DateTime.TryParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                && s.All(c => c.IsNumber());
        }

        protected override bool ValidCheckBitImpl(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out char rightBit)
        {
            rightBit = char.MinValue;
            return true;
        }

        protected override string GetSequenceImpl(ReadOnlySpan<char> idNumber)
        {
            return idNumber.Slice(12,3).GetString();
        }
    }
}