using System;
using Cosmos.Conversions.Determiners;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredGuidToken : ValueRequiredTypeToken
    {
        private const string Name = "ValueRequiredGuidTypeToken";

        /// <inheritdoc />
        public ValueRequiredGuidToken(VerifiableMemberContract contract)
            : base(contract, TypeClass.GuidClazz, false, Name, null, true) { }

        protected override bool IsValidImpl(object value)
        {
            if (VerifiableMember.MemberType == TypeClass.GuidClazz || VerifiableMember.MemberType == TypeClass.GuidNullableClazz)
                return true;

            if (value is Guid)
                return true;

            if (value is string strValue)
                return StringGuidDeterminer.Is(strValue);

            return StringGuidDeterminer.Is(value.ToString());
        }

        protected override string GetDefaultMessageSinceToken(object obj)
        {
            return "The given type is not a Guid value.";
        }
    }
}