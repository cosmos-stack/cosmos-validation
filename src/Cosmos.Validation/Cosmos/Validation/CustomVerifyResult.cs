namespace Cosmos.Validation
{
    /// <summary>
    /// Custom verify result
    /// </summary>
    public class CustomVerifyResult
    {
        /// <summary>
        /// Verify result
        /// </summary>
        public bool VerifyResult { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Operation name
        /// </summary>
        public string OperationName { get; set; }
    }
}