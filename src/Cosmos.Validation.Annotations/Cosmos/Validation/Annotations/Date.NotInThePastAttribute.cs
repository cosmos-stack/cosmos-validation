using System;
using Cosmos.Conversions.Determiners;
using Cosmos.Date;
using Cosmos.Reflection;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// /Not in past
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NotInThePastAttribute : VerifiableParamsAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Not-In-The-Past Annotation";
        
        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value cannot exist in the past.";
        
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
                valid = memberValueGetter().Check<DateTime?>(v => DateGuard.ShouldBeInTheFuture(v, memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.StringClazz).Valid)
                valid = memberValueGetter().Check<DateTime?>(v => DateGuard.ShouldBeInTheFuture(v, memberName, ErrorMessage), o => StringDateTimeDeterminer.To(o.ToString()));
            else
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : memberValueGetter().ToString().Check<DateTime?>(v => DateGuard.ShouldBeInTheFuture(v, memberName, ErrorMessage));

            return valid.Valid;
        }
    }
}