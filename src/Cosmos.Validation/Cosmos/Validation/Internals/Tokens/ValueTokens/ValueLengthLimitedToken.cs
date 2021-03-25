using System;
using System.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Length limited token
    /// </summary>
    internal class ValueLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ArrayLengthToken";
        public static int[] _mutuallyExclusiveFlags = {90111, 90115, 90119, 90120};

        private readonly int _minLength;
        private readonly int _maxLength;

        /// <inheritdoc />
        public ValueLengthLimitedToken(VerifiableMemberContract contract, int min, int max) : base(contract)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min));
            if (max != -1 && max < min)
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min.");

            _minLength = min;
            _maxLength = max;
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => NAME;

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public override bool MutuallyExclusive => true;

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        /// <summary>
        /// Verification for VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var value = GetValueFrom(context);
            
            if(!IsActivate(value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value, out var len))
            {
                UpdateVal(verifyVal, value, len);
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
            
            if(!IsActivate(value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

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
    }
}