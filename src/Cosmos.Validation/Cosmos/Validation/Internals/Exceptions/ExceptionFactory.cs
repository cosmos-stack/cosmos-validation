namespace Cosmos.Validation.Internals.Exceptions
{
    internal static class ExceptionFactory
    {
        public static ValidationException Create(VerifyResult result)
        {
            var messageVal = result.ToStringVal(true);
            return new ValidationException(messageVal);
        }

        public static ValidationException Create(VerifyResult result, string message)
        {
            var messageVal = result.ToStringVal(true, message);
            return new ValidationException(messageVal);
        }

        public static ValidationException Create(VerifyResult result, long errorCode, string message)
        {
            var messageVal = result.ToStringVal(true, message);
            return new ValidationException(errorCode, messageVal);
        }

        public static ValidationException Create(VerifyResult result, string flag, string message)
        {
            var messageVal = result.ToStringVal(true, message);
            return new ValidationException(messageVal, flag);
        }

        public static ValidationException Create(VerifyResult result, long errorCode, string flag, string message)
        {
            var messageVal = result.ToStringVal(true, message);
            return new ValidationException(errorCode, messageVal, flag);
        }
    }
}