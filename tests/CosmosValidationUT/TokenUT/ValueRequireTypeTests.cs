using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueRequireTypeTests")]
    public class ValueRequireTypeTests
    {
        [Fact]
        public void AunnCoo_RequireTypesToken_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypesToken(contract1, typeof(AunnEnum), typeof(AunnEnum2));
            var token2 = new ValueRequiredTypesToken(contract2, typeof(AunnEnum), typeof(AunnEnum2), typeof(string));

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_RequireTypesToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypesToken(contract1, typeof(string), typeof(AunnEnum2));
            var token2 = new ValueRequiredTypesToken(contract2, typeof(AunnEnum), typeof(AunnEnum2));

            token1.Valid(context).IsSuccess.ShouldBeFalse();
            token1.Valid(member1).IsSuccess.ShouldBeFalse();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member2).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_GenericRequireTypesToken_1_N_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypesToken<AunnEnum, AunnEnum2>(contract1);
            var token2 = new ValueRequiredTypesToken<AunnEnum, AunnEnum2, string>(contract2);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_GenericRequireTypesToken_1_N_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypesToken<string, AunnEnum2>(contract1);
            var token2 = new ValueRequiredTypesToken<AunnEnum, AunnEnum2>(contract2);

            token1.Valid(context).IsSuccess.ShouldBeFalse();
            token1.Valid(member1).IsSuccess.ShouldBeFalse();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member2).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_RequireTypeToken_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypeToken(contract1, typeof(AunnEnum));
            var token2 = new ValueRequiredTypeToken(contract2, typeof(string));

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_RequireTypeToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypeToken(contract1, typeof(AunnEnum2));
            var token2 = new ValueRequiredTypeToken(contract2, typeof(AunnEnum));

            token1.Valid(context).IsSuccess.ShouldBeFalse();
            token1.Valid(member1).IsSuccess.ShouldBeFalse();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member2).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_GenericRequireTypeToken_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypeToken<AunnEnum>(contract1);
            var token2 = new ValueRequiredTypeToken<string>(contract2);

            token1.Valid(context).IsSuccess.ShouldBeTrue();
            token1.Valid(member1).IsSuccess.ShouldBeTrue();

            token2.Valid(context).IsSuccess.ShouldBeTrue();
            token2.Valid(member2).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_GenericRequireTypeToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One, AunnType = typeof(string)};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member1 = context.GetValue("AunnClass");
            var member2 = context.GetValue("AunnType");
            var contract1 = member1.ExposeContract();
            var contract2 = member2.ExposeContract();

            var token1 = new ValueRequiredTypeToken<AunnEnum2>(contract1);
            var token2 = new ValueRequiredTypeToken<AunnEnum>(contract2);

            token1.Valid(context).IsSuccess.ShouldBeFalse();
            token1.Valid(member1).IsSuccess.ShouldBeFalse();

            token2.Valid(context).IsSuccess.ShouldBeFalse();
            token2.Valid(member2).IsSuccess.ShouldBeFalse();
        }
    }
}