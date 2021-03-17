using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueGreaterThanOrEqualTokenTests")]
    public class ValueGreaterThanOrEqualTokenTests
    {
        [Fact]
        public void AunnCoo_ValueType_For_GreaterThanOrEqualToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueGreaterThanOrEqualToken(contract, 10);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        } 
        
        [Fact]
        public void AunnCoo_ValueType_For_GreaterThanOrEqualToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueGreaterThanOrEqualToken(contract, 11);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}