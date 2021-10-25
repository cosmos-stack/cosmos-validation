﻿using System;
using System.Collections;
using System.Collections.Generic;
using CosmosStack.Reflection;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens.Basic
{
    /// <summary>
    /// Abstract basic token for collection type, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    internal abstract class ValueCollBasicToken<TVal, TItem> : ValueToken<TVal>
        where TVal : IEnumerable<TItem>
    {
        private readonly Func<TItem, bool> _func;

        /// <inheritdoc />
        protected ValueCollBasicToken(VerifiableMemberContract contract, Func<TItem, bool> func, string tokenName) : base(contract)
        {
            _func = func;

            TokenName = tokenName;
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName { get; }

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

        /// <summary>
        /// Impl of valid ops
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected abstract bool IsValidImpl(ICollection collection, Func<TItem, bool> func);

        private void UpdateVal(CorrectVerifyVal val, TVal obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? "There are no members that meet the conditions in the array or collection.");
        }
    }
}