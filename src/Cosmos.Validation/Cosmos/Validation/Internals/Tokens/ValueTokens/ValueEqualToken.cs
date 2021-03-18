using System;
using System.Collections;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Internals.Tokens.ValueTokens.Basic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Equal token
    /// </summary>
    internal class ValueEqualToken : ValueRequiredBasicToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueEqualToken";

        private readonly object _valueToCompare;
        private readonly Type _typeOfValueToCompare;
        private readonly IEqualityComparer _comparer;

        /// <inheritdoc />
        public ValueEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer)
            : this(contract, valueToCompare, comparer, false, NAME) { }

        protected ValueEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer, bool not, string tokenName)
            : base(contract, not, tokenName)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = _valueToCompare.GetType();
            _comparer = comparer;
        }

        protected override bool IsValidImpl(object value)
        {
            if (_comparer != null)
            {
                return ConclusionReversal(_comparer.Equals(_valueToCompare, value));
            }

            if (VerifiableMember.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.Valid(VerifiableMember.MemberType, value, _typeOfValueToCompare, _valueToCompare))
                    return ConclusionReversal(true);
            }

            return ConclusionReversal(Equals(_valueToCompare, value));
        }

        protected override string GetDefaultMessageSinceToken(object obj) => $"The two values given must be equal. The current value is: {obj} and the value being compared is {_valueToCompare}.";
    }

    /// <summary>
    /// Equal token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal class ValueEqualToken<TVal> : ValueRequiredBasicToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueEqualToken";

        private readonly TVal _valueToCompare;
        private readonly IEqualityComparer<TVal> _comparer;

        /// <inheritdoc />
        public ValueEqualToken(VerifiableMemberContract contract, TVal valueToCompare, IEqualityComparer<TVal> comparer)
            : this(contract, valueToCompare, comparer, false, NAME) { }

        protected ValueEqualToken(VerifiableMemberContract contract, TVal valueToCompare, IEqualityComparer<TVal> comparer, bool not, string tokenName)
            : base(contract, not, tokenName)
        {
            _valueToCompare = valueToCompare;
            _comparer = comparer;
        }

        protected override bool IsValidImpl(TVal value)
        {
            if (_comparer != null)
            {
                return ConclusionReversal(_comparer.Equals(_valueToCompare, value));
            }

            return ConclusionReversal(Equals(_valueToCompare, value));
        }

        protected override string GetDefaultMessageSinceToken(TVal obj) => $"The two values given must be equal. The current value is: {obj} and the value being compared is {_valueToCompare}.";
    }

    /// <summary>
    /// Not equal token
    /// </summary>
    internal class ValueNotEqualToken : ValueEqualToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueNotEqualToken";

        /// <inheritdoc />
        public ValueNotEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer)
            : base(contract, valueToCompare, comparer, true, NAME) { }

        protected override string GetDefaultMessageSinceToken(object obj) => $"The values must not be equal. The current value type is: {VerifiableMember.MemberType.GetFriendlyName()}.";
    }

    /// <summary>
    /// Not equal token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal class ValueNotEqualToken<TVal> : ValueEqualToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueNotEqualToken";

        /// <inheritdoc />
        public ValueNotEqualToken(VerifiableMemberContract contract, TVal valueToCompare, IEqualityComparer<TVal> comparer)
            : base(contract, valueToCompare, comparer, true, NAME) { }

        protected override string GetDefaultMessageSinceToken(TVal obj) => $"The values must not be equal. The current value type is: {VerifiableMember.MemberType.GetFriendlyName()}.";
    }
}