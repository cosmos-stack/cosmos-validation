namespace Cosmos.Validation.Internals
{
    internal class CorrectVerifyVal
    {
        public bool IsSuccess { get; set; } = true;

        public string NameOfExecutedRule { get; set; }

        public object VerifiedValue { get; set; }

        public string ErrorMessage { get; set; }

        public static CorrectVerifyVal Success => new() {IsSuccess = true};
    }
}