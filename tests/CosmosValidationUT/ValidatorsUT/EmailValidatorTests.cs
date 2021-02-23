using Cosmos.Validation.Validators;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "EmailValidator")]
    public class EmailValidatorTests
    {
        [Fact(DisplayName = "To verify email address and return success")]
        public void VerifyEmailAddressAndShouldBeSuccessTest()
        {
            var validator = EmailValidator.Instance;

            validator.Verify("280000000@qq.com").IsValid.ShouldBeTrue();
            validator.Verify("alex@lewis.com").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "To verify email address and return failure")]
        public void VerifyEmailAddressAndShouldBeFailureTest()
        {
            var validator = EmailValidator.Instance;
            validator.Verify("280000000").IsValid.ShouldBeFalse();
            validator.Verify("280000000@").IsValid.ShouldBeFalse();
            validator.Verify("280000000@nice").IsValid.ShouldBeFalse();
            validator.Verify("alex@@lewis.com").IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "To verify international email address")]
        public void VerifyInternationalEmailTest()
        {
            var validator = EmailValidator.Instance;

            validator.Verify("霸道@qq.com").IsValid.ShouldBeFalse();
            validator.Verify("霸道@总裁.com").IsValid.ShouldBeFalse();
            validator.Verify("霸道1008@qq.com").IsValid.ShouldBeFalse();
            validator.Verify("霸道1008@总裁2020.com").IsValid.ShouldBeFalse();

            validator.Verify("霸道@qq.com", allowInternational: true).IsValid.ShouldBeTrue();
            validator.Verify("霸道@总裁.com", allowInternational: true).IsValid.ShouldBeTrue();
            validator.Verify("霸道1008@qq.com", allowInternational: true).IsValid.ShouldBeTrue();
            validator.Verify("霸道1008@总裁2020.com", allowInternational: true).IsValid.ShouldBeTrue();

            validator.Verify("大王").IsValid.ShouldBeFalse();
            validator.Verify("大王@").IsValid.ShouldBeFalse();
            validator.Verify("大王@nice").IsValid.ShouldBeFalse();
            validator.Verify("大王@@救我.com").IsValid.ShouldBeFalse();

            validator.Verify("大王", allowInternational: true).IsValid.ShouldBeFalse();
            validator.Verify("大王@", allowInternational: true).IsValid.ShouldBeFalse();
            validator.Verify("大王@nice", allowInternational: true).IsValid.ShouldBeFalse();
            validator.Verify("大王@@救我.com", allowInternational: true).IsValid.ShouldBeFalse();
        }
    }
}