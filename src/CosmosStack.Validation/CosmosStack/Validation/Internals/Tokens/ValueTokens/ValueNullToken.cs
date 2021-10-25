using CosmosStack.Validation.Internals.Tokens.ValueTokens.Basic;
using CosmosStack.Validation.Objects;

// ReSharper disable InconsistentNaming

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Null token
    /// </summary>
    internal class ValueNullToken : ValueRequiredBasicToken
    {
        private const string Name = "NullValueToken";

        private static readonly int[] _mutuallyExclusiveFlags = {90114};

        /// <inheritdoc />
        public ValueNullToken(VerifiableMemberContract contract) : this(contract, false, Name, _mutuallyExclusiveFlags) { }

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
        private const string Name = "NotNullValueToken";

        private static readonly int[] _mutuallyExclusiveFlags = {90111, 90112, 90113, 90114};

        /// <inheritdoc />
        public ValueNotNullToken(VerifiableMemberContract contract) : base(contract, true, Name, _mutuallyExclusiveFlags) { }

        protected override string GetDefaultMessageSinceToken(object obj) => "The value is must be not null.";
    }
}