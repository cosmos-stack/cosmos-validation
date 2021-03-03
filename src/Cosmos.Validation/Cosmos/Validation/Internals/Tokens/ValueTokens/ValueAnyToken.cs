using System;
using System.Collections;
using System.Linq;
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

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};
            var flag = false;

            if (value is ICollection collection)
            {
                if (collection.Cast<object>().Any(one => _func.Invoke(one)))
                {
                    flag = true;
                }

                if (!flag)
                {
                    UpdateVal(val, value);
                }
            }
            else
            {
                UpdateVal(val, value, "The type is not a collection or an array, and an exception occurs when using AntToken.");
            }

            return val;
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