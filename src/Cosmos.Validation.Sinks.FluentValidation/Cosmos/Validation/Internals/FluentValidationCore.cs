using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Collections;
using Cosmos.Validation.Objects;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Cosmos.Validation.Internals
{
    internal static class FluentValidationCore
    {
        public static VerifyResult Verify(FluentValidation.IValidator validator, VerifiableObjectContext context, Type typeOfValidator)
        {
#if NET452
            var ret = validator.Validate(context.Instance);
#else
            var ctx = FluentValidationContextFactory.Resolve(context);
            var ret = validator.Validate(ctx);
#endif

            if (ret.IsValid)
                return VerifyResult.Success;

            var failures = ret.Errors.ConvertToVerifyFailures(typeOfValidator);
            return new VerifyResult(failures) { NameOfExecutedRules = ret.RuleSetsExecuted.Copy() };
        }

        public static VerifyResult Verify(FluentValidation.IValidator validator, Type declaringType, object instance, Type typeOfValidator)
        {
#if NET452
            var ret = validator.Validate(instance);
#else
            var ctx = FluentValidationContextFactory.Resolve(declaringType, instance);
            var ret = validator.Validate(ctx);
#endif

            if (ret.IsValid)
                return VerifyResult.Success;

            var failures = ret.Errors.ConvertToVerifyFailures(typeOfValidator);
            return new VerifyResult(failures) { NameOfExecutedRules = ret.RuleSetsExecuted.Copy() };
        }

        public static VerifyResult VerifyOne(FluentValidation.IValidator validator, VerifiableMemberContext context, Type typeOfValidator)
        {
            var propertyValidators = validator.CreateDescriptor().GetValidatorsForMember(context.MemberName);
            var parentContext = FluentValidationContextFactory.Resolve(context.DeclaringType, default);

            var originalFailures = new List<ValidationFailure>();
            var nameOfExecutedRules = new List<string>();

            foreach (var propertyValidator in propertyValidators)
            {
                foreach (var rule in (IEnumerable<IValidationRule>)validator)
                {
                    if (rule is PropertyRule propertyRule)
                    {
#if NET452
                        var propertyValidatorContext = new PropertyValidatorContext((ValidationContext)parentContext, propertyRule, context.MemberName, context.Value);
#else
                        var propertyValidatorContext = new PropertyValidatorContext(parentContext, propertyRule, context.MemberName, context.Value);
#endif
                        var localFailures = propertyValidator.Validate(propertyValidatorContext);

                        originalFailures.AddRange(localFailures);
                        nameOfExecutedRules.AddRange(propertyRule.RuleSets);
                    }
                }
            }

            if (!originalFailures.Any())
                return VerifyResult.Success;

            var failures = originalFailures.ConvertToVerifyFailures(typeOfValidator);
            return new VerifyResult(failures) { NameOfExecutedRules = nameOfExecutedRules.Distinct().ToArray() };
        }
    }
}