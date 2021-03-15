using System;
using System.Collections.Generic;
using Cosmos.Date;
using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars;
using Cosmos.Validation.Validators;
using CosmosValidationUT.Models;
using FluentAssertions;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "ValidationHandler")]
    public class ValidationHandlerTests
    {
        public ValidationHandlerTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact(DisplayName = "Verify an instance and return a success VerifyResult.")]
        public void VerifyAnByInstanceShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyAnByInstanceShouldBeSuccessTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = handler.Verify(instance);

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

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyAnByInstanceShouldBeFailureTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            var instance = new NiceBoat
            {
                Name = "",
                Length = -1000,
                Width = -30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@@boat.com"
            };

            var result1 = handler.Verify(instance);

            result1.ShouldNotBeNull();
            result1.IsValid.ShouldBeFalse();
            
            result1.Errors.Should().HaveCount(3);
        }

        [Fact(DisplayName = "VerifyOne and return a success VerifyResult.")]
        public void VerifyOneShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyOneShouldBeSuccessTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            handler.VerifyOne(typeof(NiceBoat), "Nice", "Name").IsValid.ShouldBeTrue();
            handler.VerifyOne(typeof(NiceBoat), 1000, "Length").IsValid.ShouldBeTrue();
            handler.VerifyOne(typeof(NiceBoat), 20, "Width").IsValid.ShouldBeTrue();
            handler.VerifyOne(typeof(NiceBoat), DateTimeFactory.Create(2020, 12, 21), "CreateTime").IsValid.ShouldBeTrue();
            handler.VerifyOne(typeof(NiceBoat), "nice@boat.com", "Email").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyOne and return a failure VerifyResult.")]
        public void VerifyOneShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyOneShouldBeFailureTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            handler.VerifyOne(typeof(NiceBoat), "", "Name").IsValid.ShouldBeFalse();
            handler.VerifyOne(typeof(NiceBoat), -1000, "Length").IsValid.ShouldBeFalse();
            handler.VerifyOne(typeof(NiceBoat), -20, "Width").IsValid.ShouldBeFalse();
            handler.VerifyOne(typeof(NiceBoat), DateTimeFactory.Create(2020, 12, 21), "CreateTime").IsValid.ShouldBeTrue();
            handler.VerifyOne(typeof(NiceBoat), "nice@@boat.com", "Email").IsValid.ShouldBeTrue(); // Because Annotation and CustomValidation are all disable.
        }

        [Fact(DisplayName = "VerifyOne with property selector and return a success VerifyResult.")]
        public void VerifyOneWithPropertySelectorAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyOneWithPropertySelectorAndShouldBeSuccessTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            handler.VerifyOne<NiceBoat, string>(x => x.Name, "Nice").IsValid.ShouldBeTrue();
            handler.VerifyOne<NiceBoat, long>(x => x.Length, 1000).IsValid.ShouldBeTrue();
            handler.VerifyOne<NiceBoat, long>(x => x.Width, 30).IsValid.ShouldBeTrue();
            handler.VerifyOne<NiceBoat, DateTime>(x => x.CreateTime, DateTimeFactory.Create(2020, 12, 21)).IsValid.ShouldBeTrue();
            handler.VerifyOne<NiceBoat, string>(x => x.Email, "nice@boat.com").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyOne with property selector and return a failure VerifyResult.")]
        public void VerifyOneWithPropertySelectorAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyOneWithPropertySelectorAndShouldBeFailureTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            handler.VerifyOne<NiceBoat, string>(x => x.Name, "").IsValid.ShouldBeFalse();
            handler.VerifyOne<NiceBoat, long>(x => x.Length, -1000).IsValid.ShouldBeFalse();
            handler.VerifyOne<NiceBoat, long>(x => x.Width, -30).IsValid.ShouldBeFalse();
            handler.VerifyOne<NiceBoat, DateTime>(x => x.CreateTime, DateTimeFactory.Create(2020, 12, 21)).IsValid.ShouldBeTrue();
            handler.VerifyOne<NiceBoat, string>(x => x.Email, "nice@@boat.com").IsValid.ShouldBeTrue(); // Because Annotation and CustomValidation are all disable.
        }

        [Fact(DisplayName = "VerifyMany a dictionary and return a success VerifyResult.")]
        public void VerifyManyByDictionaryShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyAnByDictionaryShouldBeSuccessTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var result1 = handler.VerifyMany(typeof(NiceBoat), d);

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

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyAnByDictionaryShouldBeFailureTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            var d = new Dictionary<string, object>
            {
                ["Name"] = "",
                ["Length"] = -1000,
                ["Width"] = -30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@@boat.com"
            };

            var result1 = handler.VerifyMany(typeof(NiceBoat), d);

            result1.ShouldNotBeNull();
            result1.IsValid.ShouldBeFalse();
            result1.Errors.Should().HaveCount(3);
        }

        [Fact(DisplayName = "Verify an instance with Annotations and return a failure VerifyResult.")]
        public void VerifyAnByInstanceWithAnnotationTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = true;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyAnByInstanceWithAnnotationTest")
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            var instance = new NiceBoat
            {
                Name = "",
                Length = -1000,
                Width = -30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@@boat.com"
            };

            var result1 = handler.Verify(instance);

            result1.ShouldNotBeNull();
            result1.IsValid.ShouldBeFalse();
            result1.Errors.Should().HaveCount(4);
        }

        [Fact(DisplayName = "Verify an instance with CustomValidators and return a failure VerifyResult.")]
        public void VerifyAnByInstanceWithCustomValidatorTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            var handler = ValidationRegistrar.ForProvider(provider, "UTH_VerifyAnByInstanceWithCustomValidatorTest")
                                             .ForCustomValidator<EmailValidator, string>()
                                             .ForType<NiceBoat>()
                                             .ForMember("Name").NotEmpty()
                                             .AndForMember("Length").GreaterThanOrEqual(0)
                                             .AndForMember("Width").GreaterThanOrEqual(0)
                                             .TempBuild();

            var instance = new NiceBoat
            {
                Name = "",
                Length = -1000,
                Width = -30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@@boat.com"
            };

            var result1 = handler.Verify(instance);

            result1.ShouldNotBeNull();
            result1.IsValid.ShouldBeFalse();
            result1.Errors.Should().HaveCount(4);
        }
    }
}