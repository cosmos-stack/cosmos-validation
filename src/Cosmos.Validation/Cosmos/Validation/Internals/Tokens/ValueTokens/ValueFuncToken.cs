using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueFuncToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "Value Func condition rule";
        private readonly Func<object, CustomVerifyResult> _func;

        public ValueFuncToken(VerifiableMemberContract contract, Func<object, CustomVerifyResult> func) : base(contract)
        {
            _func = func;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Func;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var result = _func.Invoke(value);

            if (result != null && !result.VerifyResult)
            {
                UpdateVal(val, value, result.ErrorMessage);
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "The value does not satisfy the given Func condition.";

            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message);
        }

        public override string ToString() => NAME;
    }
}