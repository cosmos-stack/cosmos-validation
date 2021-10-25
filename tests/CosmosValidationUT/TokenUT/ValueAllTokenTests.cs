using System;
using System.Collections.Generic;
using CosmosStack.Validation.Internals.Tokens.ValueTokens;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueAllTokenTests")]
    public class ValueAllTokenTests
    {
        [Fact]
        public void AunnCoo_List_For_AllToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAllToken(contract, condition);
            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_AllToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAllToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_List_For_GenericAllToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAllToken<List<string>, string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_GenericAllToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAllToken<List<string>, string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Array_For_AllToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAllToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Array_For_AllToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAllToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Array_For_GenericAllToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAllToken<string[], string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Array_For_GenericAllToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Normal", "New"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAllToken<string[], string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}