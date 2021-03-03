using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    internal sealed class SealedValidator : CustomValidator
    {
        public SealedValidator() : base("SealedValidator") { }

        protected override VerifyResult VerifyImpl(VerifiableObjectContext context)
        {
            return VerifyResult.Success;
        }

        protected override VerifyResult VerifyOneImpl(VerifiableMemberContext context)
        {
            return VerifyResult.Success;
        }

        public static SealedValidator Instance { get; } = new();
    }

    internal sealed class SealedValidator<T> : CustomValidator<T>
    {
        public SealedValidator() : base("SealedValidator`1") { }

        protected override VerifyResult VerifyImpl(VerifiableObjectContext context)
        {
            return VerifyResult.Success;
        }

        protected override VerifyResult VerifyOneImpl(VerifiableMemberContext context)
        {            
            return VerifyResult.Success;
        }

        public static SealedValidator<T> Instance { get; } = new();
    }
}