using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Null token
    /// </summary>
    internal class ValueNullToken : ValueRequiredBasicToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "NullValueToken";

        public static int[] _mutuallyExclusiveFlags = {90114};

        /// <inheritdoc />
        public ValueNullToken(VerifiableMemberContract contract) : this(contract, false, NAME, _mutuallyExclusiveFlags) { }

        protected ValueNullToken(VerifiableMemberContract contract, bool not, string tokenName, int[] mutuallyExclusiveFlags)
            : base(contract, not, tokenName, mutuallyExclusiveFlags) { }

        protected override bool IsValidImpl(object value)
        {
            return ConclusionReversal(value is null);
        }

        protected override string GetDefaultMessageSinceToken(object obj) => "The value is must be null.";
    }

    /// <summary>
    /// Not-Null token
    /// </summary>
    internal class ValueNotNullToken : ValueNullToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "NotNullValueToken";

        public static int[] _mutuallyExclusiveFlags = {90111, 90112, 90113, 90114};

        /// <inheritdoc />
        public ValueNotNullToken(VerifiableMemberContract contract) : base(contract, true, NAME, _mutuallyExclusiveFlags) { }

        protected override string GetDefaultMessageSinceToken(object obj) => "The value is must be not null.";
    }
}