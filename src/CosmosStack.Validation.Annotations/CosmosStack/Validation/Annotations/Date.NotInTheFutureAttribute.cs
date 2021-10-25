using System;
using CosmosStack.Conversions.Determiners;
using CosmosStack.Date;
using CosmosStack.Reflection;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Not in future
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NotInTheFutureAttribute : VerifiableParamsAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Not-In-The-Future Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value cannot exist in the future.";
        
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
            else if (memberType.Is(TypeClass.DateTimeClazz).Valid)
                valid = memberValueGetter().Check<DateTime?>(v => DateGuard.ShouldBeInThePast(v, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.StringClazz).Valid)
                valid = memberValueGetter().Check<DateTime?>(v => DateGuard.ShouldBeInThePast(v, memberName, ErrorMessage), o => StringDateTimeDeterminer.To(o.ToString()));
            else
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : memberValueGetter().ToString().Check<DateTime?>(v => DateGuard.ShouldBeInThePast(v, memberName, ErrorMessage));

            return valid.Valid;
        }
    }
}