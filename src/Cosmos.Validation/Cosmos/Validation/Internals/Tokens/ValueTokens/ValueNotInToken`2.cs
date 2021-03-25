using System.Collections.Generic;
using System.Linq;
using Cosmos.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Not-In token, a generic version with two generic types.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    internal class ValueNotInToken<TVal, TItem> : ValueToken<TVal>
        where TVal : IEnumerable<TItem>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueInToken2";

        private readonly ICollection<TItem> _objects;

        /// <inheritdoc />
        public ValueNotInToken(VerifiableMemberContract contract, ICollection<TItem> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<TItem>();
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
            return items.All(item => !_objects.Contains(item));
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is contained in the given value array or collection.");
        }
    }
}