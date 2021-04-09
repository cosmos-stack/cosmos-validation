using Cosmos.Conversions.Determiners;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredBooleanToken : ValueRequiredTypeToken
    {
        private const string Name = "ValueRequiredBooleanTypeToken";

        /// <inheritdoc />
        public ValueRequiredBooleanToken(VerifiableMemberContract contract)
            : base(contract, TypeClass.BooleanClazz, false, Name, null, true) { }

        protected override bool IsValidImpl(object value)
        {
            if (VerifiableMember.MemberType == TypeClass.BooleanClazz || VerifiableMember.MemberType == TypeClass.BooleanNullableClazz)
                return true;

            if (value is bool)
                return true;

            if (value is string strValue)
                return StringBooleanDeterminer.Is(strValue);

            return StringBooleanDeterminer.Is(value.ToString());
        }

        protected override string GetDefaultMessageSinceToken(object obj)
        {
            return "The given type is not a boolean value.";
        }
    }
}