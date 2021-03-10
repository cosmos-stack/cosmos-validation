using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueAnyToken<TVal, TItem> : ValueToken<TVal>
        where TVal : IEnumerable<TItem>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueAnyToken";

        private readonly Func<TItem, bool> _func;

        public ValueAnyToken(VerifiableMemberContract contract, Func<TItem, bool> func) : base(contract)
        {
            _func = func;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Any_T1;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};

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
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};

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
            return collection.Cast<TItem>().Any(one => _func.Invoke(one));
        }

        private void UpdateVal(CorrectVerifyVal val, TVal obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? "There are no members that meet the conditions in the array or collection.");
        }

        public override string ToString() => NAME;
    }
}