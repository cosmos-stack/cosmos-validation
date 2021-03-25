using System;
using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// In token, a generic version with one generic type.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal class ValueInToken<TVal> : ValueToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueInToken";

        private readonly ICollection<TVal> _objects;

        /// <inheritdoc />
        public ValueInToken(VerifiableMemberContract contract, ICollection<TVal> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<TVal>();
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
            var value = GetValueFrom(context);

            if (!IsActivate(context, value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!_objects.Contains(value))
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

            if (!_objects.Contains(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is not contained in the given value array or collection.");
        }
    }
}