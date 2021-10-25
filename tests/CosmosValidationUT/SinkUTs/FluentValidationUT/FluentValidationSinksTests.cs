using CosmosStack.Validation;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Registrars;
using CosmosValidationUT.SinkUTs.FluentValidationUT.Models;
using FluentAssertions;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.SinkUTs.FluentValidationUT
{
    [Trait("FluentValidationUT", "FluentValidationSinksTests")]
    public class FluentValidationSinksTests
    {
        public FluentValidationSinksTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact(DisplayName = "To test a model work with FluentValidation and should returns success.")]
        public void ValidationModelWorkWithFluentValidationAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar
                .ForProvider(provider, "UTF_ValidationModelWorkWithFluentValidationAndShouldBeSuccessTest")
                .ForFluentValidator<GanglvToniValidator>()
                .Build();

            var validator = ValidationMe.Use("UTF_ValidationModelWorkWithFluentValidationAndShouldBeSuccessTest")
                                        .Resolve<GanglvToni>();

            var model = new GanglvToni() { Name = "Good", Age = 11 };

            validator.Verify(typeof(GanglvToni), model).IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "To test a model work with FluentValidation and should returns failure.")]
        public void ValidationModelWorkWithFluentValidationAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar
                .ForProvider(provider, "UTF_ValidationModelWorkWithFluentValidationAndShouldBeFailureTest")
                .ForFluentValidator<GanglvToniValidator, GanglvToni>()
                .Build();

            var validator = ValidationMe.Use("UTF_ValidationModelWorkWithFluentValidationAndShouldBeFailureTest")
                                        .Resolve<GanglvToni>();

            var model1 = new GanglvToni() { Name = "", Age = 11 };
            var model2 = new GanglvToni() { Name = "11111111111", Age = 11 };
            var model3 = new GanglvToni() { Name = "Good", Age = 9 };
            var model4 = new GanglvToni() { Name = "", Age = -9 };

            var r1 = validator.Verify(typeof(GanglvToni), model1);
            var r2 = validator.Verify(typeof(GanglvToni), model2);
            var r3 = validator.Verify(typeof(GanglvToni), model3);
            var r4 = validator.Verify(typeof(GanglvToni), model4);

            r1.IsValid.ShouldBeFalse();
            r2.IsValid.ShouldBeFalse();
            r3.IsValid.ShouldBeFalse();
            r4.IsValid.ShouldBeFalse();

            r1.MemberNames.Should().HaveCount(1);
            r2.MemberNames.Should().HaveCount(1);
            r3.MemberNames.Should().HaveCount(1);
            r4.MemberNames.Should().HaveCount(2);
        }

        [Fact(DisplayName = "To test a member of model work with FluentValidation and should returns success.")]
        public void ValidationOneWorkWithFluentValidationAndShouldBeSuccessTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UTF_ValidationOneWorkWithFluentValidationAndShouldBeSuccessTest")
                               .ForFluentValidator<GanglvToniValidator>()
                               .Build();

            var validator = ValidationMe.Use("UTF_ValidationOneWorkWithFluentValidationAndShouldBeSuccessTest")
                                        .Resolve<GanglvToni>();

            var model = new GanglvToni() { Name = "Good", Age = 11 };

            validator.VerifyOne(x => x.Name, model.Name).IsValid.Should().BeTrue();
            validator.VerifyOne(model.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GanglvToni), model.Name, "Name").IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "To test a member if model work with FluentValidation and should returns failure.")]
        public void ValidationOneWorkWithFluentValidationAndShouldBeFailureTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = true;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UTF_ValidationOneWorkWithFluentValidationAndShouldBeFailureTest")
                               .ForFluentValidator<GanglvToniValidator>()
                               .Build();

            var validator = ValidationMe.Use("UTF_ValidationOneWorkWithFluentValidationAndShouldBeFailureTest")
                                        .Resolve<GanglvToni>();

            var model1 = new GanglvToni() { Name = "", Age = 11 };
            var model2 = new GanglvToni() { Name = "11111111111", Age = 11 };
            var model3 = new GanglvToni() { Name = "Good", Age = 9 };
            var model4 = new GanglvToni() { Name = "", Age = -9 };

            validator.VerifyOne(x => x.Name, model1.Name).IsValid.Should().BeFalse();
            validator.VerifyOne(model1.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model1.Name, "Name").IsValid.Should().BeFalse();

            validator.VerifyOne(x => x.Age, model1.Age).IsValid.Should().BeTrue();
            validator.VerifyOne(model1.Age, "Age").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GanglvToni), model1.Age, "Age").IsValid.Should().BeTrue();

            validator.VerifyOne(x => x.Name, model2.Name).IsValid.Should().BeFalse();
            validator.VerifyOne(model2.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model2.Name, "Name").IsValid.Should().BeFalse();

            validator.VerifyOne(x => x.Age, model2.Age).IsValid.Should().BeTrue();
            validator.VerifyOne(model2.Age, "Age").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GanglvToni), model2.Age, "Age").IsValid.Should().BeTrue();


            validator.VerifyOne(x => x.Name, model3.Name).IsValid.Should().BeTrue();
            validator.VerifyOne(model3.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GanglvToni), model3.Name, "Name").IsValid.Should().BeTrue();

            validator.VerifyOne(x => x.Age, model3.Age).IsValid.Should().BeFalse();
            validator.VerifyOne(model3.Age, "Age").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model3.Age, "Age").IsValid.Should().BeFalse();


            validator.VerifyOne(x => x.Name, model4.Name).IsValid.Should().BeFalse();
            validator.VerifyOne(model4.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model4.Name, "Name").IsValid.Should().BeFalse();

            validator.VerifyOne(x => x.Age, model4.Age).IsValid.Should().BeFalse();
            validator.VerifyOne(model4.Age, "Age").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model4.Age, "Age").IsValid.Should().BeFalse();
        }
    }
}