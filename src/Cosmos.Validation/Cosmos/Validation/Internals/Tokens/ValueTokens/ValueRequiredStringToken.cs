﻿using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredStringToken : ValueRequiredTypeToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredStringTypeToken";

        /// <inheritdoc />
        public ValueRequiredStringToken(VerifiableMemberContract contract)
            : base(contract, TypeClass.StringClazz, false, NAME, null, true) { }

        protected override bool IsValidImpl(object value)
        {
            if (VerifiableMember.MemberType == TypeClass.StringClazz)
                return true;

            if (value is string)
                return true;

            return false;
        }

        protected override string GetDefaultMessageSinceToken(object obj)
        {
            return "The given type is not a string.";
        }
    }
}