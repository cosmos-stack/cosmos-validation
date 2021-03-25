using System;
using System.Linq;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// String-enum token
    /// </summary>
    internal class ValueStringEnumToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueStringEnumToken";

        private readonly Type _enumType;
        private readonly bool _caseSensitive;

        /// <inheritdoc />
        public ValueStringEnumToken(VerifiableMemberContract contract, Type enumType, bool caseSensitive) : base(contract)
        {
            _enumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
            _caseSensitive = caseSensitive;

            if (!enumType.IsEnum)
            {
                throw new ArgumentOutOfRangeException(nameof(enumType), $"The type '{enumType.Name}' is not an enum and can't be used with IsEnumName.");
            }
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => NAME;

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
            
            if(!IsActivate(value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
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
            
            if(!IsActivate(value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value)
        {
            if (value is null) return true;

            var stringValue = value.ToString();
            var comparison = _caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            return Enum.GetNames(_enumType).Any(x => x.Equals(stringValue, comparison));
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The given value is not a member of the specified enumeration type.");
        }
    }

    /// <summary>
    /// String-enum token, a generic version.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    internal class ValueStringEnumToken<TEnum> : ValueStringEnumToken
    {
        /// <inheritdoc />
        public ValueStringEnumToken(VerifiableMemberContract contract, bool caseSensitive) : base(contract, typeof(TEnum), caseSensitive) { }
    }
}