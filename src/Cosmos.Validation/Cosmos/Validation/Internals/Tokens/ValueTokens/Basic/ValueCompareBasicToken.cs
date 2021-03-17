using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens.Basic
{
    internal abstract class ValueCompareBasicToken : ValueToken
    {
        private readonly object _valueToCompare;
        private readonly Type _typeOfValueToCompare;

        protected ValueCompareBasicToken(VerifiableMemberContract contract, object valueToCompare, string tokenName) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = _valueToCompare.GetType();
            TokenName = tokenName;
        }

        public override string TokenName { get; }

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, _valueToCompare, _typeOfValueToCompare, out var message))
            {
                UpdateVal(verifyVal, value, message);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, _valueToCompare, _typeOfValueToCompare, out var message))
            {
                UpdateVal(verifyVal, value, message);
            }

            return verifyVal;
        }

        protected abstract bool IsValidImpl(object value, object valueToCompare, Type typeOfValueToCompare, out string message);

        protected virtual void UpdateVal(CorrectVerifyVal val, object obj, object valueToCompare, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value does not meet the requirements.");
        }

        public override string ToString() => TokenName;
    }
}