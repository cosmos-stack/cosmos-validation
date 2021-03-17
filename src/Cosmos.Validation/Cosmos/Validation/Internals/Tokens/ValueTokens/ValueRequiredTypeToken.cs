using System;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredTypeToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredTypeToken";
        private readonly Type _type;

        public ValueRequiredTypeToken(VerifiableMemberContract contract, Type type) : base(contract)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, GetValueFrom(context));
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
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

        public override string ToString() => NAME;
    }
    
    internal class ValueRequiredTypeToken<T> : ValueRequiredTypeToken
    {
        public ValueRequiredTypeToken(VerifiableMemberContract contract) : base(contract, typeof(T)) { }
    }
}