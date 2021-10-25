﻿using System;
using CosmosStack.Reflection;

namespace CosmosStack.Validation.Internals
{
    internal static class ValueTypeEqualCalculator
    {
        public static bool Valid(Type typeOfValue, object value, Type typeOfValueToCompare, object valueToCompare)
        {
            if (TypeJudge.IsNumericType(typeOfValue) && TypeJudge.IsNumericType(typeOfValueToCompare))
            {
                var valueNumeric = Convert.ToDecimal(value);
                var valueNumericToCompare = Convert.ToDecimal(valueToCompare);
                return valueNumeric == valueNumericToCompare;
            }

            if (value is char valueChar && valueToCompare is char valueCharToCompare)
            {
                return valueChar == valueCharToCompare;
            }

            if (TypeJudge.IsEnumType(typeOfValue) && TypeJudge.IsEnumType(typeOfValueToCompare))
            {
                var valueEnumName = Enum.GetName(typeOfValue, value);
                var valueEnumNameToCompare = Enum.GetName(typeOfValueToCompare, valueToCompare);
                return valueEnumName == valueEnumNameToCompare;
            }

            long valueTimeStamp = ToLongTimeTicks(value);

            long valueTimeStampToCompare = ToLongTimeTicks(valueToCompare, -1L);

            return valueTimeStamp == valueTimeStampToCompare;
        }

        public static int CompareTo(Type typeOfValue, object value, Type typeOfValueToCompare, object valueToCompare)
        {
            if (TypeJudge.IsNumericType(typeOfValue) && TypeJudge.IsNumericType(typeOfValueToCompare))
            {
                var valueNumeric = Convert.ToDecimal(value);
                var valueNumericToCompare = Convert.ToDecimal(valueToCompare);
                return valueNumeric.CompareTo(valueNumericToCompare);
            }

            if (value is char valueChar && valueToCompare is char valueCharToCompare)
            {
                return valueChar.CompareTo(valueCharToCompare);
            }

            if (TypeJudge.IsEnumType(typeOfValue) && TypeJudge.IsEnumType(typeOfValueToCompare))
            {
                var valueEnumName = Enum.GetName(typeOfValue, value);
                var valueEnumNameToCompare = Enum.GetName(typeOfValueToCompare, valueToCompare);
                return string.Compare(valueEnumName, valueEnumNameToCompare, StringComparison.Ordinal);
            }

            long valueTimeStamp = ToLongTimeTicks(value);

            long valueTimeStampToCompare = ToLongTimeTicks(valueToCompare, -1L);

            return valueTimeStamp.CompareTo(valueTimeStampToCompare);
        }

        public static long ToLongTimeTicks(object value, long defaultVal = 0L)
        {
            return value switch
            {
                DateTime dt => dt.Ticks,
                DateTimeOffset dto => dto.Ticks,
                TimeSpan ts => ts.Ticks,
                _ => defaultVal
            };
        }
    }
}