using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cosmos.Collections;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Cosmos.Validation.Internals
{
    internal static class FluentValidationCore
    {
#if !NETFRAMEWORK

        static FluentValidationCore()
        {
            NatashaInitializer.InitializeAndPreheating();
        }

#endif

        public static VerifyResult Verify(IValidator validator, VerifiableObjectContext context, Type typeOfValidator)
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

        public static VerifyResult Verify(IValidator validator, Type declaringType, object instance, Type typeOfValidator)
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

#if NET452
        public static VerifyResult VerifyOne(IValidator validator, VerifiableMemberContext context, Type typeOfValidator)
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
                        var propertyValidatorContext = new PropertyValidatorContext((ValidationContext)parentContext, propertyRule, context.MemberName, context.Value);

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
#elif NETFRAMEWORK
        public static VerifyResult VerifyOne(IValidator validator, VerifiableMemberContext context, Type typeOfValidator)
        {
            var propertyValidators = validator.CreateDescriptor().GetValidatorsForMember(context.MemberName);
            var parentContext = FluentValidationContextFactory.Resolve(context.DeclaringType, default);
            
            var originalFailures = new List<ValidationFailure>();
            var nameOfExecutedRules = new List<string>();
            
            foreach (var propertyValidator in propertyValidators)
            {
                foreach (var rule in (IEnumerable<IValidationRule>) validator)
                {
                    if (rule is PropertyRule propertyRule)
                    {
                        var propertyValidatorContext = new PropertyValidatorContext(parentContext, propertyRule, context.MemberName, context.Value);
            
                        var localFailures = propertyValidator.Validate(propertyValidatorContext);
            
                        originalFailures.AddRange(localFailures);
                        nameOfExecutedRules.AddRange(propertyRule.RuleSets);
                    }
                }
            }
            
            if (!originalFailures.Any())
                return VerifyResult.Success;
            
            var failures = originalFailures.ConvertToVerifyFailures(typeOfValidator);
            return new VerifyResult(failures) {NameOfExecutedRules = nameOfExecutedRules.Distinct().ToArray()};
            
            // Validation/Sink-for-FluentValidation will no-longer support VerifyOne any more.
            // return VerifyResult.Success;
        }
#else
        public static VerifyResult VerifyOne(IValidator validator, VerifiableMemberContext context, Type typeOfValidator)
        {
            if (!validator.CanValidateInstancesOfType(context.DeclaringType))
                return VerifyResult.Success;

            var friendlyNameOfValidationResult = typeof(ValidationResult).GetFriendlyName();
            var friendlyNameOfDeclaringType = context.DeclaringType.GetFriendlyName();

            var script = @$"
if(arg1 is not FluentValidation.IValidator<{friendlyNameOfDeclaringType}> realValidator)
    return new {friendlyNameOfValidationResult}();

var @try = Cosmos.Exceptions.Try.Create(() => TypeVisit.CreateInstance<{friendlyNameOfDeclaringType}>());
if(@try.IsFailure)
    return new {friendlyNameOfValidationResult}();

var instance = @try.Value;
instance.{context.MemberName} = ({context.MemberType.GetFriendlyName()})arg2.Value;

return FluentValidation.DefaultValidatorExtensions.Validate<{friendlyNameOfDeclaringType}>(
    realValidator,
    instance,
    options => options.IncludeProperties(arg2.MemberName)
);";

            var v = NDelegate.RandomDomain()
                             .Func<IValidator, VerifiableMemberContext, ValidationResult>(script)
                             .Invoke(validator, context);
            
            if (v.IsValid)
                return VerifyResult.Success;

            var failures = v.Errors.ConvertToVerifyFailures(typeOfValidator);

            return new VerifyResult(failures) { NameOfExecutedRules = v.RuleSetsExecuted };
        }
#endif
    }
}