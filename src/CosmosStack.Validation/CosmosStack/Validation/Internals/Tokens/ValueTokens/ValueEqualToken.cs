using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using CosmosStack.Validation.Internals.Extensions;
using CosmosStack.Validation.Internals.Tokens.ValueTokens.Basic;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Equal token
    /// </summary>
    internal class ValueEqualToken : ValueRequiredBasicToken
    {
        private const string Name = "ValueEqualToken";

        private readonly object _valueToCompare;
        private readonly Func<object> _valueToCompareFunc;
        private readonly AsyncLocal<object> _cache = new();
        private readonly Type _typeOfValueToCompare;
        private readonly IEqualityComparer _comparer;

        /// <inheritdoc />
        public ValueEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer)
            : this(contract, valueToCompare, comparer, false, Name) { }

        /// <inheritdoc />
        public ValueEqualToken(VerifiableMemberContract contract, Func<object> valueToCompareFunc, Type typeOfCompareVal, IEqualityComparer comparer)
            : this(contract, valueToCompareFunc, typeOfCompareVal, comparer, false, Name) { }

        protected ValueEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer, bool not, string tokenName)
            : base(contract, not, tokenName)
        {
            _valueToCompare = valueToCompare;
            _valueToCompareFunc = null;
            _typeOfValueToCompare = _valueToCompare.GetType();
            _comparer = comparer;
        }

        protected ValueEqualToken(VerifiableMemberContract contract, Func<object> valueToCompareFunc, Type typeOfCompareVal, IEqualityComparer comparer, bool not, string tokenName)
            : base(contract, not, tokenName)
        {
            _valueToCompare = null;
            _valueToCompareFunc = valueToCompareFunc;
            _typeOfValueToCompare = typeOfCompareVal;
            _comparer = comparer;
        }

        protected override bool IsValidImpl(object value)
        {
            var valueToCompare = _valueToCompareFunc is null ? _valueToCompare : _valueToCompareFunc.Invoke();

            _cache.Value = valueToCompare;

            if (_comparer is not null)
            {
                return ConclusionReversal(_comparer.Equals(valueToCompare, value));
            }

            if (VerifiableMember.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.Valid(VerifiableMember.MemberType, value, _typeOfValueToCompare, valueToCompare))
                    return ConclusionReversal(true);
            }

            return ConclusionReversal(Equals(valueToCompare, value));
        }

        protected override string GetDefaultMessageSinceToken(object obj) => $"The two values given must be equal. The current value is: {obj} and the value being compared is {_cache.Value}.";
    }

    /// <summary>
    /// Equal token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal class ValueEqualToken<TVal> : ValueRequiredBasicToken<TVal>
    {
        private const string Name = "GenericValueEqualToken";

        private readonly TVal _valueToCompare;
        private readonly Func<TVal> _valueToCompareFunc;
        private readonly AsyncLocal<TVal> _cache = new();
        private readonly IEqualityComparer<TVal> _comparer;

        /// <inheritdoc />
        public ValueEqualToken(VerifiableMemberContract contract, TVal valueToCompare, IEqualityComparer<TVal> comparer)
            : this(contract, valueToCompare, comparer, false, Name) { }

        /// <inheritdoc />
        public ValueEqualToken(VerifiableMemberContract contract, Func<TVal> valueToCompareFunc, IEqualityComparer<TVal> comparer)
            : this(contract, valueToCompareFunc, comparer, false, Name) { }

        protected ValueEqualToken(VerifiableMemberContract contract, TVal valueToCompare, IEqualityComparer<TVal> comparer, bool not, string tokenName)
            : base(contract, not, tokenName)
        {
            _valueToCompare = valueToCompare;
            _valueToCompareFunc = null;
            _comparer = comparer;
        }

        protected ValueEqualToken(VerifiableMemberContract contract, Func<TVal> valueToCompareFunc, IEqualityComparer<TVal> comparer, bool not, string tokenName)
            : base(contract, not, tokenName)
        {
            _valueToCompare = default;
            _valueToCompareFunc = valueToCompareFunc;
            _comparer = comparer;
        }

        protected override bool IsValidImpl(TVal value)
        {
            var valueToCompare = _valueToCompareFunc is null ? _valueToCompare : _valueToCompareFunc.Invoke();

            _cache.Value = valueToCompare;

            if (_comparer is not null)
            {
                return ConclusionReversal(_comparer.Equals(valueToCompare, value));
            }

            return ConclusionReversal(Equals(valueToCompare, value));
        }

        protected override string GetDefaultMessageSinceToken(TVal obj) => $"The two values given must be equal. The current value is: {obj} and the value being compared is {_cache.Value}.";
    }

    /// <summary>
    /// Not equal token
    /// </summary>
    internal class ValueNotEqualToken : ValueEqualToken
    {
        private const string Name = "ValueNotEqualToken";

        /// <inheritdoc />
        public ValueNotEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer)
            : base(contract, valueToCompare, comparer, true, Name) { }

        /// <inheritdoc />
        public ValueNotEqualToken(VerifiableMemberContract contract, Func<object> valueToCompareFunc, Type typeOfCompareVal, IEqualityComparer comparer)
            : base(contract, valueToCompareFunc, typeOfCompareVal, comparer, true, Name) { }

        protected override string GetDefaultMessageSinceToken(object obj) => $"The values must not be equal. The current value type is: {VerifiableMember.MemberType.GetFriendlyName()}.";
    }

    /// <summary>
    /// Not equal token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal class ValueNotEqualToken<TVal> : ValueEqualToken<TVal>
    {
        private const string Name = "GenericValueNotEqualToken";

        /// <inheritdoc />
        public ValueNotEqualToken(VerifiableMemberContract contract, TVal valueToCompare, IEqualityComparer<TVal> comparer)
            : base(contract, valueToCompare, comparer, true, Name) { }

        /// <inheritdoc />
        public ValueNotEqualToken(VerifiableMemberContract contract, Func<TVal> valueToCompareFunc, IEqualityComparer<TVal> comparer)
            : base(contract, valueToCompareFunc, comparer, true, Name) { }

        protected override string GetDefaultMessageSinceToken(TVal obj) => $"The values must not be equal. The current value type is: {VerifiableMember.MemberType.GetFriendlyName()}.";
    }
}