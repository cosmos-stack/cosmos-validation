using System;
using System.Reflection;
using Cosmos.Exceptions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueEnumToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueEnumToken";
        private readonly Type _enumType;

        public ValueEnumToken(VerifiableMemberContract contract, Type enumType) : base(contract)
        {
            _enumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
        }

        public override CorrectValueOps Ops => CorrectValueOps.Enum;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var message))
                UpdateVal(verifyVal, value, message);

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value, out var message))
                UpdateVal(verifyVal, value, message);

            return verifyVal;
        }

        private bool IsValidImpl(object value, out string message)
        {
            message = null;

            if (value is null)
                return true;

            var enumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;

            if (!enumType.IsEnum)
                return false;

            if (enumType.GetCustomAttribute<FlagsAttribute>() is not null)
                return IsFlagsEnumDefined(enumType, value, out message);

            return Try.Create(() => Enum.IsDefined(enumType, value)).IsSuccess;
        }

        private static bool IsFlagsEnumDefined(Type enumType, object value, out string message)
        {
            message = null;
            var nameOfType = Enum.GetUnderlyingType(enumType).Name;

            switch (nameOfType)
            {
                case "Byte":
                    return EvaluateFlagEnumValues(value.As<byte>(), enumType);

                case "Int16":
                    return EvaluateFlagEnumValues(value.As<short>(), enumType);

                case "Int32":
                    return EvaluateFlagEnumValues(value.As<int>(), enumType);

                case "Int64":
                    return EvaluateFlagEnumValues(value.As<long>(), enumType);

                case "SByte":
                    return EvaluateFlagEnumValues(Convert.ToInt64(value.As<sbyte>()), enumType);

                case "UInt16":
                    return EvaluateFlagEnumValues(value.As<ushort>(), enumType);

                case "UInt32":
                    return EvaluateFlagEnumValues(value.As<uint>(), enumType);

                case "UInt64":
                    return EvaluateFlagEnumValues((long) value.As<ulong>(), enumType);

                default:
                    message = $"Unexpected typeName of '{nameOfType}' during flags enum evaluation.";
                    return false;
            }

            bool EvaluateFlagEnumValues(long localValue, Type localEnumType)
            {
                long mask = 0;
                foreach (var enumValue in Enum.GetValues(localEnumType))
                {
                    var enumValueAsInt64 = Convert.ToInt64(enumValue);
                    if ((enumValueAsInt64 & localValue) == enumValueAsInt64)
                    {
                        mask |= enumValueAsInt64;
                        if (mask == localValue)
                            return true;
                    }
                }

                return false;
            }
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message ?? $"'{VerifiableMember.MemberName}' has a range of values which does not include '{obj}'.");
        }

        public override string ToString() => NAME;
    }
}