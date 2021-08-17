using Cosmos.Validation;
using Cosmos.Validation.Validators;
using Shouldly;
using Xunit;

// ReSharper disable once CheckNamespace
namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "ChinaIdNumberValidator")]
    public class ChinaIdNumberValidatorTests
    {
        [Fact(DisplayName = "To verify china id card's number / 15 and return success")]
        public void VerifyIdCard15NumberAndShouldBeSuccessTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("370986890623212", ChinaIdLength.Id15).IsValid.ShouldBeTrue();
            validator.Verify("370986890623212", ChinaIdLength.Id15).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "To verify china id card's number / 15 and return failure")]
        public void VerifyIdCard15NumberAndShouldBeFailureTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("3709868906232150", ChinaIdLength.Id15).IsValid.ShouldBeFalse();
            validator.Verify("37098689062325.0", ChinaIdLength.Id15).IsValid.ShouldBeFalse();
            validator.Verify("37098689063125", ChinaIdLength.Id15).IsValid.ShouldBeFalse();
            validator.Verify("37098689062R25", ChinaIdLength.Id15).IsValid.ShouldBeFalse();
        }
        
        [Fact(DisplayName = "To verify china id card's number / 18 and return success")]
        public void VerifyIdCard18NumberAndShouldBeSuccessTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("110101199003076990").IsValid.ShouldBeTrue();
            validator.Verify("110101199003070898").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "To verify china id card's number / 18 and return failure")]
        public void VerifyIdCard18NumberAndShouldBeFailureTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("110101199003076991").IsValid.ShouldBeFalse();
            validator.Verify("110101199003070899").IsValid.ShouldBeFalse();
            validator.Verify("110101199003320899").IsValid.ShouldBeFalse();
            validator.Verify("1101011990030R0899").IsValid.ShouldBeFalse();
            validator.Verify("11R101199003070899").IsValid.ShouldBeFalse();
        }
    }
}