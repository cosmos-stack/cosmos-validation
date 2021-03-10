using System;
using System.Linq;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueStringEnumToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueStringEnumToken";

        private readonly Type _enumType;
        private readonly bool _caseSensitive;

        public ValueStringEnumToken(VerifiableMemberContract contract, Type enumType, bool caseSensitive) : base(contract)
        {
            _enumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
            _caseSensitive = caseSensitive;

            if (!enumType.IsEnum)
            {
                throw new ArgumentOutOfRangeException(nameof(enumType), $"The type '{enumType.Name}' is not an enum and can't be used with IsEnumName.");
            }
        }

        public override CorrectValueOps Ops => CorrectValueOps.StringEnum;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var value = GetValueFrom(context);

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

        public override string ToString() => NAME;
    }
}