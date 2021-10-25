﻿using System;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Func token
    /// </summary>
    internal class ValueFuncToken : ValueToken
    {
        private const string Name = "Value Func condition rule";

        private readonly Func<object, CustomVerifyResult> _func;

        /// <inheritdoc />
        public ValueFuncToken(VerifiableMemberContract contract, Func<object, CustomVerifyResult> func) : base(contract)
        {
            _func = func;
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => Name;

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public override bool MutuallyExclusive => false;

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        /// <summary>
        /// Verification for VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var value = GetValueFrom(context);

            if (!IsActivate(context, value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value, out var result))
            {
                verifyVal.NameOfExecutedRule = result?.OperationName ?? Name;
                UpdateVal(verifyVal, value, result?.ErrorMessage);
            }

            return verifyVal;
        }

        /// <summary>
        /// Verification for VerifiableMemberContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var value = GetValueFrom(context);

            if (!IsActivate(context, value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value, out var result))
            {
                verifyVal.NameOfExecutedRule = result?.OperationName ?? Name;
                UpdateVal(verifyVal, value, result?.ErrorMessage);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value, out CustomVerifyResult result)
        {
            result = _func.Invoke(value);
            return result?.VerifyResult ?? false;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = "The value does not satisfy the given Func condition.";

            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message);
        }
    }
}