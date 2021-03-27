using System;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Must negative, alias of <see cref="NotPositiveOrZeroAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class NegativeAttribute : NotPositiveOrZeroAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Must-Negative Annotation";
    }
}