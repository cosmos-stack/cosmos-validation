using System;
using System.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Min length limited token
    /// </summary>
    internal class ValueMinLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ArrayMinLengthToken";
        public static int[] _mutuallyExclusiveFlags = {90113, 90117, 90119};

        private readonly int _minLength;

        /// <inheritdoc />
        public ValueMinLengthLimitedToken(VerifiableMemberContract contract, int min) : base(contract)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min));
            _minLength = min;
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
            
            if (!IsActivate(context.Instance, value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value, out var currentLength))
            {
                UpdateVal(verifyVal, value, currentLength);
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
    }
}