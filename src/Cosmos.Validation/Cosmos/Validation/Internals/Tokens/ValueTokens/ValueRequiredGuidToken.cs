using System;
using Cosmos.Conversions.Determiners;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredGuidToken : ValueRequiredTypeToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredGuidTypeToken";

        private readonly TypeIsOptions _isOptions;

        /// <inheritdoc />
        public ValueRequiredGuidToken(VerifiableMemberContract contract, TypeIsOptions isOptions = TypeIsOptions.Default)
            : base(contract, TypeClass.GuidClazz, false, NAME, null, true)
        {
            _isOptions = isOptions;
        }

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