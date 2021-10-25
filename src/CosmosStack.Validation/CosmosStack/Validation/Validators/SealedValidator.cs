﻿using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// A validator that only returns success information, and cannot be inherited. <br />
    /// 静态验证器，只返回成功，不返回失败
    /// </summary>
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

    /// <summary>
    /// A validator that only returns success information, and cannot be inherited. <br />
    /// 静态验证器，只返回成功，不返回失败
    /// </summary>
    /// <typeparam name="T"></typeparam>
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