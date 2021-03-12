using System;
using System.Linq;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredTypesToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredTypesToken";
        private readonly Type[] _types;

        public ValueRequiredTypesToken(VerifiableMemberContract contract, params Type[] types) : base(contract)
        {
            _types = types;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Types;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value)
        {
            if (!_types.Any())
                return true;

            var realType = _getType();

            foreach (var type in _types)
            {
                if (VerifiableMember.MemberType == type || VerifiableMember.MemberType.IsDerivedFrom(type))
                    return true;

                if (realType is not null && (realType == type || realType.IsDerivedFrom(type)))
                    return true;
            }

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
            val.ErrorMessage = MergeMessage($"The given type is not a derived class or implementation of any one of the types in the list. The current type is {VerifiableMember.MemberType.GetFriendlyName()}.");
        }

        public override string ToString() => NAME;
    }
}