using System;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Require
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class RequireAttribute : NotNullAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Require Annotation";
    }
}