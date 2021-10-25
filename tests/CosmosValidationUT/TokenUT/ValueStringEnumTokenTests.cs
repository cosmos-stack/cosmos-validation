using CosmosStack.Validation.Internals.Tokens.ValueTokens;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueStringEnumTokenTests")]
    public class ValueStringEnumTokenTests
    {
        [Fact]
        public void AunnCoo_String_StringEnumToken_Test()
        {
            var model = new AunnCoo {Name = "One"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token1 = new ValueStringEnumToken(contract, typeof(AunnEnum), false);
            var token2 = new ValueStringEnumToken(contract, typeof(AunnEnum), true);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member).IsSuccess.ShouldBeTrue();

            model.Name = "one";
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Name");

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_String_GenericStringEnumToken_Test()
        {
            var model = new AunnCoo {Name = "One"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token1 = new ValueStringEnumToken<AunnEnum>(contract, false);
            var token2 = new ValueStringEnumToken<AunnEnum>(contract, true);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member).IsSuccess.ShouldBeTrue();

            model.Name = "one";
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Name");

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}