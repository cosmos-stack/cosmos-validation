using System;
using System.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ArrayLengthToken";
        public static int[] _mutuallyExclusiveFlags = {90111, 90115, 90119, 90120};

        private readonly int _minLength;
        private readonly int _maxLength;

        public ValueLengthLimitedToken(VerifiableMemberContract contract, int min, int max) : base(contract)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min));
            if (max != -1 && max < min)
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min.");

            _minLength = min;
            _maxLength = max;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Length;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var len))
            {
                UpdateVal(verifyVal, value, len);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var len))
            {
                UpdateVal(verifyVal, value, len);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value, out int currentLength)
        {
            if (value is string stringVal)
            {
                currentLength = stringVal.Length;
                if (currentLength < _minLength ||
                    currentLength > _maxLength && _maxLength != -1)
                    return false;
            }

            else if (VerifiableMember.MemberType == typeof(string) && _minLength > 0)
            {
                currentLength = 0;
                return false;
            }

            else if (value is ICollection collection)
            {
                currentLength = collection.Count;
                if (currentLength < _minLength ||
                    currentLength > _maxLength && _maxLength != -1)
                    return false;
            }

            currentLength = 0;
            return true;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, int currentLength)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The array length should be greater than {_minLength} and less than {_maxLength}, and the current length is {currentLength}.");
        }

        public override string ToString()
        {
            return $"{NAME}: The minimum length is {_minLength}, and the maximum length is {_maxLength}.";
        }
    }
}