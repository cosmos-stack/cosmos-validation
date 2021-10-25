using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CosmosStack.Collections;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// In token
    /// </summary>
    internal class ValueInToken : ValueToken
    {
        private const string Name = "ValueInToken";

        private readonly ICollection<object> _objects;
        private readonly Func<ICollection<object>> _objectsFunc;

        /// <inheritdoc />
        public ValueInToken(VerifiableMemberContract contract, ICollection<object> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<object>();
            _objectsFunc = null;
        }

        /// <inheritdoc />
        public ValueInToken(VerifiableMemberContract contract, Func<ICollection<object>> objectsFunc) : base(contract)
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

        private bool IsValidImpl(object value)
        {
            var coll = _objectsFunc is null ? _objects : _objectsFunc.Invoke();
            
            if (value is ICollection collection)
            {
                if (collection.Cast<object>().Any(item => coll.Contains(item)))
                {
                    return true;
                }
            }

            return coll.Contains(value);
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? "The value is not contained in the given value array or collection.");
        }
    }
}