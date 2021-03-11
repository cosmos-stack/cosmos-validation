using System.Collections.Generic;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueNotEmptyTokenTests")]
    public class ValueNotEmptyTokenTests
    {
        [Fact]
        public void AunnCoo_Null_For_NotEmptyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo();

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Null_For_NotEmptyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                OtherInfo = new()
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_String_For_NotEmptyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo();

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_String_For_NotEmptyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "N"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_NotEmptyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Tags = new()};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_List_For_NotEmptyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Tags = new List<string> {"Nice", "Good", "Hello"}};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Array_For_NotEmptyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {Career = new string[0]};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Array_For_NotEmptyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Career = new string[1] {"Nice"}};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_DefaultValue_For_NotEmptyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo();

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_DefaultValue_For_NotEmptyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {Age = 10};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Age");
            var contract = member.ExposeContract();

            var token = new ValueNotEmptyToken(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }
    }
}