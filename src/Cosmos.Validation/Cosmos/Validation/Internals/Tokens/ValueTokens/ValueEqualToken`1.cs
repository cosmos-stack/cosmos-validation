﻿using System.Collections.Generic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueEqualToken<TVal> : ValueToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueEqualToken";

        private readonly TVal _valueToCompare;
        private readonly IEqualityComparer<TVal> _comparer;

        public ValueEqualToken(VerifiableMemberContract contract, TVal valueToCompare, IEqualityComparer<TVal> comparer) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _comparer = comparer;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Equal_T1;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(TVal value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var success = ValidCore(value);

            if (!success)
            {
                UpdateVal(val, value);
            }

            return val;
        }

        private bool ValidCore(TVal value)
        {
            if (_comparer != null)
            {
                return _comparer.Equals(_valueToCompare, value);
            }

            return Equals(_valueToCompare, value);
        }

        private void UpdateVal(CorrectVerifyVal val, TVal obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The two values given must be equal. The current value is: {obj} and the value being compared is {_valueToCompare}.");
        }

        public override string ToString() => NAME;
    }
}