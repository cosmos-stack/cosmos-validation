using System;
using System.Collections.Generic;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal static class Assists
    {
        private static readonly ChinaId15Assist ChinaId15 = new();

        private static readonly ChinaId18Assist ChinaId18 = new();

        private static readonly ChinaHongKongId03Assist ChinaHkId03 = new();

        private static readonly ChinaMacauIdAssist ChinaMacauId = new();

        private static readonly ChinaTaiwanIdAssist ChinaTwId = new();

        public static bool ValidLength(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            if (idNumber.Length == 0)
            {
                failures.Add(new(options.ParamName, "Instance cannot be null."));
                return false;
            }

            return X(options).ValidLength(idNumber, options, failures, info);
        }

        public static bool ValidBirthday(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            return X(options).ValidBirthday(idNumber, options, failures, info);
        }

        public static bool ValidArea(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            return X(options).ValidArea(idNumber, options, failures, info);
        }

        public static bool ValidCheckBit(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            return X(options).ValidCheckBit(idNumber, options, failures, info);
        }

        public static bool ValidTheRest(ReadOnlySpan<char> idNumber, ChinaIdNumberValidationOptions options, List<VerifyFailure> failures, ChinaIdNumberInfo info)
        {
            return X(options).ValidTheRest(idNumber, options, failures, info);
        }

        private static IAssist X(ChinaIdNumberValidationOptions options)
        {
            return options.Styles switch
            {
                ChinaIdStyles.Id15 => ChinaId15,
                ChinaIdStyles.Id18 => ChinaId18,
                ChinaIdStyles.HkId03 => ChinaHkId03,
                ChinaIdStyles.Macau => ChinaMacauId,
                ChinaIdStyles.Taiwan => ChinaTwId,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}