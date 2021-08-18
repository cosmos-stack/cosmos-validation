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
        public override bool ValidLength(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            if (idNumber.Length == 15)
                return true;
            failures.Add(new(options.ParamName, "The length of the instance must be 15."));
            return false;
        }
        
        protected override bool ValidBirthdayImpl(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out DateTime date)
        {
            var t = idNumber.Substring(6, 6);
            var s = t.StartsWith("0") ? $"20{t}" : $"19{t}";
            return DateTime.TryParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                && s.All(c => c.IsNumber());
        }

        protected override bool ValidCheckBitImpl(string idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, out char rightBit)
        {
            rightBit = char.MinValue;
            return true;
        }
        
        protected override string GetSequenceImpl(string idNumber)
        {
            return idNumber.Substring(12, 3);
        }
    }
}