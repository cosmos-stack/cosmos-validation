﻿using System;
using System.Collections.Generic;
using System.Linq;
using CosmosStack.Collections;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// In token, a generic version with two generic types.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    internal class ValueInToken<TVal, TItem> : ValueToken<TVal>
        where TVal : IEnumerable<TItem>
    {
        private const string Name = "GenericValueInToken2";

        private readonly ICollection<TItem> _objects;
        private readonly Func<ICollection<TItem>> _objectsFunc;

        /// <inheritdoc />
        public ValueInToken(VerifiableMemberContract contract, ICollection<TItem> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<TItem>();
            _objectsFunc = null;
        }

        /// <inheritdoc />
        public ValueInToken(VerifiableMemberContract contract, Func<ICollection<TItem>> objectsFunc) : base(contract)
        {
            _objects = default;
            _objectsFunc = objectsFunc;
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

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
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

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private bool IsValidImpl(IEnumerable<TItem> items)
        {
            var coll = _objectsFunc is null ? _objects : _objectsFunc.Invoke();

            return items.Any(item => coll.Contains(item));
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is not contained in the given value array or collection.");
        }
    }
}