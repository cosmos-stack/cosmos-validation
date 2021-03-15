using System;

namespace Cosmos.Validation
{
    /// <summary>
    /// Verify the wrong information.
    /// </summary>
    [Serializable]
    public class VerifyError
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The type of validator that generated this error message.
        /// </summary>
        public ValidatorType ViaValidatorType { get; set; } = ValidatorType.BuildIn;

        /// <summary>
        /// The name of the validator that generated the error message.
        /// </summary>
        public string ValidatorName { get; set; } = "BuildInValidator";
    }
}