using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueLessThanOrEqualToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueLessThanOrEqualToken";

        private readonly object _valueToCompare;
        private readonly Type _typeOfValueToCompare;

        public ValueLessThanOrEqualToken(VerifiableMemberContract contract, object valueToCompare) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = _valueToCompare.GetType();
        }

        public override CorrectValueOps Ops => CorrectValueOps.LessThanOrEqual;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;
        
        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var message))
            {
                UpdateVal(verifyVal, value, message);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var message))
            {
                UpdateVal(verifyVal, value, message);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value, out string message)
        {
            message = null;

            if (VerifiableMember.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.CompareTo(VerifiableMember.MemberType, value, _typeOfValueToCompare, _valueToCompare) <= 0)
                {
                    return true;
                }
            }

            else if (value is IComparable c1 && _valueToCompare is IComparable c2)
            {
                if (c1.CompareTo(c2) <= 0)
                {
                    return true;
                }
            }

            message = "Neither the given value nor the value being compared can be converted to IComparable.";
            return false;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value must be less than or equal to {_valueToCompare}.");
        }

        public override string ToString() => NAME;
    }
}