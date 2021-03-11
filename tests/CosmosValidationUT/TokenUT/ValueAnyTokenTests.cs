using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueAnyTokenTests")]
    public class ValueAnyTokenTests
    {
        [Fact]
        public void AunnCoo_List_For_AnyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAnyToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_AnyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAnyToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_List_For_GenericAnyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAnyToken<List<string>, string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_List_For_GenericAnyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Tags = new List<string> {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Tags");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAnyToken<List<string>, string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Array_For_AnyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAnyToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Array_For_AnyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAnyToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Array_For_GenericAnyToken_And_ShouldBeValid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.StartsWith("N");

            var token = new ValueAnyToken<string[], string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Array_For_GenericAnyToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                Career = new[] {"Nice", "Good", "Hello"}
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Career");
            var contract = member.ExposeContract();
            Func<object, bool> condition = obj => obj is string str && str.EndsWith("N");

            var token = new ValueAnyToken<string[], string>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}