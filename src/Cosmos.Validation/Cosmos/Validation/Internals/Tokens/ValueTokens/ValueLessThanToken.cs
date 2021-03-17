using System;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueLessThanToken : ValueCompareBasicToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueLessThanToken";

        public ValueLessThanToken(VerifiableMemberContract contract, object valueToCompare) : base(contract, valueToCompare, NAME) { }

        protected override bool IsValidImpl(object value, object valueToCompare, Type typeOfValueToCompare, out string message)
        {
            message = null;

            if (VerifiableMember.MemberType.IsValueType && typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.CompareTo(VerifiableMember.MemberType, value, typeOfValueToCompare, valueToCompare) < 0)
                {
                    return true;
                }
            }

            else if (value is IComparable c1 && valueToCompare is IComparable c2)
            {
                if (c1.CompareTo(c2) < 0)
                    return true;
            }

            message = "Neither the given value nor the value being compared can be converted to IComparable.";
            return false;
        }

        protected override void UpdateVal(CorrectVerifyVal val, object obj, object valueToCompare, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value must be less than {valueToCompare}.");
        }
    }
}