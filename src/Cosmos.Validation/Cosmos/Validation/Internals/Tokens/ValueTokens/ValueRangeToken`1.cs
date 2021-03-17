using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRangeToken<TVal> : ValueToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueRangeToken";
        private readonly TVal _from;
        private readonly TVal _to;
        private readonly RangeOptions _options;

        public ValueRangeToken(VerifiableMemberContract contract, TVal from, TVal to, RangeOptions options) : base(contract)
        {
            _from = from;
            _to = to;

            _options = options;
        }

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var currentValue, out var message))
            {
                UpdateVal(verifyVal, currentValue, message);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var currentValue, out var message))
            {
                UpdateVal(verifyVal, currentValue, message);
            }

            return verifyVal;
        }

        private bool IsValidImpl(TVal value, out TVal currentValue, out string message)
        {
            message = "";
            currentValue = value;

            if (value is null)
            {
                currentValue = default;
                return false;
            }

            else if (value is IComparable<TVal> comparable)
            {
                if (_options == RangeOptions.OpenInterval)
                {
                    // Open Interval
                    if (comparable.CompareTo(_from) <= 0 || comparable.CompareTo(_to) >= 0)
                    {
                        return false;
                    }
                }
                else
                {
                    // Close Interval
                    if (comparable.CompareTo(_from) < 0 || comparable.CompareTo(_to) > 0)
                    {
                        return false;
                    }
                }
            }

            else
            {
                message = "The given value cannot be compared.";

                return false;
            }

            return true;
        }

        private void UpdateVal(CorrectVerifyVal val, TVal obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value is not in the valid range. The current value is: {obj}, and the valid range is from {_from} to {_to}.");
        }

        public override string ToString() => NAME;
    }
}