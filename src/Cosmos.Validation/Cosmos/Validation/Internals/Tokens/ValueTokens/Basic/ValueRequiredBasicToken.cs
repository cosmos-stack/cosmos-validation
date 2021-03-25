using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens.Basic
{
    internal abstract class ValueRequiredBasicToken : ValueToken
    {
        protected ValueRequiredBasicToken(
            VerifiableMemberContract contract,
            bool not,
            string tokenName,
            int[] mutuallyExclusiveFlags = null,
            bool? mutuallyExclusive = null) : base(contract)
        {
            Not = not;
            TokenName = tokenName;
            MutuallyExclusive = mutuallyExclusive ?? mutuallyExclusiveFlags is not null;
            MutuallyExclusiveFlags = mutuallyExclusiveFlags ?? NoMutuallyExclusiveFlags;
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName { get; }

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public override bool MutuallyExclusive { get; }

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public override int[] MutuallyExclusiveFlags { get; }

        /// <summary>
        /// Not
        /// </summary>
        protected bool Not { get; }

        protected bool ConclusionReversal(bool value)
        {
            return Not ? !value : value;
        }

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

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
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

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        protected abstract bool IsValidImpl(object value);

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(GetDefaultMessageSinceToken(obj));
        }

        protected abstract string GetDefaultMessageSinceToken(object obj);
    }
}