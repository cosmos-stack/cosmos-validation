using Cosmos.Date;
using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ConditionUT
{
    [Trait("ConditionUT", "ConditionBasicTests")]
    public class ConditionBasicTests
    {
        public ConditionBasicTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact]
        public void SimpleAndOps_ShouldBeSuccess_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleAndOps_ShouldBeSuccess_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(10).And().MinLength(8)
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleAndOps_ShouldBeSuccess_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void SimpleAndOps_ShouldBeFailure_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleAndOps_ShouldBeFailure_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(10).And().MinLength(8)
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleAndOps_ShouldBeFailure_Test").Resolve<NiceBoat>();

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

            result1.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void SimpleOrOps_ShouldBeSuccess_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleOrOps_ShouldBeSuccess_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Width).GreaterThan(8).Or().LessThan(8)
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleOrOps_ShouldBeSuccess_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void SimpleOrOps_ShouldBeFailure_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleOrOps_ShouldBeFailure_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Width).GreaterThan(12).Or().LessThan(8)
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleOrOps_ShouldBeFailure_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1",
                Length = 1000,
                Width = 10,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();

            result1.IsValid.ShouldBeFalse();
        }
    }
}