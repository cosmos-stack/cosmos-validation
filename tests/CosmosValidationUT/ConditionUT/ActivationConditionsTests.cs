using CosmosStack.Date;
using CosmosStack.Validation;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Registrars;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ConditionUT
{
    [Trait("ConditionUT", "ActivationConditionsTests")]
    public class ActivationConditionsTests
    {
        public ActivationConditionsTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact]
        public void SimpleAC_ShouldBeSuccess_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleAC_ShouldBeSuccess_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(8).When(x => x.StartsWith("M"))
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleAC_ShouldBeSuccess_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1", //9
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
        public void SimpleAC_ShouldBeFailure_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleAC_ShouldBeFailure_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(8).When(x => x.StartsWith("N"))
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleAC_ShouldBeFailure_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1", //9
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
        public void SimpleAndOps_WithAC_ShouldBeSuccess_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleAndOps_WithAC_ShouldBeSuccess_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(8).When(x => x.StartsWith("M")).And().MinLength(7).Unless(x => x.StartsWith("N"))
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleAndOps_WithAC_ShouldBeSuccess_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1", //9
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
        public void SimpleAndOps_WithAC_ShouldBeFailure_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleAndOps_WithAC_ShouldBeFailure_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(8).When(x => x.StartsWith("M")).And().MinLength(10).Unless(x => x.StartsWith("N"))
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleAndOps_WithAC_ShouldBeFailure_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1", //9
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
        public void SimpleOrOps_WithAC_ShouldBeSuccess_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleOrOps_WithAC_ShouldBeSuccess_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(8).When(x => x.StartsWith("N")).Or().MinLength(7).Unless(x => x.StartsWith("N"))
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleOrOps_WithAC_ShouldBeSuccess_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1", //9
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
        public void SimpleOrOps_WithAC_ShouldBeFailure_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_SimpleOrOps_WithAC_ShouldBeFailure_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(8).When(x => x.StartsWith("N")).Or().MinLength(10).Unless(x => x.StartsWith("N"))
                               .Build();

            var validator = ValidationMe.Use("UT_SimpleOrOps_WithAC_ShouldBeFailure_Test").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1", //9
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
        public void ConditionWithAndOr_WithAC_ShouldBeSuccess_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_ConditionWithAndOr_WithAC_ShouldBeSuccess_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(8).When(x => x.StartsWith("M")).And().MinLength(6).When(x => x.StartsWith("M")).Or().MinLength(7).Unless(x => x.StartsWith("N")).Or().Empty()
                               .Build();

            var validator = ValidationMe.Use("UT_ConditionWithAndOr_WithAC_ShouldBeSuccess_Test").Resolve<NiceBoat>();

            var instance1 = new NiceBoat
            {
                Name = "NiceBoat1", //9
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var instance2 = new NiceBoat
            {
                Name = "", //0
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance1);
            var result2 = validator.Verify(instance2);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
            result2.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ConditionWithAndOr_WithAC_Complex_ShouldBeSuccess_Test()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_ConditionWithAndOr_WithAC_Complex_ShouldBeSuccess_Test")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name)
                               .MaxLength(8).When((o, v) => o.Width == 30 && v.StartsWith("M")).And().MinLength(6).When(x => x.StartsWith("M"))
                               .Or().MinLength(7).Unless((o, v) => v.StartsWith("M") || o.Email == "nice@boat.com")
                               .Or().Empty().When((o, v) => o.Length == 1000)
                               .Build();

            var validator = ValidationMe.Use("UT_ConditionWithAndOr_WithAC_Complex_ShouldBeSuccess_Test").Resolve<NiceBoat>();

            var instance1 = new NiceBoat
            {
                Name = "NiceBoat1", //9
                Length = 1001,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var instance2 = new NiceBoat
            {
                Name = "", //0
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance1);
            var result2 = validator.Verify(instance2);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
            result2.IsValid.ShouldBeTrue();
        }
    }
}