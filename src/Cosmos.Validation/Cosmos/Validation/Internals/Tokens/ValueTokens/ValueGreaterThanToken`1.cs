using System;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueGreaterThanToken<TVal> : ValueToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueGreaterThanToken";

        private readonly TVal _valueToCompare;
        private readonly Type _typeOfValueToCompare;

        public ValueGreaterThanToken(VerifiableMemberContract contract, TVal valueToCompare) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = typeof(TVal);
        }

        public override CorrectValueOps Ops => CorrectValueOps.GreaterThan_T1;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;
        
        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var message))
            {
                UpdateVal(verifyVal, value, message);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var message))
            {
                UpdateVal(verifyVal, value, message);
            }

            return verifyVal;
        }

        private bool IsValidImpl(TVal value, out string message)
        {
            message = null;

            if (VerifiableMember.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.CompareTo(VerifiableMember.MemberType, value, _typeOfValueToCompare, _valueToCompare) > 0)
                {
                    return true;
                }
            }

            else if (value is IComparable c1 && _valueToCompare is IComparable c2)
            {
                if (c1.CompareTo(c2) > 0)
                {
                    return true;
                }
            }

            message = $"Neither the given value nor the value being compared can be converted to IComparable<{_typeOfValueToCompare.GetFriendlyName()}>.";

            return false;
        }

        private void UpdateVal(CorrectVerifyVal val, TVal obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value must be greater than {_valueToCompare}.");
        }

        public override string ToString() => NAME;
    }
}