﻿using System;
using CosmosStack.Validation.Objects;

/*
 * Reference to:
 *   https://github.com/FluentValidation/FluentValidation/blob/master/src/FluentValidation/Validators/ScalePrecisionValidator.cs
 */

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Scale precision token
    /// </summary>
    internal class ValueScalePrecisionToken : ValueToken
    {
        private const string Name = "ValueScalePrecisionToken";

        /// <inheritdoc />
        public ValueScalePrecisionToken(VerifiableMemberContract contract, int scale, int precision, bool ignoreTrailingZeros = false) : base(contract)
        {
            Init(scale, precision);
            IgnoreTrailingZeros = ignoreTrailingZeros;
        }

        private void Init(int scale, int precision)
        {
            Scale = scale;
            Precision = precision;

            if (Scale < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(scale), $"Scale must be a positive integer. Current scale value is {Scale}.");
            if (Precision < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(precision), $"Precision must be a positive integer. Current precision value is {Precision}.");
            if (Precision < Scale)
                throw new ArgumentOutOfRangeException(
                    nameof(scale),
                    $"Scale must be less than precision. Current scale is {Scale} and precision is {Precision}.");
        }

        /// <summary>
        /// Scale
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// Precision
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// Ignore trailing zeros
        /// </summary>
        public bool IgnoreTrailingZeros { get; set; }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => Name;

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public override bool MutuallyExclusive => true;

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        /// <summary>
        /// Verification for VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var value = GetValueFrom(context);

            if (!IsActivate(context, value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value, out var scale, out var actualIntegerDigits))
            {
                UpdateVal(verifyVal, value, scale, actualIntegerDigits);
            }

            return verifyVal;
        }

        /// <summary>
        /// Verification for VerifiableMemberContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var value = GetValueFrom(context);

            if (!IsActivate(context, value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value, out var scale, out var actualIntegerDigits))
            {
                UpdateVal(verifyVal, value, scale, actualIntegerDigits);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value, out int scale, out int actualIntegerDigits)
        {
            if (value is decimal decimalValue)
            {
                scale = GetScale(decimalValue);
                var precision = GetPrecision(decimalValue);
                actualIntegerDigits = precision - scale;
                var expectedIntegerDigits = Precision - Scale;

                if (scale > Scale || actualIntegerDigits > expectedIntegerDigits)
                {
                    return false;
                }
            }

            scale = 0;
            actualIntegerDigits = 0;
            return true;
        }

        private static uint[] GetBits(decimal @decimal)
        {
            // We want the integer parts as uint
            // C# doesn't permit int[] to uint[] conversion, but .NET does. This is somewhat evil...
            return (uint[]) (object) decimal.GetBits(@decimal);
        }

        private static decimal GetMantissa(decimal @decimal)
        {
            var bits = GetBits(@decimal);
            return (bits[2] * 4294967296m * 4294967296m) + (bits[1] * 4294967296m) + bits[0];
        }

        private static uint GetUnsignedScale(decimal @decimal)
        {
            var bits = GetBits(@decimal);
            var scale = (bits[3] >> 16) & 31;
            return scale;
        }

        private int GetScale(decimal @decimal)
        {
            var scale = GetUnsignedScale(@decimal);
            if (IgnoreTrailingZeros)
            {
                return (int) (scale - NumTrailingZeros(@decimal));
            }

            return (int) scale;
        }

        private static uint NumTrailingZeros(decimal @decimal)
        {
            uint trailingZeros = 0;
            var scale = GetUnsignedScale(@decimal);
            for (var tmp = GetMantissa(@decimal); tmp % 10m == 0 && trailingZeros < scale; tmp /= 10)
            {
                trailingZeros++;
            }

            return trailingZeros;
        }

        private int GetPrecision(decimal @decimal)
        {
            // Precision: number of times we can divide by 10 before we get to 0
            uint precision = 0;
            for (var tmp = GetMantissa(@decimal); tmp >= 1; tmp /= 10)
            {
                precision++;
            }

            if (IgnoreTrailingZeros)
            {
                return (int) (precision - NumTrailingZeros(@decimal));
            }

            return (int) precision;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, int actualScale, int actualIntegerDigits)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The given value must not be more than {Precision} digits in total, with allowance for {Scale} decimals. {actualIntegerDigits} digits and {actualScale} decimals were found.");
        }
    }
}