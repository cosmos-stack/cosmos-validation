﻿using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueNullTokenTests")]
    public class ValueNullTokenTests
    {
        [Fact]
        public void AunnCoo_Null_For_NullToken_And_ShouldBeValid()
        {
            var model = new AunnCoo();

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNullToken(contract);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Null_For_NullToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo
            {
                OtherInfo = new()
            };

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("OtherInfo");
            var contract = member.ExposeContract();

            var token = new ValueNullToken(contract);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}