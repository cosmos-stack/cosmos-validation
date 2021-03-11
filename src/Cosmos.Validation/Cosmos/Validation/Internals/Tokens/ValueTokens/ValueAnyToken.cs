using System;
using System.Collections;
using System.Linq;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueAnyToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueAnyToken";

        private readonly Func<object, bool> _func;

        public ValueAnyToken(VerifiableMemberContract contract, Func<object, bool> func) : base(contract)
        {
            _func = func;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Any;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (ContainsMember(context) && Types.IsCollectionType(VerifiableMember.MemberType) && value is ICollection collection)
            {
                if (!IsValidImpl(collection))
                {
                    UpdateVal(verifyVal, value);
                }
            }
            else
            {
                UpdateVal(verifyVal, value, "The type is not a collection or an array, and an exception occurs when using AnyToken.");
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (context is not null && Types.IsCollectionType(VerifiableMember.MemberType) && value is ICollection collection)
            {
                if (!IsValidImpl(collection))
                {
                    UpdateVal(verifyVal, value);
                }
            }
            else
            {
                UpdateVal(verifyVal, value, "The type is not a collection or an array, and an exception occurs when using AnyToken.");
            }

            return verifyVal;
        }

        private bool IsValidImpl(ICollection collection)
        {
            return collection.Cast<object>().Any(one => _func.Invoke(one));
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? "There are no members that meet the conditions in the array or collection.");
        }

        public override string ToString() => NAME;
    }
}