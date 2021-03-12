using Cosmos.Validation;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueRangeTokenTests")]
    public class ValueRangeTokenTests
    {
        [Fact]
        public void AunnCoo_Int_For_RangeToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token1 = new ValueRangeToken(contract, 9, 11, RangeOptions.OpenInterval);
            var token2 = new ValueRangeToken(contract, 9, 10, RangeOptions.CloseInterval);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Int_For_RangeToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token1 = new ValueRangeToken(contract, 10, 11, RangeOptions.OpenInterval);
            var token2 = new ValueRangeToken(contract, 9, 9, RangeOptions.CloseInterval);

            token1.Valid(context).IsSuccess.ShouldBeFalse();
            token1.Valid(member).IsSuccess.ShouldBeFalse();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Char_For_RangeToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {C = 'b'};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("C");
            var contract = member.ExposeContract();

            var token1 = new ValueRangeToken(contract, 'a', 'c', RangeOptions.OpenInterval);
            var token2 = new ValueRangeToken(contract, 'a', 'b', RangeOptions.CloseInterval);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Char_For_RangeToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {C = 'b'};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("C");
            var contract = member.ExposeContract();

            var token1 = new ValueRangeToken(contract, 'b', 'c', RangeOptions.OpenInterval);
            var token2 = new ValueRangeToken(contract, 'a', 'a', RangeOptions.CloseInterval);

            token1.Valid(context).IsSuccess.ShouldBeFalse();
            token1.Valid(member).IsSuccess.ShouldBeFalse();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}