using System;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation
{
    /// <summary>
    /// Verify the wrong information. <br />
    /// 错误信息
    /// </summary>
    [Serializable]
    public class VerifyError
    {
        /// <summary>
        /// Error message <br />
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The type of validator that generated this error message. <br />
        /// 验证其类型，默认为内建验证器。
        /// </summary>
        public ValidatorType ViaValidatorType { get; set; } = ValidatorType.BuildIn;

        /// <summary>
        /// The name of the validator that generated the error message. <br />
        /// 验证其名称，默认为 BuildInValidator
        /// </summary>
        public string ValidatorName { get; set; } = "BuildInValidator";
    }
}