using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRangeToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRangeToken";
        private readonly IComparable _from;
        private readonly IComparable _to;
        private readonly RangeOptions _options;

        private bool _returnFalseDirectly;

        public ValueRangeToken(VerifiableMemberContract contract, object from, object to, RangeOptions options) : base(contract)
        {
            if (from is null || to is null)
            {
                _from = default;
                _to = default;
                _returnFalseDirectly = true;
            }

            if (!_returnFalseDirectly && from is IComparable from0)
            {
                _from = from0;
            }
            else
            {
                _from = default;
                _returnFalseDirectly = true;
            }

            if (!_returnFalseDirectly && to is IComparable to0)
            {
                _to = to0;
            }
            else
            {
                _to = default;
                _returnFalseDirectly = true;
            }

            if (!_returnFalseDirectly && _from!.CompareTo(_to) > 0)
            {
                _returnFalseDirectly = true;
            }

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

        private bool IsValidImpl(object value, out object currentValue, out string message)
        {
            message = "";
            currentValue = value;

            if (_returnFalseDirectly)
            {
                return false;
            }

            else if (value is null)
            {
                currentValue = null;
                return false;
            }

            else if (value is char c)
            {
                var fromC = Convert.ToChar(_from);
                var toC = Convert.ToChar(_to);
                currentValue = c;

                if (_options == RangeOptions.OpenInterval)
                {
                    // Open Interval
                    if (c <= fromC || c >= toC)
                    {
                        return false;
                    }
                }
                else
                {
                    // Close Interval
                    if (c < fromC || c > toC)
                    {
                        return false;
                    }
                }
            }

            else if (VerifiableMember.MemberType.IsPrimitive && VerifiableMember.MemberType.IsValueType)
            {
                var d = Convert.ToDecimal(value);
                var fromD = Convert.ToDecimal(_from);
                var toD = Convert.ToDecimal(_to);

                currentValue = d;
                if (_options == RangeOptions.OpenInterval)
                {
                    // Open Interval
                    if (d <= fromD || d >= toD)
                    {
                        return false;
                    }
                }
                else
                {
                    // Close Interval
                    if (d < fromD || d > toD)
                    {
                        return false;
                    }
                }
            }

            else if (value is DateTime || value is DateTimeOffset)
            {
                var t = ValueTypeEqualCalculator.ToLongTimeTicks(value);
                var fromT = ValueTypeEqualCalculator.ToLongTimeTicks(_from);
                var toT = ValueTypeEqualCalculator.ToLongTimeTicks(_to);

                if (_options == RangeOptions.OpenInterval)
                {
                    // Open Interval
                    if (t <= fromT || t >= toT)
                    {
                        return false;
                    }
                }
                else
                {
                    // Close Interval
                    if (t < fromT || t > toT)
                    {
                        return false;
                    }
                }
            }

            else if (value is IComparable comparable)
            {
                currentValue = comparable;

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

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"The given value is not in the valid range. The current value is: {obj}, and the valid range is from {_from} to {_to}.");
        }

        public override string ToString() => NAME;
    }
}