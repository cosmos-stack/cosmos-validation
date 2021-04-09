using System;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Required type token
    /// </summary>
    internal class ValueRequiredTypeToken : ValueRequiredBasicToken
    {
        private const string Name = "ValueRequiredTypeToken";

        private readonly Type _type;

        /// <inheritdoc />
        public ValueRequiredTypeToken(VerifiableMemberContract contract, Type type)
            : base(contract, false, Name, null, true)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected ValueRequiredTypeToken(VerifiableMemberContract contract, Type type, bool not, string tokenName, int[] mutuallyExclusiveFlags = null, bool? mutuallyExclusive = null)
            : base(contract, not, tokenName, mutuallyExclusiveFlags, mutuallyExclusive)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }

        protected override bool IsValidImpl(object value)
        {
            var realType = _getType();

            if (VerifiableMember.MemberType == _type || VerifiableMember.MemberType.IsDerivedFrom(_type))
                return true;

            if (realType is not null && (realType == _type || realType.IsDerivedFrom(_type)))
                return true;

            return false;

            // ReSharper disable once InconsistentNaming
            Type _getType()
            {
                if (value is Type t)
                    return t;
                return value?.GetType();
            }
        }

        protected override string GetDefaultMessageSinceToken(object obj)
        {
            return $"The given type is not a derived class of {_type.GetFriendlyName()} or its implementation. The current type is {VerifiableMember.MemberType.GetFriendlyName()}.";
        }
    }

    internal class ValueRequiredTypeToken<T> : ValueRequiredTypeToken
    {
        public ValueRequiredTypeToken(VerifiableMemberContract contract) : base(contract, typeof(T)) { }
    }
}