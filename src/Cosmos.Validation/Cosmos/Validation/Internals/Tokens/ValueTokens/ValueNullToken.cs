using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueNullToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "NullValueToken";

        public static int[] _mutuallyExclusiveFlags = {90114};

        public ValueNullToken(VerifiableMemberContract contract) : base(contract) { }

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;
        
        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (value is not null)
            {
                UpdateVal(verifyVal, null);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (value is not null)
            {
                UpdateVal(verifyVal, null);
            }

            return verifyVal;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is must be null.");
        }

        public override string ToString() => NAME;
    }
}