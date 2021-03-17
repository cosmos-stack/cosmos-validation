using System;
using System.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueEqualToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueEqualToken";

        private readonly object _valueToCompare;
        private readonly Type _typeOfValueToCompare;
        private readonly IEqualityComparer _comparer;

        public ValueEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = _valueToCompare.GetType();
            _comparer = comparer;
        }

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value)
        {
            if (_comparer != null)
            {
                return _comparer.Equals(_valueToCompare, value);
            }

            if (VerifiableMember.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.Valid(VerifiableMember.MemberType, value, _typeOfValueToCompare, _valueToCompare))
                    return true;
            }

            return Equals(_valueToCompare, value);
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The two values given must be equal. The current value is: {obj} and the value being compared is {_valueToCompare}.");
        }

        public override string ToString() => NAME;
    }
}