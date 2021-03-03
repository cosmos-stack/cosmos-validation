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

        public override CorrectValueOps Ops => CorrectValueOps.Type;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};
            var flag = false;

            if (VerifiableMember.MemberType == _type)
            {
                flag = true;
            }

            else if (value != null && VerifiableMember.MemberType == _type)
            {
                flag = true;
            }

            else if (VerifiableMember.MemberType.IsDerivedFrom(_type))
            {
                flag = true;
            }

            if (!flag)
            {
                UpdateVal(val, value);
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The given type is not a derived class of {_type.GetFriendlyName()} or its implementation. The current type is {VerifiableMember.MemberType.GetFriendlyName()}.");
        }

        public override string ToString() => NAME;
    }
}