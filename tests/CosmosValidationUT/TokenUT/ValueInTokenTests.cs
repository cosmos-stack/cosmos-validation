using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueInTokenTests")]
    public class ValueInTokenTests
    {
        [Fact]
        public void AunnCoo_List_For_InToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();

            var token = new ValueInToken(contract, new List<object> {"Nice", "Duo", "Long"});

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_InToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();

            var token = new ValueInToken(contract, new List<object> {"Bad", "Duo", "Long"});

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_One_For_InToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Name = "N"
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueInToken(contract, new List<object> {"Nice", "Duo", "Long", "N"});

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_One_For_InToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Name = "N"
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueInToken(contract, new List<object> {"Bad", "Duo", "Long"});

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_List_For_GenericInToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();

            var token = new ValueInToken<List<string>, string>(contract, new List<string> {"Nice", "Duo", "Long"});

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_GenericInToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();

            var token = new ValueInToken<List<string>, string>(contract, new List<string> {"Bad", "Duo", "Long"});

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_One_For_GenericInToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Name = "N"
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueInToken<string>(contract, new List<string> {"Nice", "Duo", "Long", "N"});

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_One_For_GenericInToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Name = "N"
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueInToken<string>(contract, new List<string> {"Bad", "Duo", "Long"});

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}