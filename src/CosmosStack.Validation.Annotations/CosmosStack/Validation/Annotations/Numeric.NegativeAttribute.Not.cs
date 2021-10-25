using System;
using CosmosStack.Date;
using CosmosStack.Numeric;
using CosmosStack.Reflection;
using CosmosStack.Text;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Not negative
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NotNegativeAttribute : VerifiableParamsAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Not-Negative Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value cannot be negative.";

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
            else  if (memberType.Is(TypeClass.IntClazz).Valid)
                valid = memberValueGetter().Check<int?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.LongClazz).Valid)
                valid = memberValueGetter().Check<long?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.FloatClazz).Valid)
                valid = memberValueGetter().Check<float?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DoubleClazz).Valid)
                valid = memberValueGetter().Check<double?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.DecimalClazz).Valid)
                valid = memberValueGetter().Check<decimal?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.TimeSpanClazz).Valid)
                valid = memberValueGetter().Check<TimeSpan?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.StringClazz).Valid)
            {
                if (memberValueGetter()._TryTo<string>().IsNumeric())
                    valid = memberValueGetter().Check<decimal?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
                else if (memberValueGetter()._TryTo<string>().IsTimeSpan())
                    valid = memberValueGetter().Check<TimeSpan?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
                else
                    valid = IgnoreUnexpectedType ? Success(memberType) : Failure(memberType, ErrorMessage);
            }
            else
            {
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : memberValueGetter().ToString().Check<decimal?>(v => v.RequirePositiveOrZero(memberName, ErrorMessage));
            }
            
            return valid.Valid;
        }
    }
}