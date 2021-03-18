using System;
using System.Collections;
using System.Linq;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Empty token
    /// </summary>
    internal class ValueEmptyToken : ValueRequiredBasicToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "EmptyValueToken";

        // ReSharper disable once InconsistentNaming
        public static readonly int[] _mutuallyExclusiveFlags = {90115, 90116, 90117, 90118};

        /// <inheritdoc />
        public ValueEmptyToken(VerifiableMemberContract contract) : this(contract, false, NAME, _mutuallyExclusiveFlags) { }

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
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueNotEmptyToken";

        // ReSharper disable once InconsistentNaming
        public static int[] _mutuallyExclusiveFlags = {90118};

        /// <inheritdoc />
        public ValueNotEmptyToken(VerifiableMemberContract contract) : base(contract, true, NAME, _mutuallyExclusiveFlags) { }

        protected override string GetDefaultMessageSinceToken(object obj) => "The value is must be not empty.";
    }
}