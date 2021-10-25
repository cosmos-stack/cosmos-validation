using System;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Must negative or zero, alias of <see cref="NotPositiveAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NegativeOrZeroAttribute : NotPositiveAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Must-Negative-Or-Zero Annotation";
    }
}