using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "CustomValidator")]
    public class LengthShould16ValidatorTests
    {
        public LengthShould16ValidatorTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            ValidationObjectResolver = new BuildInObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IValidationObjectResolver ValidationObjectResolver { get; set; }

        [Fact(DisplayName = "Verify an instance for CustomValidator and return a success VerifyResult.")]
        public void VerifyCustomValidatorAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true; //important

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyCustomValidatorAndShouldBeSuccessTest")
                               .ForCustomValidator<LengthShould16Validator>()
                               .Build();

            var validator1 = ValidationMe.Use("UT_VerifyCustomValidatorAndShouldBeSuccessTest").Resolve<Length16Model>();
            var validator2 = new LengthShould16Validator();

            var model = new Length16Model {Name = "1234567890123456"};
            validator1.Verify(model).IsValid.ShouldBeTrue();
            validator2.Verify(typeof(Length16Model), model).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify an instance for CustomValidator and return a success VerifyResult.")]
        public void VerifyCustomValidatorAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true; //important

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyCustomValidatorAndShouldBeFailureTest")
                               .ForCustomValidator(new LengthShould16Validator())
                               .Build();

            var validator1 = ValidationMe.Use("UT_VerifyCustomValidatorAndShouldBeFailureTest").Resolve<Length16Model>();
            var validator2 = new LengthShould16Validator();

            var model = new Length16Model {Name = "12345678901234"};
            validator1.Verify(model).IsValid.ShouldBeFalse();
            validator2.Verify(typeof(Length16Model), model).IsValid.ShouldBeFalse();
        }
    }
}