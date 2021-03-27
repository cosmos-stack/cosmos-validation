using System;
using Cosmos.Reflection;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Must long
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class RequireLongTypeAttribute : VerifiableParamsAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Must-Int64 Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The type of the current value must be a long integer.";

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
                ? memberType.IsNot(TypeClass.LongClazz).OrNot(TypeClass.LongNullableClazz)
                : memberType.IsNot(TypeClass.LongClazz);

            return valid.Valid;
        }
    }
}