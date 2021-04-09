using System;
using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Not-In token, a generic version with one generic type.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal class ValueNotInToken<TVal> : ValueToken<TVal>
    {
        private const string Name = "GenericValueInToken";

        private readonly ICollection<TVal> _objects;
        private readonly Func<ICollection<TVal>> _objectsFunc;

        /// <inheritdoc />
        public ValueNotInToken(VerifiableMemberContract contract, ICollection<TVal> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<TVal>();
            _objectsFunc = null;
        }

        /// <inheritdoc />
        public ValueNotInToken(VerifiableMemberContract contract, Func<ICollection<TVal>> objectsFunc) : base(contract)
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

        private bool IsValidImpl(TVal value)
        {
            var coll = _objectsFunc is null ? _objects : _objectsFunc.Invoke();

            return !coll.Contains(value);
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is contained in the given value array or collection.");
        }
    }
}