namespace Cosmos.Validation.Internals.Extensions
{
    internal static class ValidationOptionsExtensions
    {
        public static VerifyResult ReturnUnexpectedTypeOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfProjectNotMatch ? VerifyResult.UnexpectedType : VerifyResult.Success;
        }

        public static VerifyResult ReturnNullReferenceOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfInstanceIsNull ? VerifyResult.NullReference : VerifyResult.Success;
        }

        public static VerifyResult ReturnNullReferenceOrSuccess(this ValidationOptions options, string paramName)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfInstanceIsNull ? VerifyResult.NullReferenceWith(paramName) : VerifyResult.Success;
        }

        public static VerifyResult ReturnUnregisterProjectForSuchTypeOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfProjectNotMatch ? VerifyResult.UnregisterProjectForSuchType : VerifyResult.Success;
        }

        public static VerifyResult ReturnUnregisterProjectForSuchNamedTypeOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfProjectNotMatch ? VerifyResult.UnregisterProjectForSuchNamedType : VerifyResult.Success;
        }
    }
}