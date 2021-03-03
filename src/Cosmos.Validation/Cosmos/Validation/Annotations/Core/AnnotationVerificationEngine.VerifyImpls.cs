using System;
using System.Collections.Generic;
using Cosmos.Numeric;
using Cosmos.Reflection;
using Cosmos.Text;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Annotations.Core
{
    internal static partial class AnnotationVerificationEngine
    {
        private static class VerifyImpls
        {
            public static void MustNumericType(VerifiableMemberContext context, MustNumericTypeAttribute attr, List<VerifyError> errors)
            {
                var condition = attr.MayBeNullable
                    ? Types.IsNumericType(context.MemberType)
                    : Types.IsNumericType(context.MemberType) && !Types.IsNullableType(context.MemberType);
                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void MustIntType(VerifiableMemberContext context, MustIntTypeAttribute attr, List<VerifyError> errors)
            {
                var condition = attr.MayBeNullable
                    ? context.IsNot(TypeClass.IntClazz).OrNot(TypeClass.IntNullableClazz)
                    : context.IsNot(TypeClass.IntClazz);
                condition.Result.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void MustLongType(VerifiableMemberContext context, MustLongTypeAttribute attr, List<VerifyError> errors)
            {
                var condition = attr.MayBeNullable
                    ? context.IsNot(TypeClass.LongClazz).OrNot(TypeClass.LongNullableClazz)
                    : context.IsNot(TypeClass.LongClazz);
                condition.Result.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void MustStringType(VerifiableMemberContext context, MustStringTypeAttribute attr, List<VerifyError> errors)
            {
                var condition = context.Is(TypeClass.StringClazz);
                condition.Result.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void ValidDateValue(VerifiableMemberContext context, ValidDateValueAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.DateTimeClazz) && context.Value is DateTime)
                    condition = true;
                else if (context.Is(TypeClass.StringClazz) && context.Value is string str)
                    condition = str.IsDateTime();
                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotInTheFuture(VerifiableMemberContext context, NotInTheFutureAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.DateTimeClazz) && context.Value is DateTime d)
                    condition = d <= DateTime.UtcNow;
                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotInThePast(VerifiableMemberContext context, NotInThePastAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.DateTimeClazz) && context.Value is DateTime d)
                    condition = d >= DateTime.UtcNow;
                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotNegative(VerifiableMemberContext context, NotNegativeAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.IntClazz))
                    condition = context.GetValue<int>() >= 0;
                else if (context.Is(TypeClass.LongClazz))
                    condition = context.GetValue<long>() >= 0;
                else if (context.Is(TypeClass.FloatClazz))
                    condition = context.GetValue<float>() >= 0;
                else if (context.Is(TypeClass.DoubleClazz))
                    condition = context.GetValue<double>() >= 0;
                else if (context.Is(TypeClass.DecimalClazz))
                    condition = context.GetValue<decimal>() >= 0;
                else if (context.Is(TypeClass.TimeSpanClazz) && context.Value is TimeSpan tsVal)
                    condition = tsVal >= TimeSpan.Zero;
                else
                    condition = false;

                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotNegativeOrZero(VerifiableMemberContext context, NotNegativeOrZeroAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.IntClazz))
                    condition = context.GetValue<int>() > 0;
                else if (context.Is(TypeClass.LongClazz))
                    condition = context.GetValue<long>() > 0;
                else if (context.Is(TypeClass.FloatClazz))
                    condition = context.GetValue<float>() > 0;
                else if (context.Is(TypeClass.DoubleClazz))
                    condition = context.GetValue<double>() > 0;
                else if (context.Is(TypeClass.DecimalClazz))
                    condition = context.GetValue<decimal>() > 0;
                else if (context.Is(TypeClass.TimeSpanClazz) && context.Value is TimeSpan tsVal)
                    condition = tsVal > TimeSpan.Zero;
                else
                    condition = false;

                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotPositive(VerifiableMemberContext context, NotPositiveAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.IntClazz))
                    condition = context.GetValue<int>() <= 0;
                else if (context.Is(TypeClass.LongClazz))
                    condition = context.GetValue<long>() <= 0;
                else if (context.Is(TypeClass.FloatClazz))
                    condition = context.GetValue<float>() <= 0;
                else if (context.Is(TypeClass.DoubleClazz))
                    condition = context.GetValue<double>() <= 0;
                else if (context.Is(TypeClass.DecimalClazz))
                    condition = context.GetValue<decimal>() <= 0;
                else if (context.Is(TypeClass.TimeSpanClazz) && context.Value is TimeSpan tsVal)
                    condition = tsVal <= TimeSpan.Zero;
                else
                    condition = false;

                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotPositiveOrZero(VerifiableMemberContext context, NotPositiveOrZeroAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.IntClazz))
                    condition = context.GetValue<int>() < 0;
                else if (context.Is(TypeClass.LongClazz))
                    condition = context.GetValue<long>() < 0;
                else if (context.Is(TypeClass.FloatClazz))
                    condition = context.GetValue<float>() < 0;
                else if (context.Is(TypeClass.DoubleClazz))
                    condition = context.GetValue<double>() < 0;
                else if (context.Is(TypeClass.DecimalClazz))
                    condition = context.GetValue<decimal>() < 0;
                else if (context.Is(TypeClass.TimeSpanClazz) && context.Value is TimeSpan tsVal)
                    condition = tsVal < TimeSpan.Zero;
                else
                    condition = false;

                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotNull(VerifiableMemberContext context, NotNullAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.StringClazz) && context.Value is string s)
                    condition = !string.IsNullOrEmpty(s);
                else
                    condition = context.Value is not null;
                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotWhiteSpace(VerifiableMemberContext context, NotWhiteSpaceAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.StringClazz) && context.Value is string s)
                    condition = !string.IsNullOrWhiteSpace(s);
                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotOutOfLength(VerifiableMemberContext context, NotOutOfLengthAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.StringClazz) && context.Value is string s)
                    condition = s.Trim().Length <= attr.Length;
                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void NotOutOfRange(VerifiableMemberContext context, NotOutOfRangeAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.IntClazz) && context.Value is int intVal)
                {
                    int intMin = attr.Min.AsOr(NumericConstants.INT_MIN);
                    int intMax = attr.Max.AsOr(NumericConstants.INT_MAX);
                    condition = NumericJudge.IsBetween(intVal, intMin, intMax);
                }
                else if (context.Is(TypeClass.LongClazz) && context.Value is long longVal)
                {
                    long longMin = attr.Min.AsOr(NumericConstants.LONG_MIN);
                    long longMax = attr.Max.AsOr(NumericConstants.LONG_MAX);
                    condition = NumericJudge.IsBetween(longVal, longMin, longMax);
                }
                else if (context.Is(TypeClass.FloatClazz) && context.Value is float floatVal)
                {
                    float floatMin = attr.Min.AsOr(NumericConstants.FLOAT_MIN);
                    float floatMax = attr.Max.AsOr(NumericConstants.FLOAT_MAX);
                    condition = NumericJudge.IsBetween(floatVal, floatMin, floatMax);
                }
                else if (context.Is(TypeClass.DoubleClazz) && context.Value is double doubleVal)
                {
                    double doubleMin = attr.Min.AsOr(NumericConstants.DOUBLE_MIN);
                    double doubleMax = attr.Max.AsOr(NumericConstants.DOUBLE_MAX);
                    condition = NumericJudge.IsBetween(doubleVal, doubleMin, doubleMax);
                }
                else if (context.Is(TypeClass.DecimalClazz) && context.Value is decimal decimalVal)
                {
                    decimal decimalMin = attr.Min.AsOr(NumericConstants.DECIMAL_MIN);
                    decimal decimalMax = attr.Max.AsOr(NumericConstants.DECIMAL_MAX);
                    condition = NumericJudge.IsBetween(decimalVal, decimalMin, decimalMax);
                }

                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }

            public static void ValidEmailValue(VerifiableMemberContext context, ValidEmailValueAttribute attr, List<VerifyError> errors)
            {
                var condition = false;
                if (context.Is(TypeClass.StringClazz) && context.Value is string emailStr)
                {
                    var result = EmailValidator.Instance.Verify(
                        emailStr,
                        context.MemberName,
                        attr.AllowTopLevelDomains,
                        attr.AllowInternational);

                    condition = result.IsValid;
                }
                else
                {
                    var result = attr.StrongVerify(context.ConvertToObjectContext());

                    condition = result.IsValid;
                }

                condition.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }
        }
    }
}