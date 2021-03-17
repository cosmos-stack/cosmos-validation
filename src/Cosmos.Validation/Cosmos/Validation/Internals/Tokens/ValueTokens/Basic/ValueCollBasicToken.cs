using System;
using System.Collections;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens.Basic
{
    internal abstract class ValueCollBasicToken : ValueToken
    {
        private readonly Func<object, bool> _func;

        protected ValueCollBasicToken(VerifiableMemberContract contract, Func<object, bool> func, string tokenName) : base(contract)
        {
            _func = func;

            TokenName = tokenName;
        }

        public override string TokenName { get; }

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (ContainsMember(context) && Types.IsCollectionType(VerifiableMember.MemberType) && value is ICollection collection)
            {
                if (!IsValidImpl(collection, _func))
                {
                    UpdateVal(verifyVal, value);
                }
            }
            else
            {
                UpdateVal(verifyVal, value, $"The type is not a collection or an array, and an exception occurs when using {TokenName}.");
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (context is not null && Types.IsCollectionType(VerifiableMember.MemberType) && value is ICollection collection)
            {
                if (!IsValidImpl(collection, _func))
                {
                    UpdateVal(verifyVal, value);
                }
            }
            else
            {
                UpdateVal(verifyVal, value, $"The type is not a collection or an array, and an exception occurs when using {TokenName}.");
            }

            return verifyVal;
        }

        protected abstract bool IsValidImpl(ICollection collection, Func<object, bool> func);

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? "There are no members that meet the conditions in the array or collection.");
        }

        public override string ToString() => TokenName;
    }
}