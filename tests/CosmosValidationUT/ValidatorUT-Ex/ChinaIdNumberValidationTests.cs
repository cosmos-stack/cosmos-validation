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
            validator.Verify("370986890623212", ChinaIdStyles.Id15).IsValid.ShouldBeTrue();
            validator.Verify("370986890623212", ChinaIdStyles.Id15).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "To verify china id card's number / 15 and return failure")]
        public void VerifyIdCard15NumberAndShouldBeFailureTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("3709868906232150", ChinaIdStyles.Id15).IsValid.ShouldBeFalse();
            validator.Verify("37098689062325.0", ChinaIdStyles.Id15).IsValid.ShouldBeFalse();
            validator.Verify("37098689063125", ChinaIdStyles.Id15).IsValid.ShouldBeFalse();
            validator.Verify("37098689062R25", ChinaIdStyles.Id15).IsValid.ShouldBeFalse();
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

        [Fact(DisplayName = "To verify china id card's number / TW and return success")]
        public void VerifyTwIdCardNumberAndShouldBeSuccessTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("N227243218", ChinaIdStyles.Taiwan).IsValid.ShouldBeTrue();
            validator.Verify("K278798724", ChinaIdStyles.Taiwan).IsValid.ShouldBeTrue();
            validator.Verify("S185258113", ChinaIdStyles.Taiwan).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "To verify china id card's number / TW and return failure")]
        public void VerifyTwIdCardNumberAndShouldBeFailureTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("N127243218", ChinaIdStyles.Taiwan).IsValid.ShouldBeFalse();
            validator.Verify("2278798724", ChinaIdStyles.Taiwan).IsValid.ShouldBeFalse();
            validator.Verify("K278798729", ChinaIdStyles.Taiwan).IsValid.ShouldBeFalse();
            validator.Verify("S1852581130", ChinaIdStyles.Taiwan).IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "To verify china id card's number / HK and return success")]
        public void VerifyHkIdCardNumberAndShouldBeSuccessTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("P1032651", ChinaIdStyles.HkId03).IsValid.ShouldBeTrue();
            validator.Verify("P103265(1)", ChinaIdStyles.HkId03).IsValid.ShouldBeTrue();
            validator.Verify("U627656(0)", ChinaIdStyles.HkId03).IsValid.ShouldBeTrue();
            validator.Verify("R363137(A)", ChinaIdStyles.HkId03).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "To verify china id card's number / HK and return failure")]
        public void VerifyHkIdCardNumberAndShouldBeFailureTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("P103265(1", ChinaIdStyles.HkId03).IsValid.ShouldBeFalse();
            validator.Verify("P103265(2)", ChinaIdStyles.HkId03).IsValid.ShouldBeFalse();
            validator.Verify("Z627656(0)", ChinaIdStyles.HkId03).IsValid.ShouldBeFalse();
            validator.Verify("R363137", ChinaIdStyles.HkId03).IsValid.ShouldBeFalse();
            validator.Verify("0363137(A)", ChinaIdStyles.HkId03).IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "To verify china id card's number / MO and return success")]
        public void VerifyMoIdCardNumberAndShouldBeSuccessTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("12281575", ChinaIdStyles.Macau).IsValid.ShouldBeTrue();
            validator.Verify("1228155(7)", ChinaIdStyles.Macau).IsValid.ShouldBeTrue();
            validator.Verify("52152998", ChinaIdStyles.Macau).IsValid.ShouldBeTrue();
            validator.Verify("5215299(8)", ChinaIdStyles.Macau).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "To verify china id card's number / MO and return failure")]
        public void VerifyMoIdCardNumberAndShouldBeFailureTest()
        {
            var validator = ChinaIdNumberValidator.Instance;
            validator.Verify("1228155(7", ChinaIdStyles.Macau).IsValid.ShouldBeFalse();
            validator.Verify("1228155(79", ChinaIdStyles.Macau).IsValid.ShouldBeFalse();
            validator.Verify("122815790", ChinaIdStyles.Macau).IsValid.ShouldBeFalse();
            validator.Verify("2228157", ChinaIdStyles.Macau).IsValid.ShouldBeFalse();
        }
    }
}