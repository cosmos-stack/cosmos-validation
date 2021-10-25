using CosmosStack.Validation.Internals.Tokens.ValueTokens;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueNotNullTokenTests")]
    public class ValueNotNullTokenTests
    {
        [Fact]
        public void AunnCoo_Null_For_NotNullToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                OtherInfo = new()
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotNullToken(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Null_For_NotNullToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo();

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotNullToken(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}