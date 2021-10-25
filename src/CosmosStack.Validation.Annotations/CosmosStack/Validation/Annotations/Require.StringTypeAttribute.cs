using System;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Must string type
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class RequireStringTypeAttribute : VerifiableParamsAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Must-String Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The type of the current value must be a string.";
        
        /// <summary>
        /// Invoke internal impl
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="memberName"></param>
        /// <param name="memberValueGetter"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(Type memberType, string memberName, Func<object> memberValueGetter)
        {
            var valid = memberType.Is<string>();

            return valid.Valid;
        }
    }
}