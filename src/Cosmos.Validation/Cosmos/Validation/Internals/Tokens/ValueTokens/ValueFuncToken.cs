﻿using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Func token
    /// </summary>
    internal class ValueFuncToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "Value Func condition rule";
        private readonly Func<object, CustomVerifyResult> _func;

        /// <inheritdoc />
        public ValueFuncToken(VerifiableMemberContract contract, Func<object, CustomVerifyResult> func) : base(contract)
        {
            _func = func;
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => NAME;

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
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var result))
            {
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
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var result))
            {
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