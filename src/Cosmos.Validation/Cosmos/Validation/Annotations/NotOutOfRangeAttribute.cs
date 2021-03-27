using System;
using Cosmos.Numeric;
using Cosmos.Reflection;
using Cosmos.Text;
using Cosmos.Validation.Annotations.Core;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Not out of range
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NotOutOfRangeAttribute : ValidationParameterAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Not-Out-Of-Range Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value exceeds the upper and lower bounds.";

        /// <summary>
        /// Min
        /// </summary>
        public decimal Min { get; set; }

        /// <summary>
        /// Max
        /// </summary>
        public decimal Max { get; set; }

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
            else if (memberType.Is(TypeClass.IntClazz).Valid)
                valid = memberValueGetter().Check<int?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.LongClazz).Valid)
                valid = memberValueGetter().Check<long?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.FloatClazz).Valid)
                valid = memberValueGetter().Check<float?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DoubleClazz).Valid)
                valid = memberValueGetter().Check<double?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DecimalClazz).Valid)
                valid = memberValueGetter().Check<decimal?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.StringClazz).Valid)
            {
                if (memberValueGetter()._TryTo<string>().IsNumeric())
                    valid = memberValueGetter().Check<decimal?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
                else
                    valid = IgnoreUnexpectedType ? Success(memberType) : Failure(memberType, ErrorMessage);
            }
            else
            {
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : memberValueGetter().ToString().Check<decimal?>(v => v.RequireWithinRange(IntMin, IntMax, memberName, ErrorMessage));
            }

            return valid.Valid;
        }


        private long LongMin => Min.TryTo(NumericConstants.LONG_MIN);

        private long LongMax => Max.TryTo(NumericConstants.LONG_MAX);

        private int IntMin => Min.TryTo(NumericConstants.INT_MIN);

        private int IntMax => Max.TryTo(NumericConstants.INT_MAX);

        private float FloatMin => Min.TryTo(NumericConstants.FLOAT_MIN);

        private float FloatMax => Max.TryTo(NumericConstants.FLOAT_MAX);

        private double DoubleMin => Min.TryTo(NumericConstants.DOUBLE_MIN);

        private double DoubleMax => Max.TryTo(NumericConstants.DOUBLE_MAX);

        private decimal DecimalMin => Min.TryTo(NumericConstants.DECIMAL_MIN);

        private decimal DecimalMax => Max.TryTo(NumericConstants.DECIMAL_MAX);
    }
}