using System.Globalization;
using CosmosStack.Conversions.StringDeterminers;
using CosmosStack.Date;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredDateInfoToken : ValueRequiredTypeToken
    {
        private const string Name = "ValueRequiredDateInfoTypeToken";

        private readonly DateTimeStyles _style;

        /// <inheritdoc />
        public ValueRequiredDateInfoToken(VerifiableMemberContract contract, DateTimeStyles style = DateTimeStyles.None)
            : base(contract, typeof(DateInfo), false, Name, null, true)
        {
            _style = style;
        }

        protected override bool IsValidImpl(object value)
        {
            if (VerifiableMember.MemberType == typeof(DateInfo))
                return true;

            if (value is DateInfo)
                return true;

            if (value is string strValue)
                return StringDateInfoDeterminer.Is(strValue, _style);

            return StringDateInfoDeterminer.Is(value.ToString(), _style);
        }

        protected override string GetDefaultMessageSinceToken(object obj)
        {
            return "The given type is not a DateInfo value.";
        }
    }
}