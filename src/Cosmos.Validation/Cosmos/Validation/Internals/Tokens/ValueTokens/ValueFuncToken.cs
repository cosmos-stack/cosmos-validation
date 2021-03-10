﻿using System;
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

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
           
            var value = GetValueFrom(context);

            var result = _func.Invoke(value);

            if (result is not null && !result.VerifyResult)
            {
                UpdateVal(verifyVal, value, result.ErrorMessage);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
            
            var value = GetValueFrom(context);

            var result = _func.Invoke(value);

            if (result is not null && !result.VerifyResult)
            {
                UpdateVal(verifyVal, value, result.ErrorMessage);
            }

            return verifyVal;
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