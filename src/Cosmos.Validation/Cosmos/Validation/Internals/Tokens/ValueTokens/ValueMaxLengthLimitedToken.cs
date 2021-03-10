using System;
using System.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueMaxLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ArrayMaxLengthToken";
        public static int[] _mutuallyExclusiveFlags = {90112, 90116, 90120};

        private readonly int _maxLength;

        public ValueMaxLengthLimitedToken(VerifiableMemberContract contract, int max) : base(contract)
        {
            if (max < 0)
                throw new ArgumentOutOfRangeException(nameof(max));
            _maxLength = max;
        }

        public override CorrectValueOps Ops => CorrectValueOps.MaxLen;

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
                if (currentLength > _maxLength)
                    return false;
            }

            else if (value is ICollection collection)
            {
                currentLength = collection.Count;
                if (currentLength > _maxLength)
                    return false;
            }

            currentLength = 0;
            return true;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, int currentLength)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The array length should be less than {_maxLength}, and the current length is {currentLength}.");
        }

        public override string ToString()
        {
            return $"{NAME}: The maximum length is {_maxLength}.";
        }
    }
}