using System;
using System.Collections.Generic;
using Cosmos.Date;
using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars;
using Cosmos.Validation.Validators;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "AggregationValidator")]
    public class AggregationValidatorTests
    {
        public AggregationValidatorTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            ValidationObjectResolver = new BuildInObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IValidationObjectResolver ValidationObjectResolver { get; set; }

        [Fact(DisplayName = "Verify an instance and return a success VerifyResult.")]
        public void VerifyAnByInstanceShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyAnByInstanceShouldBeSuccessTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyAnByInstanceShouldBeSuccessTest").Resolve<NiceBoat>();

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

        [Fact(DisplayName = "Verify an instance and return a failure VerifyResult.")]
        public void VerifyAnByInstanceShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyAnByInstanceShouldBeFailureTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyAnByInstanceShouldBeFailureTest").Resolve<NiceBoat>();

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

        [Fact(DisplayName = "VerifyOne and return a success VerifyResult.")]
        public void VerifyOneShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyOneShouldBeSuccessTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyOneShouldBeSuccessTest").Resolve<NiceBoat>();

            validator.VerifyOne(typeof(string), "Nice", "Name").IsValid.ShouldBeTrue();
            validator.VerifyOne(typeof(long), 1000, "Length").IsValid.ShouldBeTrue();
            validator.VerifyOne(typeof(long), 20, "Width").IsValid.ShouldBeTrue();
            validator.VerifyOne(typeof(DateTime), DateTimeFactory.Create(2020, 12, 21), "CreateTime").IsValid.ShouldBeTrue();
            validator.VerifyOne(typeof(string), "nice@boat.com", "Email").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyOne and return a failure VerifyResult.")]
        public void VerifyOneShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyOneShouldBeFailureTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyOneShouldBeFailureTest").Resolve<NiceBoat>();

            validator.VerifyOne(typeof(string), "", "Name").IsValid.ShouldBeFalse();
            validator.VerifyOne(typeof(long), -1000, "Length").IsValid.ShouldBeFalse();
            validator.VerifyOne(typeof(long), -20, "Width").IsValid.ShouldBeFalse();
            validator.VerifyOne(typeof(DateTime), DateTimeFactory.Create(2020, 12, 21), "CreateTime").IsValid.ShouldBeTrue();
            validator.VerifyOne(typeof(string), "nice@@boat.com", "Email").IsValid.ShouldBeTrue(); // Because Annotation and CustomValidation are all disable.
        }

        [Fact(DisplayName = "VerifyOne with property selector and return a success VerifyResult.")]
        public void VerifyOneWithPropertySelectorAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyOneWithPropertySelectorAndShouldBeSuccessTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyOneWithPropertySelectorAndShouldBeSuccessTest").Resolve<NiceBoat>();

            validator.VerifyOne(x => x.Name, "Nice").IsValid.ShouldBeTrue();
            validator.VerifyOne(x => x.Length, 1000).IsValid.ShouldBeTrue();
            validator.VerifyOne(x => x.Width, 30).IsValid.ShouldBeTrue();
            validator.VerifyOne(x => x.CreateTime, DateTimeFactory.Create(2020, 12, 21)).IsValid.ShouldBeTrue();
            validator.VerifyOne(x => x.Email, "nice@boat.com").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyOne with property selector and return a failure VerifyResult.")]
        public void VerifyOneWithPropertySelectorAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyOneWithPropertySelectorAndShouldBeFailureTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyOneWithPropertySelectorAndShouldBeFailureTest").Resolve<NiceBoat>();

            validator.VerifyOne(x => x.Name, "").IsValid.ShouldBeFalse();
            validator.VerifyOne(x => x.Length, -1000).IsValid.ShouldBeFalse();
            validator.VerifyOne(x => x.Width, -30).IsValid.ShouldBeFalse();
            validator.VerifyOne(x => x.CreateTime, DateTimeFactory.Create(2020, 12, 21)).IsValid.ShouldBeTrue();
            validator.VerifyOne(x => x.Email, "nice@@boat.com").IsValid.ShouldBeTrue(); // Because Annotation and CustomValidation are all disable.
        }

        [Fact(DisplayName = "VerifyMany a dictionary and return a success VerifyResult.")]
        public void VerifyManyByDictionaryShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyAnByDictionaryShouldBeSuccessTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyAnByDictionaryShouldBeSuccessTest").Resolve<NiceBoat>();

            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var result1 = validator.VerifyMany(d);

            result1.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyMany a dictionary and return a failure VerifyResult.")]
        public void VerifyManyByDictionaryShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.CustomValidatorEnabled = false;
            options.FailureIfProjectNotMatch = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyAnByDictionaryShouldBeFailureTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyAnByDictionaryShouldBeFailureTest").Resolve<NiceBoat>();

            var d = new Dictionary<string, object>
            {
                ["Name"] = "",
                ["Length"] = -1000,
                ["Width"] = -30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@@boat.com"
            };

            var result1 = validator.VerifyMany(d);

            result1.ShouldNotBeNull();
            result1.IsValid.ShouldBeFalse();
            result1.Errors.Count.ShouldBe(3);
        }

        [Fact(DisplayName = "Verify an instance with Annotations and return a failure VerifyResult.")]
        public void VerifyAnByInstanceWithAnnotationTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = true;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyAnByInstanceWithAnnotationTest")
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyAnByInstanceWithAnnotationTest").Resolve<NiceBoat>();

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
            result1.Errors.Count.ShouldBe(4);
        }

        [Fact(DisplayName = "Verify an instance with CustomValidators and return a failure VerifyResult.")]
        public void VerifyAnByInstanceWithCustomValidatorTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, ValidationObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_VerifyAnByInstanceWithCustomValidatorTest")
                               .ForValidator<EmailValidator, string>()
                               .ForType<NiceBoat>()
                               .ForMember("Name").NotEmpty()
                               .AndForMember("Length").GreaterThanOrEqual(0)
                               .AndForMember("Width").GreaterThanOrEqual(0)
                               .Build();

            var validator = ValidationMe.Use("UT_VerifyAnByInstanceWithCustomValidatorTest").Resolve<NiceBoat>();

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
            result1.Errors.Count.ShouldBe(4);
        }
    }
}