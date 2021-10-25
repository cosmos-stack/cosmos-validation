using System;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Number only
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NumberOnlyAttribute : RequireNumericTypeAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Number-Only Annotation";
    }
}