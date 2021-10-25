using CosmosStack.Conversions.Determiners;
using CosmosStack.Reflection;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredNumericToken : ValueRequiredTypeToken
    {
        private const string Name = "ValueRequiredNumericTypeToken";

        private readonly TypeIsOptions _isOptions;

        /// <inheritdoc />
        public ValueRequiredNumericToken(VerifiableMemberContract contract, TypeIsOptions isOptions = TypeIsOptions.Default)
            : base(contract, TypeClass.DecimalClazz, false, Name, null, true)
        {
            _isOptions = isOptions;
        }

        protected override bool IsValidImpl(object value)
        {
            if (VerifiableMember.MemberType.IsNumeric(_isOptions))
                return true;

            if (value is string strValue)
                return StringDecimalDeterminer.Is(strValue);
            
            return StringDecimalDeterminer.Is(value.ToString());
        }

        protected override string GetDefaultMessageSinceToken(object obj)
        {
            return "The given type is not a number.";
        }
    }
}