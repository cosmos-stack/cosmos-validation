using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CosmosStack.Reflection;
using CosmosStack.Validation.Internals.Extensions;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Internals
{
    internal static class DataAnnotationCore
    {
        public static VerifyResult Verify(VerifiableObjectContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return Verify(context.Instance);
        }

        public static VerifyResult Verify(object instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));

            var results = new List<ValidationResult>();
            var context = new ValidationContext(instance, null, null);

            if (Validator.TryValidateObject(instance, context, results, true))
                return VerifyResult.Success;

            var failures = new List<VerifyFailure>();

            foreach (var result in results)
            {
                if (result == ValidationResult.Success)
                    continue;

                var propertyName = result.MemberNames.First();

                var failure = VerifyFailureFactory.Create(propertyName, result.ErrorMessage);

                failures.Add(failure);
            }

            return new VerifyResult(failures);
        }

        public static VerifyResult VerifyOne(VerifiableMemberContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var annotations = context.Attributes
                                     .Where(a => a.GetType().IsDerivedFrom<ValidationAttribute>())
                                     .Select(a => a as ValidationAttribute);

            var errors = new List<VerifyError>();

            foreach (var annotation in annotations)
            {
                if (annotation is null)
                    continue;

                if (annotation.IsValid(context.Value))
                    continue;

                var error = new VerifyError
                {
                    ErrorMessage = annotation.ErrorMessage,
                    ViaValidatorType = ValidatorType.Custom,
                    ValidatorName = $"DataAnnotation {annotation.GetType().GetFriendlyName()} Validator"
                };

                errors.Add(error);
            }

            if (!errors.Any())
                return VerifyResult.Success;

            var failure = new VerifyFailure(context.MemberName, $"There are multiple errors in this Member '{context.MemberName}'.", context.Value);

            return new VerifyResult(failure);
        }
    }
}