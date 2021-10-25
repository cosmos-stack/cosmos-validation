using CosmosStack.Validation.Internals.Tokens.ValueTokens;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueNotEqualTokenTests")]
    public class ValueNotEqualTokenTests
    {
        private AunnGood Haha = new AunnGood {Aka = "Haha"};

        private AunnGood Hoho = new AunnGood {Aka = "Hoho"};

        [Fact]
        public void AunnCoo_Object_For_NotEqualToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {OtherInfo = Haha};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken(contract, Hoho, null);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Object_For_NotEqualToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {OtherInfo = Haha};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken(contract, Haha, null);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_ValueType_For_NotEqualToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken(contract, 11, null);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_ValueType_For_NotEqualToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken(contract, 10, null);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Object_For_GenericNotEqualToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {OtherInfo = Haha};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken<object>(contract, Hoho, null);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Object_For_GenericNotEqualToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {OtherInfo = Haha};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken<object>(contract, Haha, null);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_ValueType_For_GenericNotEqualToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken<int>(contract, 11, null);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_ValueType_For_GenericNotEqualToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueNotEqualToken<int>(contract, 10, null);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}