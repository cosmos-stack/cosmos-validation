using CosmosStack.Reflection;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredStringToken : ValueRequiredTypeToken
    {
        private const string Name = "ValueRequiredStringTypeToken";

        /// <inheritdoc />
        public ValueRequiredStringToken(VerifiableMemberContract contract)
            : base(contract, TypeClass.StringClazz, false, Name, null, true) { }

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