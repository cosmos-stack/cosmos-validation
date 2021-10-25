using System;
using CosmosStack.Conversions.Determiners;
using CosmosStack.Numeric;
using CosmosStack.Reflection;
using CosmosStack.Text;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Between, be in range
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class BetweenAttribute : VerifiableParamsAttribute
    {
        public BetweenAttribute(char min, char max)
        {
            CMin = min;
            CMax = max;
        }

        public BetweenAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public BetweenAttribute(long min, long max)
        {
            Min = min;
            Max = max;
        }

        public BetweenAttribute(float min, float max)
        {
            DMin = min;
            DMax = max;
        }

        public BetweenAttribute(double min, double max)
        {
            DMin = min;
            DMax = max;
        }

        public BetweenAttribute(decimal min, decimal max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Between Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value exceeds the upper and lower bounds.";

        /// <summary>
        /// Min
        /// </summary>
        private decimal Min { get; set; }

        /// <summary>
        /// Max
        /// </summary>
        private decimal Max { get; set; }

        private double DMin { get; set; }

        private double DMax { get; set; }

        private double CMin { get; set; }

        private double CMax { get; set; }

        /// <summary>
        /// Invoke internal impl
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="memberName"></param>
        /// <param name="memberValueGetter"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(Type memberType, string memberName, Func<object> memberValueGetter)
        {
            (bool Valid, Type ParameterType, string Message) valid;

            if (memberValueGetter() is null && !IgnoreNullObject)
                valid = Failure(memberType, ErrorMessage);
            else if (memberType.IsNumeric())
                valid = NumericValid(memberType, memberName, memberValueGetter);
            else if (memberType.Is(TypeClass.CharClazz).Valid)
                valid = memberValueGetter().Check<char?>(v => v.RequireWithinRange(CharMin, CharMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.StringClazz).Valid && memberValueGetter() is string strNum)
                valid = NumericValid(memberType, memberName, strNum);
            else
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : memberValueGetter().ToString().Check<decimal?>(v => v.RequireWithinRange(DecimalMin, DecimalMax, memberName, ErrorMessage));

            return valid.Valid;
        }

        private (bool Valid, Type ParameterType, string Message) NumericValid(Type memberType, string memberName, Func<object> memberValueGetter)
        {
            (bool Valid, Type ParameterType, string Message) valid;

            if (memberType.Is(TypeClass.IntClazz).Valid)
                valid = memberValueGetter().Check<int?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.LongClazz).Valid)
                valid = memberValueGetter().Check<long?>(v => v.RequireWithinRange(LongMin, LongMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.FloatClazz).Valid)
                valid = memberValueGetter().Check<float?>(v => v.RequireWithinRange(FloatMin, FloatMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DoubleClazz).Valid)
                valid = memberValueGetter().Check<double?>(v => v.RequireWithinRange(DoubleMin, DoubleMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DecimalClazz).Valid)
                valid = memberValueGetter().Check<decimal?>(v => v.RequireWithinRange(DecimalMin, DecimalMax, memberName, ErrorMessage));
            else
                valid = Failure(memberType, ErrorMessage);

            return valid;
        }

        private (bool Valid, Type ParameterType, string Message) NumericValid(Type memberType, string memberName, string stringNum)
        {
            (bool Valid, Type ParameterType, string Message) valid;

            if (StringDecimalDeterminer.Is(stringNum))
            {
                valid = StringDecimalDeterminer
                        .To(stringNum)
                        .Check(v => v.RequireWithinRange(DecimalMin, DecimalMax, memberName, ErrorMessage), o => o);
            }
            else if (StringDoubleDeterminer.Is(stringNum))
            {
                valid = StringDoubleDeterminer
                        .To(stringNum)
                        .Check(v => v.RequireWithinRange(DoubleMin, DoubleMax, memberName, ErrorMessage), o => o);
            }
            else
                valid = Failure(memberType, ErrorMessage);

            return valid;
        }

        private long LongMin => Min.TryTo(NumericConstants.LONG_MIN);

        private long LongMax => Max.TryTo(NumericConstants.LONG_MAX);

        private int IntMin => Min.TryTo(NumericConstants.INT_MIN);

        private int IntMax => Max.TryTo(NumericConstants.INT_MAX);

        private float FloatMin => DMin.TryTo(NumericConstants.FLOAT_MIN);

        private float FloatMax => DMax.TryTo(NumericConstants.FLOAT_MAX);

        private double DoubleMin => DMin.TryTo(NumericConstants.DOUBLE_MIN);

        private double DoubleMax => DMax.TryTo(NumericConstants.DOUBLE_MAX);

        private decimal DecimalMin => Min.TryTo(NumericConstants.DECIMAL_MIN);

        private decimal DecimalMax => Max.TryTo(NumericConstants.DECIMAL_MAX);

        private char CharMin => CMin.TryTo(char.MinValue);

        private char CharMax => CMax.TryTo(char.MaxValue);
    }
}