using System;
using System.Threading;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens.Basic
{
    /// <summary>
    /// Abstract basic token for compare type.
    /// </summary>
    internal abstract class ValueCompareBasicToken : ValueToken
    {
        private readonly object _valueToCompare;
        private readonly Func<object> _valueToCompareFunc;
        private readonly Type _typeOfValueToCompare;

        /// <inheritdoc />
        protected ValueCompareBasicToken(VerifiableMemberContract contract, object valueToCompare, string tokenName) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _valueToCompareFunc = null;
            _typeOfValueToCompare = _valueToCompare.GetType();
            TokenName = tokenName;
        }

        /// <inheritdoc />
        protected ValueCompareBasicToken(VerifiableMemberContract contract, Func<object> valueToCompareFunc, Type valueType, string tokenName) : base(contract)
        {
            _valueToCompare = default;
            _valueToCompareFunc = valueToCompareFunc;
            _typeOfValueToCompare = valueType;
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

            var valueToCompare = _valueToCompareFunc is null ? _valueToCompare : _valueToCompareFunc.Invoke();

            if (!IsValidImpl(value, valueToCompare, _typeOfValueToCompare, out var message))
            {
                UpdateVal(verifyVal, value, message);
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

            var valueToCompare = _valueToCompareFunc is null ? _valueToCompare : _valueToCompareFunc.Invoke();

            if (!IsValidImpl(value, valueToCompare, _typeOfValueToCompare, out var message))
            {
                UpdateVal(verifyVal, value, message);
            }

            return verifyVal;
        }

        /// <summary>
        /// Impl of valid ops.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueToCompare"></param>
        /// <param name="typeOfValueToCompare"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected abstract bool IsValidImpl(object value, object valueToCompare, Type typeOfValueToCompare, out string message);

        protected virtual void UpdateVal(CorrectVerifyVal val, object obj, object valueToCompare, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value does not meet the requirements.");
        }
    }
}