using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueLengthLimitedTokenTests")]
    public class ValueLengthLimitedTokenTests
    {
        [Fact]
        public void AunnCoo_String_LimitedToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Hello"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueLengthLimitedToken(contract, 4, 6);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_String_LimitedToken_And_ShouldBeIsvalid()
        {
            var model = new AunnCoo {Name = "Hello"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueLengthLimitedToken(contract, 6, 7);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_String_MinLimitedToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Hello"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueMinLengthLimitedToken(contract, 4);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_String_MinLimitedToken_And_ShouldBeIsvalid()
        {
            var model = new AunnCoo {Name = "Hello"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueMinLengthLimitedToken(contract, 6);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_String_MaxLimitedToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Hello"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueMaxLengthLimitedToken(contract, 6);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_String_MaxLimitedToken_And_ShouldBeIsvalid()
        {
            var model = new AunnCoo {Name = "Hello"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueMaxLengthLimitedToken(contract, 4);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}