using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Annotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public abstract class CustomAnnotationAttribute : Attribute, IFlagAnnotation
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public string ErrorMessage { get; set; }

        protected abstract bool IsValid(ObjectValueContext context);

        internal bool IsValidInternal(ObjectValueContext context) => IsValid(context);
    }
}