using Cosmos.Date;
using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.StrategyUT
{
    [Trait("StrategyUT", "StrategyTests")]
    public class StrategyTests
    {
        public StrategyTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact(DisplayName = "Use normal strategy by generic way and verify an instance and return success")]
        public void UseNormalStrategyAndVerifyInstanceAndReturnSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_UseNormalStrategyAndVerifyInstanceAndReturnSuccessTest")
                               .ForStrategy<NormalNiceBoatStrategy>()
                               .Build();

            var validator = ValidationMe.Use("UT_UseNormalStrategyAndVerifyInstanceAndReturnSuccessTest").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Use normal strategy by new() way and verify an instance and return failure")]
        public void UseNormalStrategyAndVerifyInstanceAndReturnFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_UseNormalStrategyAndVerifyInstanceAndReturnFailureTest")
                               .ForStrategy(new NormalNiceBoatStrategy())
                               .Build();

            var validator = ValidationMe.Use("UT_UseNormalStrategyAndVerifyInstanceAndReturnFailureTest").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "",
                Length = -1000,
                Width = -30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();
            result1.IsValid.ShouldBeFalse();
            result1.Errors.Count.ShouldBe(3);
        }

        [Fact(DisplayName = "Use generic strategy by generic way and verify an instance and return success")]
        public void UseGenericStrategyAndVerifyInstanceAndReturnSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_UseGenericStrategyAndVerifyInstanceAndReturnSuccessTest")
                               .ForStrategy<GenericNiceBoatStrategy, NiceBoat>()
                               .Build();

            var validator = ValidationMe.Use("UT_UseGenericStrategyAndVerifyInstanceAndReturnSuccessTest").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Use generic strategy by new() way and verify an instance and return failure")]
        public void UseGenericStrategyAndVerifyInstanceAndReturnFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);
            
            ValidationRegistrar.ForProvider(provider, "UT_UseGenericStrategyAndVerifyInstanceAndReturnFailureTest")
                               .ForStrategy(new GenericNiceBoatStrategy())
                               .Build();

            var validator = ValidationMe.Use("UT_UseGenericStrategyAndVerifyInstanceAndReturnFailureTest").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "",
                Length = -1000,
                Width = -30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();
            result1.IsValid.ShouldBeFalse();
            result1.Errors.Count.ShouldBe(3);
        }
    }
}