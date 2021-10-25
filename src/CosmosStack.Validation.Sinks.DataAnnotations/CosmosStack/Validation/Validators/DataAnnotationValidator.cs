using System;
using CosmosStack.Validation.Internals;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// DataAnnotation Validator <br />
    /// 数据注解验证器
    /// </summary>
    public class DataAnnotationValidator : CustomValidator
    {
        public DataAnnotationValidator() : base("DataAnnotationWrappedValidator") { }
        
        public static DataAnnotationValidator Instance { get; } = new();

        #region Verify

        /// <inheritdoc />
        public override VerifyResult Verify(Type declaringType, object instance)
        {
            return DataAnnotationCore.Verify(instance);
        }

        protected override VerifyResult VerifyImpl(VerifiableObjectContext context)
        {
            return DataAnnotationCore.Verify(context);
        }

        #endregion

        #region VerifyOne

        protected override VerifyResult VerifyOneImpl(VerifiableMemberContext context)
        {
            return DataAnnotationCore.VerifyOne(context);
        }

        #endregion
    }
}