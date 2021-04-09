using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Range token, a generic version
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal class ValueRangeToken<TVal> : ValueToken<TVal>
    {
        private const string Name = "GenericValueRangeToken";
        
        private readonly TVal _from;
        private readonly TVal _to;
        private readonly RangeOptions _options;

        /// <inheritdoc />
        public ValueRangeToken(VerifiableMemberContract contract, TVal from, TVal to, RangeOptions options) : base(contract)
        {
            _from = from;
            _to = to;

            _options = options;
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

            if (!IsValidImpl(value, out var currentValue, out var message))
            {
                UpdateVal(verifyVal, currentValue, message);
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
    }
}