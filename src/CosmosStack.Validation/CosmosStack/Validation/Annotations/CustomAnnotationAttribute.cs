using System;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Custom annotation attribute <br />
    /// 自定义注解特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public abstract class CustomAnnotationAttribute : Attribute, IFlagAnnotation
    {
        /// <summary>
        /// Name of this Attribute/Annotation <br />
        /// 注解名
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public string ErrorMessage { get; set; }

        protected abstract bool IsValid(VerifiableMemberContext context);

        internal bool IsValidInternal(VerifiableMemberContext context) => IsValid(context);
    }
}