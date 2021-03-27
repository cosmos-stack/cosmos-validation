using System;
using Cosmos.Reflection;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Music numeric type
    /// </summary>
    public class RequiredNumericTypeAttribute : ValidationParameterAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Must-Numeric-Type Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The type of the current value must be a numeric type.";

        /// <summary>
        /// My be nullable
        /// </summary>
        public bool MayBeNullable { get; set; }

        /// <summary>
        /// Invoke internal impl
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="memberName"></param>
        /// <param name="memberValueGetter"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(Type memberType, string memberName, Func<object> memberValueGetter)
        {
            var valid = MayBeNullable
                ? Types.IsNumericType(memberType)
                : Types.IsNumericType(memberType) && !Types.IsNullableType(memberType);

            return valid;
        }
    }
}