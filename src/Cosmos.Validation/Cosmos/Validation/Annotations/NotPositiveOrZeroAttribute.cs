using System;
using Cosmos.Date;
using Cosmos.Numeric;
using Cosmos.Reflection;
using Cosmos.Text;
using Cosmos.Validation.Annotations.Core;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Not positive or zero
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NotPositiveOrZeroAttribute : ValidationParameterAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Not-Positive-Or-Zero Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value cannot be positive or zero.";

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
                valid = memberValueGetter().Check<int?>(v => v.RequireNegative(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.LongClazz).Valid)
                valid = memberValueGetter().Check<long?>(v => v.RequireNegative(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.FloatClazz).Valid)
                valid = memberValueGetter().Check<float?>(v => v.RequireNegative(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DoubleClazz).Valid)
                valid = memberValueGetter().Check<double?>(v => v.RequireNegative(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DecimalClazz).Valid)
                valid = memberValueGetter().Check<decimal?>(v => v.RequireNegative(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.TimeSpanClazz).Valid)
                valid = memberValueGetter().Check<TimeSpan?>(v => v.RequireNegative(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.StringClazz).Valid)
            {
                if (memberValueGetter()._TryTo<string>().IsNumeric())
                    valid = memberValueGetter().Check<decimal?>(v => v.RequireNegative(memberName, ErrorMessage));
                else if (memberValueGetter()._TryTo<string>().IsTimeSpan())
                    valid = memberValueGetter().Check<TimeSpan?>(v => v.RequireNegative(memberName, ErrorMessage));
                else
                    valid = IgnoreUnexpectedType ? Success(memberType) : Failure(memberType, ErrorMessage);
            }
            else
            {
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : memberValueGetter().ToString().Check<decimal?>(v => v.RequireNegative(memberName, ErrorMessage));
            }

            return valid.Valid;
        }
    }
}