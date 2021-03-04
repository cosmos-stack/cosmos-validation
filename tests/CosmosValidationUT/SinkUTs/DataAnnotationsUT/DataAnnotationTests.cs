using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;
using CosmosValidationUT.SinkUTs.DataAnnotationsUT.Models;
using FluentAssertions;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.SinkUTs.DataAnnotationsUT
{
    [Trait("DataAnnotationsUT", "DataAnnotationTests")]
    public class DataAnnotationTests
    {
        [Fact(DisplayName = "To test a model with DataAnnotation and should returns success.")]
        public void ValidationModelWithDataAnnotationAndShouldBeSuccessTest()
        {
            var model = new GoodJob() {Name = "Good", Cost = 11};
            var validator = new DataAnnotationValidator();

            validator.Verify(typeof(GoodJob), model).IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "To test a model with DataAnnotation and should returns failure.")]
        public void ValidationModelWithDataAnnotationAndShouldBeFailureTest()
        {
            var model1 = new GoodJob() {Name = "", Cost = 11};
            var model2 = new GoodJob() {Name = "11111111111", Cost = 11};
            var model3 = new GoodJob() {Name = "Good", Cost = 9};
            var model4 = new GoodJob() {Name = "", Cost = -9};
            var validator = new DataAnnotationValidator();

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
            var model = new GoodJob() {Name = "Good", Cost = 11};
            var validator = new DataAnnotationValidator();

            validator.VerifyOne(typeof(GoodJob), model.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GoodJob), model.Cost, "Cost").IsValid.Should().BeTrue();
        }
        
        [Fact(DisplayName = "To test a member of model with DataAnnotation and should returns failure.")]
        public void ValidationOneWithDataAnnotationAndShouldBeFailureTest()
        {
            var model1 = new GoodJob() {Name = "", Cost = 11};
            var model2 = new GoodJob() {Name = "11111111111", Cost = 11};
            var model3 = new GoodJob() {Name = "Good", Cost = 9};
            var model4 = new GoodJob() {Name = "", Cost = -9};
            var validator = new DataAnnotationValidator();
            
            validator.VerifyOne(typeof(GoodJob), model1.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model1.Cost, "Cost").IsValid.Should().BeTrue();
            
            validator.VerifyOne(typeof(GoodJob), model2.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model2.Cost, "Cost").IsValid.Should().BeTrue();
            
            validator.VerifyOne(typeof(GoodJob), model3.Name, "Name").IsValid.Should().BeTrue();
            validator.VerifyOne(typeof(GoodJob), model3.Cost, "Cost").IsValid.Should().BeFalse();
            
            validator.VerifyOne(typeof(GoodJob), model4.Name, "Name").IsValid.Should().BeFalse();
            validator.VerifyOne(typeof(GoodJob), model4.Cost, "Cost").IsValid.Should().BeFalse();
        }
    }
}