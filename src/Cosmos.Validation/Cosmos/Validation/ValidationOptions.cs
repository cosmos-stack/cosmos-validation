namespace Cosmos.Validation
{
    /// <summary>
    /// Validation options
    /// </summary>
    public class ValidationOptions
    {
        /// <summary>
        /// Mark whether the annotation validator is valid.
        /// </summary>
        public bool AnnotationEnabled { get; set; } = true;

        /// <summary>
        /// Mark whether the custom validator is valid.
        /// </summary>
        public bool CustomValidatorEnabled { get; set; } = true;

        /// <summary>
        /// When the verified instance is empty, whether to return a failure result.
        /// </summary>
        public bool FailureIfInstanceIsNull { get; set; } = true;

        /// <summary>
        /// When the type of rule does not exist, whether to return a failure result.
        /// </summary>
        public bool FailureIfProjectNotMatch { get; set; } = false;
    }
}