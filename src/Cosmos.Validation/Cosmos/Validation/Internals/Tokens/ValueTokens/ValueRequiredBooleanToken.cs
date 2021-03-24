using Cosmos.Conversions.Determiners;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredBooleanToken : ValueRequiredTypeToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredBooleanTypeToken";

        private readonly TypeIsOptions _isOptions;

        /// <inheritdoc />
        public ValueRequiredBooleanToken(VerifiableMemberContract contract, TypeIsOptions isOptions = TypeIsOptions.Default)
            : base(contract, TypeClass.BooleanClazz, false, NAME, null, true)
        {
            _isOptions = isOptions;
        }

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