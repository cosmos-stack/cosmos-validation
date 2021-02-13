using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueNullToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "NullValueToken";

        public static int[] _mutuallyExclusiveFlags = {90114};

        public ValueNullToken(ObjectValueContract contract) : base(contract) { }

        public override CorrectValueOps Ops => CorrectValueOps.NotNull;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (value is not null)
            {
                UpdateVal(val, value);
            }

            return val;
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