using System.Collections.Generic;

namespace Cosmos.Validation
{
    /// <summary>
    /// The interface used to constrain the verification results.
    /// </summary>
    public interface IVerifyResult
    {
        /// <summary>
        /// Return the verification result.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Return all error messages.
        /// </summary>
        public IEnumerable<VerifyFailure> Errors { get; }

        /// <summary>
        /// Returns the names of all fields with errors.
        /// </summary>
        public IEnumerable<string> MemberNames { get; }
    }
}