using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars;
using CosmosValidationUT.SinkUTs.DataAnnotationsUT.Models;
using FluentAssertions;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.SinkUTs.DataAnnotationsUT
{
    [Trait("DataAnnotationsUT", "DataAnnotationSinksTests")]
    public class DataAnnotationSinksTests
    {
        public DataAnnotationSinksTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact(DisplayName = "To test a model with DataAnnotation and should returns success.")]
        public void ValidationModelWithDataAnnotationAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_ValidationModelWithDataAnnotationAndShouldBeSuccessTest")
                               .ForDataAnnotationSupport()
                               .Build();

            var validator = ValidationMe.Use("UT_ValidationModelWithDataAnnotationAndShouldBeSuccessTest").Resolve<GoodJob>();

            var model = new GoodJob() {Name = "Good", Cost = 11};

            validator.Verify(typeof(GoodJob), model).IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "To test a model with DataAnnotation and should returns failure.")]
        public void ValidationModelWithDataAnnotationAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_ValidationModelWithDataAnnotationAndShouldBeFailureTest")
                               .ForDataAnnotationSupport()
                               .Build();

            var validator = ValidationMe.Use("UT_ValidationModelWithDataAnnotationAndShouldBeFailureTest").Resolve<GoodJob>();

            var model1 = new GoodJob() {Name = "", Cost = 11};
            var model2 = new GoodJob() {Name = "11111111111", Cost = 11};
            var model3 = new GoodJob() {Name = "Good", Cost = 9};
            var model4 = new GoodJob() {Name = "", Cost = -9};

            var r1 = validator.Verify(typeof(GoodJob), model1);
            var r2 = validator.Verify(typeof(GoodJob), model2);
            var r3 = validator.Verify(typeof(GoodJob), model3);
            var r4 = validator.Verify(typeof(GoodJob), model4);

            r1.IsValid.ShouldBeFalse();
            r2.IsValid.ShouldBeFalse();
            r3.IsValid.ShouldBeFalse();
            r4.IsValid.ShouldBeFalse();

            r1.MemberNames.Should().HaveCount(1);
            r2.MemberNames.Should().HaveCount(1);
            r3.MemberNames.Should().HaveCount(1);
            r4.MemberNames.Should().HaveCount(2);
        }

        [Fact(DisplayName = "To test a member of model with DataAnnotation and should returns success.")]
        public void ValidationOneWithDataAnnotationAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_ValidationOneWithDataAnnotationAndShouldBeSuccessTest")
                               .ForDataAnnotationSupport()
                               .Build();

            var validator = ValidationMe.Use("UT_ValidationOneWithDataAnnotationAndShouldBeSuccessTest").Resolve<GoodJob>();

            var model = new GoodJob() {Name = "Good", Cost = 11};

            validator.VerifyOne(x => x.Name, model.Name).IsValid.Should().BeTrue();
            validator.VerifyOne(model.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GoodJob), model.Name, "Name").IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "To test a member if model with DataAnnotation and should returns failure.")]
        public void ValidationOneWithDataAnnotationAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_ValidationOneWithDataAnnotationAndShouldBeFailureTest")
                               .ForDataAnnotationSupport()
                               .Build();

            var validator = ValidationMe.Use("UT_ValidationOneWithDataAnnotationAndShouldBeFailureTest").Resolve<GoodJob>();

            var model1 = new GoodJob() {Name = "", Cost = 11};
            var model2 = new GoodJob() {Name = "11111111111", Cost = 11};
            var model3 = new GoodJob() {Name = "Good", Cost = 9};
            var model4 = new GoodJob() {Name = "", Cost = -9};

            validator.VerifyOne(x => x.Name, model1.Name).IsValid.Should().BeFalse();
            validator.VerifyOne(model1.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model1.Name, "Name").IsValid.Should().BeFalse();

            validator.VerifyOne(x => x.Cost, model1.Cost).IsValid.Should().BeTrue();
            validator.VerifyOne(model1.Cost, "Cost").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GoodJob), model1.Cost, "Cost").IsValid.Should().BeTrue();


            validator.VerifyOne(x => x.Name, model2.Name).IsValid.Should().BeFalse();
            validator.VerifyOne(model2.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model2.Name, "Name").IsValid.Should().BeFalse();

            validator.VerifyOne(x => x.Cost, model2.Cost).IsValid.Should().BeTrue();
            validator.VerifyOne(model2.Cost, "Cost").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GoodJob), model2.Cost, "Cost").IsValid.Should().BeTrue();


            validator.VerifyOne(x => x.Name, model3.Name).IsValid.Should().BeTrue();
            validator.VerifyOne(model3.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GoodJob), model3.Name, "Name").IsValid.Should().BeTrue();

            validator.VerifyOne(x => x.Cost, model3.Cost).IsValid.Should().BeFalse();
            validator.VerifyOne(model3.Cost, "Cost").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model3.Cost, "Cost").IsValid.Should().BeFalse();


            validator.VerifyOne(x => x.Name, model4.Name).IsValid.Should().BeFalse();
            validator.VerifyOne(model4.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model4.Name, "Name").IsValid.Should().BeFalse();

            validator.VerifyOne(x => x.Cost, model4.Cost).IsValid.Should().BeFalse();
            validator.VerifyOne(model4.Cost, "Cost").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model4.Cost, "Cost").IsValid.Should().BeFalse();
        }
    }
}