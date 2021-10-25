﻿using System;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Must positive, alias of <see cref="NotNegativeOrZeroAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class PositiveAttribute : NotNegativeOrZeroAttribute
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Must-Positive Annotation";
    }
}