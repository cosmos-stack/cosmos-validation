using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueNotNullToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "NotNullValueToken";

        public static int[] _mutuallyExclusiveFlags = {90111, 90112, 90113, 90114};

        public ValueNotNullToken(VerifiableMemberContract contract) : base(contract) { }

        public override CorrectValueOps Ops => CorrectValueOps.NotNull;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (value is null)
            {
                UpdateVal(verifyVal, null);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (value is null)
            {
                UpdateVal(verifyVal, null);
            }

            return verifyVal;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is must be not null.");
        }

        public override string ToString() => NAME;
    }
}