﻿using System;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueLessThanToken<TVal> : ValueToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueLessThanToken";

        private readonly TVal _valueToCompare;
        private readonly Type _typeOfValueToCompare;

        public ValueLessThanToken(VerifiableMemberContract contract, TVal valueToCompare) : base(contract)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = typeof(TVal);
        }

        public override CorrectValueOps Ops => CorrectValueOps.LessThan_T1;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(TVal value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var success = ValidCore(value, out var message);

            if (!success)
            {
                UpdateVal(val, value, message);
            }

            return val;
        }

        private bool ValidCore(TVal value, out string message)
        {
            message = null;

            if (VerifiableMember.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.CompareTo(VerifiableMember.MemberType, value, _typeOfValueToCompare, _valueToCompare) < 0)
                {
                    return true;
                }
            }

            else if (value is IComparable c1 && _valueToCompare is IComparable c2)
            {
                if (c1.CompareTo(c2) < 0)
                    return true;
            }

            message = $"Neither the given value nor the value being compared can be converted to IComparable<{_typeOfValueToCompare.GetFriendlyName()}>.";

            return false;
        }

        private void UpdateVal(CorrectVerifyVal val, TVal obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value must be less than {_valueToCompare}.");
        }

        public override string ToString() => NAME;
    }
}