using System;
using System.Collections;
using System.Linq;
using CosmosStack.Validation.Internals.Extensions;
using CosmosStack.Validation.Internals.Tokens.ValueTokens.Basic;
using CosmosStack.Validation.Objects;

// ReSharper disable InconsistentNaming

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Empty token
    /// </summary>
    internal class ValueEmptyToken : ValueRequiredBasicToken
    {
        private const string Name = "EmptyValueToken";

        private static readonly int[] _mutuallyExclusiveFlags = {90115, 90116, 90117, 90118};

        /// <inheritdoc />
        public ValueEmptyToken(VerifiableMemberContract contract) : this(contract, false, Name, _mutuallyExclusiveFlags) { }

        protected ValueEmptyToken(VerifiableMemberContract contract, bool not, string tokenName, int[] mutuallyExclusiveFlags)
            : base(contract, not, tokenName, mutuallyExclusiveFlags) { }

        protected override bool IsValidImpl(object value)
        {
            switch (value)
            {
                case null:
                case string s when string.IsNullOrWhiteSpace(s):
                case ICollection {Count: 0}:
                case Array {Length: 0}:
                case IEnumerable e when !e.Cast<object>().Any():
                    return ConclusionReversal(true);
            }

            return ConclusionReversal(Equals(value, VerifiableMember.GetDefaultValue()));
        }

        protected override string GetDefaultMessageSinceToken(object obj) => "The value is must be empty.";
    }

    /// <summary>
    /// Not empty token
    /// </summary>
    internal class ValueNotEmptyToken : ValueEmptyToken
    {
        private const string Name = "ValueNotEmptyToken";

        private static readonly int[] _mutuallyExclusiveFlags = {90118};

        /// <inheritdoc />
        public ValueNotEmptyToken(VerifiableMemberContract contract) : base(contract, true, Name, _mutuallyExclusiveFlags) { }

        protected override string GetDefaultMessageSinceToken(object obj) => "The value is must be not empty.";
    }
}