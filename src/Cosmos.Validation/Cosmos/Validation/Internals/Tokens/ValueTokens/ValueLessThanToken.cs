﻿using System;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Less than token
    /// </summary>
    internal class ValueLessThanToken : ValueCompareBasicToken
    {
        private const string Name = "ValueLessThanToken";

        /// <inheritdoc />
        public ValueLessThanToken(VerifiableMemberContract contract, object valueToCompare) : base(contract, valueToCompare, Name) { }

        /// <inheritdoc />
        public ValueLessThanToken(VerifiableMemberContract contract, Func<object> valueToCompareFunc, Type valueType) : base(contract, valueToCompareFunc, valueType, Name) { }

        protected ValueLessThanToken(VerifiableMemberContract contract, object valueToCompare, string tokenName) : base(contract, valueToCompare, tokenName) { }

        protected ValueLessThanToken(VerifiableMemberContract contract, Func<object> valueToCompareFunc, Type valueType, string tokenName) : base(contract, valueToCompareFunc, valueType, tokenName) { }

        /// <summary>
        /// Impl of valid ops.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueToCompare"></param>
        /// <param name="typeOfValueToCompare"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(object value, object valueToCompare, Type typeOfValueToCompare, out string message)
        {
            message = null;

            if (VerifiableMember.MemberType.IsValueType && typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.CompareTo(VerifiableMember.MemberType, value, typeOfValueToCompare, valueToCompare) < 0)
                {
                    return true;
                }
            }

            else if (value is IComparable c1 && valueToCompare is IComparable c2)
            {
                if (c1.CompareTo(c2) < 0)
                    return true;
            }

            message = "Neither the given value nor the value being compared can be converted to IComparable.";
            return false;
        }

        /// <inheritdoc />
        protected override void UpdateVal(CorrectVerifyVal val, object obj, object valueToCompare, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value must be less than {valueToCompare}.");
        }
    }

    internal class ValueLessThanToken<TVal> : ValueLessThanToken
    {
        private const string Name = "GenericValueLessThanToken";

        /// <inheritdoc />
        public ValueLessThanToken(VerifiableMemberContract contract, TVal valueToCompare) : base(contract, valueToCompare, Name) { }

        /// <inheritdoc />
        public ValueLessThanToken(VerifiableMemberContract contract, Func<TVal> valueToCompareFunc) : base(contract, () => valueToCompareFunc(), typeof(TVal), Name) { }
    }
}