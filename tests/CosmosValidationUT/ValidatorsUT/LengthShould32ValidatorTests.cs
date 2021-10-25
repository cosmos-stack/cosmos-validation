﻿using CosmosStack.Validation;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Registrars;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "CustomValidator<T>")]
    public class LengthShould32ValidatorTests
    {
        public LengthShould32ValidatorTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact(DisplayName = "Verify an instance for CustomValidator and return a success VerifyResult.")]
        public void VerifyCustomValidatorAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true; //important

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyCustomValidatorAndShouldBeSuccess32Test")
                               .ForCustomValidator<LengthShould32Validator>()
                               .Build();

            var validator1 = ValidationMe.Use("UT_VerifyCustomValidatorAndShouldBeSuccess32Test").Resolve<Length32Model>();
            var validator2 = new LengthShould32Validator();
            
            var model = new Length32Model {Name = "12345678901234561234567890123456"};
            validator1.Verify(model).IsValid.ShouldBeTrue();
            validator2.Verify(model).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify an instance for CustomValidator and return a success VerifyResult.")]
        public void VerifyCustomValidatorAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true; //important

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyCustomValidatorAndShouldBeFailure32Test")
                               .ForCustomValidator(new LengthShould32Validator())
                               .Build();

            var validator1 = ValidationMe.Use("UT_VerifyCustomValidatorAndShouldBeFailure32Test").Resolve<Length32Model>();
            var validator2 = new LengthShould32Validator();
            
            var model = new Length32Model {Name = "12345678901234"};
            validator1.Verify(model).IsValid.ShouldBeFalse();
            validator2.Verify(model).IsValid.ShouldBeFalse();
            
        }
    }
}