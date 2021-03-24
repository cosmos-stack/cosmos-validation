using System;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueRequireOneTests")]
    public class ValueRequireOneTests
    {
        [Fact]
        public void AunnCoo_RequireStringToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alex", OtherInfo = "Lewis"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("Name");
            var member2 = context.GetValue("OtherInfo");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredStringToken(contract1);
            var token2 = new ValueRequiredStringToken(contract2);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_RequireNumericToken_ShouldBeValid()
        {
            var model = new AunnCoo {Age = 10, OtherInfo = 20};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("Age");
            var member2 = context.GetValue("OtherInfo");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredNumericToken(contract1);
            var token2 = new ValueRequiredNumericToken(contract2);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_RequireBooleanToken_ShouldBeValid()
        {
            var model = new AunnCoo {IsThisOk = true, IsThatOk = true, OtherInfo = true};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("IsThisOk");
            var member2 = context.GetValue("IsThatOk");
            var member3 = context.GetValue("OtherInfo");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();
            var contract3 = member3.ExposeContract();

            var token1 = new ValueRequiredBooleanToken(contract1);
            var token2 = new ValueRequiredBooleanToken(contract2);
            var token3 = new ValueRequiredBooleanToken(contract3);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();

            token3.Valid(context).IsSuccess.ShouldBeTrue();
            token3.Valid(member3).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_RequireGuidToken_ShouldBeValid()
        {
            var model = new AunnCoo {ThisGuid = Guid.NewGuid(), ThatGuid = Guid.NewGuid(), OtherInfo = Guid.NewGuid()};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("ThisGuid");
            var member2 = context.GetValue("ThatGuid");
            var member3 = context.GetValue("OtherInfo");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();
            var contract3 = member3.ExposeContract();

            var token1 = new ValueRequiredGuidToken(contract1);
            var token2 = new ValueRequiredGuidToken(contract2);
            var token3 = new ValueRequiredGuidToken(contract3);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();

            token3.Valid(context).IsSuccess.ShouldBeTrue();
            token3.Valid(member3).IsSuccess.ShouldBeTrue();
        }
    }
}