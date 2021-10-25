using CosmosStack.Validation.Validators;
using CosmosValidationUT.SinkUTs.FluentValidationUT.Models;
using FluentAssertions;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.SinkUTs.FluentValidationUT
{
    [Trait("FluentValidationUT", "FluentValidationTests")]
    public class FluentValidationTests
    {
        [Fact(DisplayName = "To test a model work with FluentValidation and should returns success.")]
        public void ValidationModelWorkWithFluentValidationAndShouldBeSuccessTest()
        {
            var model = new GanglvToni() {Name = "XiE", Age = 11};
            var validator1 = new GanglvToniValidator();
            var validator2 = new FluentValidator(validator1);
            var validator3 = new FluentValidator(typeof(GanglvToniValidator));
            var validator4 = new FluentValidator<GanglvToniValidator>();
            var validator5 = new FluentValidator<GanglvToniValidator, GanglvToni>();

            validator1.Validate(model).IsValid.Should().BeTrue();
            validator2.Verify(typeof(GanglvToni), model).IsValid.Should().BeTrue();
            validator3.Verify(typeof(GanglvToni), model).IsValid.Should().BeTrue();
            validator4.Verify(typeof(GanglvToni), model).IsValid.Should().BeTrue();
            validator5.Verify(model).IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "To test a model work with FluentValidation and should returns failure.")]
        public void ValidationModelWorkWithFluentValidationAndShouldBeFailureTest()
        {
            var model1 = new GanglvToni() {Name = "", Age = 11};
            var model2 = new GanglvToni() {Name = "11111111111", Age = 11};
            var model3 = new GanglvToni() {Name = "What", Age = 9};
            var model4 = new GanglvToni() {Name = "", Age = -9};
            var validator = new FluentValidator<GanglvToniValidator, GanglvToni>();

            var r1 = validator.Verify(model1);
            var r2 = validator.Verify(model2);
            var r3 = validator.Verify(model3);
            var r4 = validator.Verify(model4);

            r1.IsValid.ShouldBeFalse();
            r2.IsValid.ShouldBeFalse();
            r3.IsValid.ShouldBeFalse();
            r4.IsValid.ShouldBeFalse();

            r1.MemberNames.Should().HaveCount(1);
            r2.MemberNames.Should().HaveCount(1);
            r3.MemberNames.Should().HaveCount(1);
            r4.MemberNames.Should().HaveCount(2);
        }

        [Fact(DisplayName = "To test a member of work model with FluentValidation and should returns success.")]
        public void ValidationOneWorkWithFluentValidationAndShouldBeSuccessTest()
        {
            var model = new GanglvToni() {Name = "Good", Age = 11};
            var validator = new FluentValidator<GanglvToniValidator, GanglvToni>();

            validator.VerifyOne(typeof(GanglvToni), model.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GanglvToni), model.Age, "Age").IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "To test a member of work model with FluentValidation and should returns failure.")]
        public void ValidationOneWorkWithFluentValidationAndShouldBeFailureTest()
        {
            var model1 = new GanglvToni() {Name = "", Age = 11};
            var model2 = new GanglvToni() {Name = "11111111111", Age = 11};
            var model3 = new GanglvToni() {Name = "Good", Age = 9};
            var model4 = new GanglvToni() {Name = "", Age = -9};
            var validator = new FluentValidator<GanglvToniValidator, GanglvToni>();

            validator.VerifyOne(typeof(GanglvToni), model1.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model1.Age, "Age").IsValid.Should().BeTrue();

            validator.VerifyOne(typeof(GanglvToni), model2.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model2.Age, "Age").IsValid.Should().BeTrue();

            validator.VerifyOne(typeof(GanglvToni), model3.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GanglvToni), model3.Age, "Age").IsValid.Should().BeFalse();

            validator.VerifyOne(typeof(GanglvToni), model4.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GanglvToni), model4.Age, "Age").IsValid.Should().BeFalse();
        }
    }
}