using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueEnumTokenTests")]
    public class ValueEnumTokenTests
    {
        [Fact]
        public void AunnCoo_Enum_For_EnumToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();

            var token = new ValueEnumToken(contract, typeof(AunnEnum));

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Enum_For_EnumToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();

            var token = new ValueEnumToken(contract, typeof(AunnEnum2));

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Enum_For_GenericEnumToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();

            var token = new ValueEnumToken<AunnEnum>(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Enum_For_GenericEnumToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();

            var token = new ValueEnumToken<AunnEnum2>(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}