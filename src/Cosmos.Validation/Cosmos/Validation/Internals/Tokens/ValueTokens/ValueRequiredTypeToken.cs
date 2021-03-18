using System;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Required type token
    /// </summary>
    internal class ValueRequiredTypeToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredTypeToken";
        private readonly Type _type;

        /// <inheritdoc />
        public ValueRequiredTypeToken(VerifiableMemberContract contract, Type type) : base(contract)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => NAME;

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public override bool MutuallyExclusive => true;

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        /// <summary>
        /// Verification for VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, GetValueFrom(context));
            }

            return verifyVal;
        }

        /// <summary>
        /// Verification for VerifiableMemberContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, GetValueFrom(context));
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value)
        {
            var realType = _getType();

            if (VerifiableMember.MemberType == _type || VerifiableMember.MemberType.IsDerivedFrom(_type))
                return true;

            if(realType is not null && (realType == _type || realType.IsDerivedFrom(_type)))
                return true;

            return false;

            Type _getType()
            {
                if (value is Type t)
                    return t;
                return value?.GetType();
            }
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The given type is not a derived class of {_type.GetFriendlyName()} or its implementation. The current type is {VerifiableMember.MemberType.GetFriendlyName()}.");
        }
    }
    
    internal class ValueRequiredTypeToken<T> : ValueRequiredTypeToken
    {
        public ValueRequiredTypeToken(VerifiableMemberContract contract) : base(contract, typeof(T)) { }
    }
}