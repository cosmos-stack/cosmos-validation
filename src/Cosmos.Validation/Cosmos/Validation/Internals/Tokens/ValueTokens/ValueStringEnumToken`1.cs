﻿using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueStringEnumToken<TEnum> : ValueStringEnumToken
    {
        public ValueStringEnumToken(VerifiableMemberContract contract, bool caseSensitive) : base(contract, typeof(TEnum), caseSensitive) { }
    }
}