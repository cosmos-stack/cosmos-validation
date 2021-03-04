using System;
using Cosmos.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public sealed class FluentValidator : CustomValidator
    {
        private readonly FluentValidation.IValidator _validatorImpl;
        private readonly Type _typeOfValidator;

        public FluentValidator(Type typeOfValidator) : base("FluentValidationWrappedValidator")
        {
            if (typeOfValidator is null) throw new ArgumentNullException(nameof(typeOfValidator));
            if (!typeOfValidator.IsDerivedFrom<FluentValidation.IValidator>())
                throw new ArgumentException("This type must derived from 'FluentValidation.IValidator'.", nameof(typeOfValidator));
            _validatorImpl = TypeVisit.CreateInstance<FluentValidation.IValidator>(typeOfValidator);
            _typeOfValidator = typeOfValidator;
        }

        public FluentValidator(FluentValidation.IValidator validator) : base("FluentValidationWrappedValidator")
        {
            _validatorImpl = validator ?? throw new ArgumentNullException(nameof(validator));
            _typeOfValidator = validator.GetType();
        }

        #region Verify

        public override VerifyResult Verify(Type declaringType, object instance)
        {
            return FluentValidationCore.Verify(_validatorImpl, declaringType, instance, _typeOfValidator);
        }

        protected override VerifyResult VerifyImpl(VerifiableObjectContext context)
        {
            return FluentValidationCore.Verify(_validatorImpl, context, _typeOfValidator);
        }

        #endregion

        #region VerifyOne

        protected override VerifyResult VerifyOneImpl(VerifiableMemberContext context)
        {
            return FluentValidationCore.VerifyOne(_validatorImpl, context, _typeOfValidator);
        }

        #endregion

        #region By

        public static FluentValidator By(Type typeOfValidator)
        {
            return new FluentValidator(typeOfValidator);
        }

        public static FluentValidator By(FluentValidation.IValidator validator)
        {
            return new FluentValidator(validator);
        }

        public static FluentValidator<TValidator> By<TValidator>()
            where TValidator : class, FluentValidation.IValidator, new()
        {
            return new FluentValidator<TValidator>();
        }

        public static FluentValidator<TValidator, T> By<TValidator, T>()
            where TValidator : class, FluentValidation.IValidator<T>, new()
        {
            return new FluentValidator<TValidator, T>();
        }

        #endregion
    }
}