using Cosmos.Conversions.Determiners;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredNumericToken : ValueRequiredTypeToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredNumericTypeToken";

        private readonly TypeIsOptions _isOptions;

        /// <inheritdoc />
        public ValueRequiredNumericToken(VerifiableMemberContract contract, TypeIsOptions isOptions = TypeIsOptions.Default)
            : base(contract, TypeClass.DecimalClazz, false, NAME, null, true)
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