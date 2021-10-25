using System;
using System.Collections.Generic;
using CosmosStack.Validation.Internals.Tokens.ValueTokens;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueNoneTokenTests")]
    public class ValueNoneTokenTests
    {
           [Fact]
        public void AunnCoo_List_For_NoneToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"MNice", "MNormal", "MNew"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_NoneToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_List_For_GenericNoneToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"MNice", "MNormal", "MNew"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken<List<string>, string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_GenericNoneToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken<List<string>, string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Array_For_NoneToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"MNice", "MNormal", "MNew"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Array_For_NoneToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Array_For_GenericNoneToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"MNice", "MNormal", "MNew"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken<string[], string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Array_For_GenericNoneToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueNoneToken<string[], string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}