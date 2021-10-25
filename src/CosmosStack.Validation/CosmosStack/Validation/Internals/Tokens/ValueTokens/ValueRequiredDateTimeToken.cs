using System;
using System.Globalization;
using CosmosStack.Conversions.Determiners;
using CosmosStack.Reflection;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredDateTimeToken : ValueRequiredTypeToken
    {
        private const string Name = "ValueRequiredDateTimeTypeToken";

        private readonly DateTimeStyles _style;

        /// <inheritdoc />
        public ValueRequiredDateTimeToken(VerifiableMemberContract contract, DateTimeStyles style = DateTimeStyles.None)
            : base(contract, TypeClass.DateTimeClazz, false, Name, null, true)
        {
            _style = style;
        }

        protected override bool IsValidImpl(object value)
        {
            if (VerifiableMember.MemberType == TypeClass.DateTimeClazz || VerifiableMember.MemberType == TypeClass.DateTimeNullableClazz)
                return true;

            if (value is DateTime)
                return true;

            if (value is string strValue)
                return StringDateTimeDeterminer.Is(strValue, _style);

            return StringDateTimeDeterminer.Is(value.ToString(), _style);
        }

        protected override string GetDefaultMessageSinceToken(object obj)
        {
            return "The given type is not a DateTime value.";
        }
    }
}