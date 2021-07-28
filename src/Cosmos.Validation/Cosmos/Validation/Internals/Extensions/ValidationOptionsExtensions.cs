using System.Runtime.CompilerServices;

namespace Cosmos.Validation.Internals.Extensions
{
    internal static class ValidationOptionsExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerifyResult ReturnUnexpectedTypeOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfProjectNotMatch ? VerifyResult.UnexpectedType : VerifyResult.Success;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerifyResult ReturnNullReferenceOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfInstanceIsNull ? VerifyResult.NullReference : VerifyResult.Success;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerifyResult ReturnNullReferenceOrSuccess(this ValidationOptions options, string paramName)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfInstanceIsNull ? VerifyResult.NullReferenceWith(paramName) : VerifyResult.Success;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerifyResult ReturnUnregisterProjectForSuchTypeOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfProjectNotMatch ? VerifyResult.UnregisterProjectForSuchType : VerifyResult.Success;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerifyResult ReturnUnregisterProjectForSuchNamedTypeOrSuccess(this ValidationOptions options)
        {
            if (options is null) return VerifyResult.Success;
            return options.FailureIfProjectNotMatch ? VerifyResult.UnregisterProjectForSuchNamedType : VerifyResult.Success;
        }
    }
}