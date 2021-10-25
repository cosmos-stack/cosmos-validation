using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Internals
{
    internal static class VerifyFailureFactory
    {
        public static VerifyFailure Create(string propertyName, string errorMessage)
        {
            var failure = new VerifyFailure(propertyName, errorMessage);

            var error = new VerifyError
            {
                ErrorMessage = errorMessage,
                ViaValidatorType = ValidatorType.Custom,
                ValidatorName = "System.ComponentModel.DataAnnotations.Validator"
            };
            
            failure.Details.Add(error);

            return failure;
        }   
    }
}