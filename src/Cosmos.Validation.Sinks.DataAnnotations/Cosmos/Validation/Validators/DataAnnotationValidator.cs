using System;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public class DataAnnotationValidator : CustomValidator
    {
        public DataAnnotationValidator() : base("DataAnnotationWrappedValidator") { }
        
        public static DataAnnotationValidator Instance { get; } = new();

        #region Verify

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