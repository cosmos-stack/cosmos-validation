using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cosmos.Text;

namespace Cosmos.Validation
{
    /// <summary>
    /// It is used to keep the information of verification failure.
    /// </summary>
    [Serializable]
    public class VerifyFailure
    {
        private VerifyFailure() { }

        /// <summary>
        /// Create an instance of VerifyFailure.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessage"></param>
        /// <param name="verifiedValue"></param>
        public VerifyFailure(
            string propertyName,
            string errorMessage,
            object verifiedValue = null)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            VerifiedValue = verifiedValue;
        }

        /// <summary>
        /// The name of property
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The verified value
        /// </summary>
        public object VerifiedValue { get; set; }

        /// <summary>
        /// Error details
        /// </summary>
        public List<VerifyError> Details { get; set; } = new();

        /// <summary>
        /// Error code
        /// </summary>
        public long Code { get; set; }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(ErrorMessage);

            if (Details is not null && Details.Any())
            {
                builder.AppendLine();
                builder.AppendLine("Detail(s):");
                foreach (var error in Details)
                {
                    builder.AppendLine($"  - {error.ErrorMessage}");
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// To Structured StringVal
        /// </summary>
        /// <returns></returns>
        public StructuredStringVal ToStringVal()
        {
            var result = new StructuredStringVal(ErrorMessage);

            if (Details is not null && Details.Any())
            {
                foreach (var error in Details)
                {
                    result.Append(error.ErrorMessage);
                }
            }
            
            return result;
        }

        /// <summary>
        /// Create a VerifyFailure object based on the given parameters.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static VerifyFailure Create(string propertyName, string errorMessage)
        {
            return new(propertyName, errorMessage);
        }

        /// <summary>
        /// Create a VerifyFailure object based on the given parameters.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessage"></param>
        /// <param name="verifiedValue"></param>
        /// <returns></returns>
        public static VerifyFailure Create(string propertyName, string errorMessage, object verifiedValue)
        {
            return new(propertyName, errorMessage, verifiedValue);
        }
    }
}