using System;
using Cosmos.Validation;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueFuncTokenTests")]
    public class ValueFuncTokenTests
    {
        [Fact]
        public void AunnCoo_Func_For_FuncToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();
            Func<object, CustomVerifyResult> condition = obj =>
            {
                if (obj is AunnEnum a && a == AunnEnum.One)
                    return new CustomVerifyResult{VerifyResult = true};
                return new CustomVerifyResult {VerifyResult = false};
            };

            var token = new ValueFuncToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Func_For_FuncToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();
            Func<object, CustomVerifyResult> condition = obj =>
            {
                if (obj is AunnEnum a && a == AunnEnum.Two)
                    return new CustomVerifyResult{VerifyResult = true};
                return new CustomVerifyResult {VerifyResult = false};
            };

            var token = new ValueFuncToken(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
        
        [Fact]
        public void AunnCoo_Func_For_GenericFuncToken_And_ShouldBeValid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();
            Func<AunnEnum, CustomVerifyResult> condition = obj =>
            {
                if (obj is AunnEnum a && a == AunnEnum.One)
                    return new CustomVerifyResult{VerifyResult = true};
                return new CustomVerifyResult {VerifyResult = false};
            };

            var token = new ValueFuncToken<AunnEnum>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Func_For_GenericFuncToken_And_ShouldBeInvalid()
        {
            var model = new AunnCoo {AunnClass = AunnEnum.One};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("AunnClass");
            var contract = member.ExposeContract();
            Func<AunnEnum, CustomVerifyResult> condition = obj =>
            {
                if (obj is AunnEnum a && a == AunnEnum.Two)
                    return new CustomVerifyResult{VerifyResult = true};
                return new CustomVerifyResult {VerifyResult = false};
            };

            var token = new ValueFuncToken<AunnEnum>(contract, condition);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}