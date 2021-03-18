using System;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Greater than or equal token
    /// </summary>
    internal class ValueGreaterThanOrEqualToken : ValueCompareBasicToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueGreaterThanOrEqualToken";

        /// <inheritdoc />
        public ValueGreaterThanOrEqualToken(VerifiableMemberContract contract, object valueToCompare) : base(contract, valueToCompare, NAME) { }

        /// <summary>
        /// Impl of valid ops.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueToCompare"></param>
        /// <param name="typeOfValueToCompare"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(object value, object valueToCompare, Type typeOfValueToCompare, out string message)
        {
            message = null;

            if (VerifiableMember.MemberType.IsValueType && typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.CompareTo(VerifiableMember.MemberType, value, typeOfValueToCompare, valueToCompare) >= 0)
                {
                    return true;
                }
            }

            else if (value is IComparable c1 && valueToCompare is IComparable c2)
            {
                if (c1.CompareTo(c2) >= 0)
                {
                    return true;
                }
            }

            message = "Neither the given value nor the value being compared can be converted to IComparable.";
            return false;
        }

        /// <inheritdoc />
        protected override void UpdateVal(CorrectVerifyVal val, object obj, object valueToCompare, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value must be greater than or equal to {valueToCompare}.");
        }
    }
}