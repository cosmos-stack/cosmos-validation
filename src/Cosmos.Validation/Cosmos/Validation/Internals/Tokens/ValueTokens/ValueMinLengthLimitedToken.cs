using System;
using System.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueMinLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ArrayMinLengthToken";
        public static int[] _mutuallyExclusiveFlags = {90113, 90117, 90119};

        private readonly int _minLength;

        public ValueMinLengthLimitedToken(VerifiableMemberContract contract, int min) : base(contract)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min));
            _minLength = min;
        }

        public override CorrectValueOps Ops => CorrectValueOps.MinLen;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var currentLength))
            {
                UpdateVal(verifyVal, value, currentLength);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
           
            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var currentLength))
            {
                UpdateVal(verifyVal, value, currentLength);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value, out int currentLength)
        {
            if (value is string stringVal)
            {
                currentLength = stringVal.Length;
                if (currentLength < _minLength)
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
                if (currentLength < _minLength)
                    return false;
            }

            currentLength = 0;
            return true;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, int currentLength)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The array length should be greater than {_minLength}, and the current length is {currentLength}.");
        }

        public override string ToString()
        {
            return $"{NAME}: The minimum length is {_minLength}.";
        }
    }
}