using System;
using System.Collections;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueNotEqualToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueNotEqualToken";

        private readonly object _valueToCompare;
        private readonly Type _typeOfValueToCompare;
        private readonly IEqualityComparer _comparer;

        public ValueNotEqualToken(VerifiableMemberContract contract, object valueToCompare, IEqualityComparer comparer) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = _valueToCompare.GetType();
            _comparer = comparer;
        }

        public override CorrectValueOps Ops => CorrectValueOps.NotEqual;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var success = !ValidCore(value);

            if (!success)
            {
                UpdateVal(val, value);
            }

            return val;
        }

        private bool ValidCore(object value)
        {
            if (_comparer != null)
            {
                return _comparer.Equals(_valueToCompare, value);
            }

            if (VerifiableMember.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.Valid(VerifiableMember.MemberType, value, _typeOfValueToCompare, _valueToCompare))
                    return true;
            }

            return Equals(_valueToCompare, value);
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The values must not be equal. The current value type is: {VerifiableMember.MemberType.GetFriendlyName()}.");
        }

        public override string ToString() => NAME;
    }
}