namespace Cosmos.Validation.Internals
{
    internal class CorrectVerifyVal
    {
        public bool IsSuccess { get; set; } = true;

        public bool IsIgnore { get; set; } = false;

        public string NameOfExecutedRule { get; set; }

        public object VerifiedValue { get; set; }

        public string ErrorMessage { get; set; }

        public static CorrectVerifyVal Success => new() {IsSuccess = true};

        public static CorrectVerifyVal Ignore => new() {IsIgnore = true, IsSuccess = false};
    }
}