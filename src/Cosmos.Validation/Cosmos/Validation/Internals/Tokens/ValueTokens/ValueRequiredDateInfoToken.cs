using System.Globalization;
using Cosmos.Conversions.StringDeterminers;
using Cosmos.Date;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredDateInfoToken : ValueRequiredTypeToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredDateInfoTypeToken";

        private readonly DateTimeStyles _style;

        /// <inheritdoc />
        public ValueRequiredDateInfoToken(VerifiableMemberContract contract, DateTimeStyles style = DateTimeStyles.None)
            : base(contract, typeof(DateInfo), false, NAME, null, true)
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