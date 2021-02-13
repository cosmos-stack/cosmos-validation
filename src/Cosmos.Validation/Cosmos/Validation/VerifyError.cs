using System;

namespace Cosmos.Validation
{
    [Serializable]
    public class VerifyError
    {
        public string ErrorMessage { get; set; }

        public ValidatorType ViaValidatorType { get; set; } = ValidatorType.BuildIn;

        public string ValidatorName { get; set; } = "BuildInValidator";
    }
}