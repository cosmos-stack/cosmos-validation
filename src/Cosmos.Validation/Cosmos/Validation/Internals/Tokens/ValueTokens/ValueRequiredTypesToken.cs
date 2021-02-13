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

        public ValueRequiredTypesToken(ObjectValueContract contract, params Type[] types) : base(contract)
        {
            _types = types;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Types;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (_types.Any())
            {
                var flag = false;
                var typeOfValue = value?.GetType();

                foreach (var type in _types)
                {
                    if (Member.MemberType == type || (typeOfValue != null && typeOfValue == type))
                    {
                        flag = true;
                        break;
                    }

                    if (Member.MemberType.IsDerivedFrom(type))
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    UpdateVal(val, value);
                }
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The given type is not a derived class or implementation of any one of the types in the list. The current type is {Member.MemberType.GetFriendlyName()}.");
        }

        public override string ToString() => NAME;
    }
}