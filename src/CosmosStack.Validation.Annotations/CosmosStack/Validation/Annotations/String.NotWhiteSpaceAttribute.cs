using System;
using CosmosStack.Reflection;
using CosmosStack.Text;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Not whitespace
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NotWhiteSpaceAttribute : VerifiableParamsAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Not-WhiteSpace Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value cannot be empty.";

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
            else if (memberType.Is(TypeClass.StringClazz).Valid)
                valid = memberValueGetter().Check<string>(v => v.CheckBlank(memberName, ErrorMessage));
            else
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : memberValueGetter().ToString().Check<string>(v => v.CheckBlank(memberName, ErrorMessage));

            return valid.Valid;
        }
    }
}