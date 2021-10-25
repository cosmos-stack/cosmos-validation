using CosmosStack.Validation.Internals.Tokens.ValueTokens;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueScalePrecisionTokenTests")]
    public class ValueScalePrecisionTokenTests
    {
        [Fact]
        public void AunnCoo_Discount_ScalePrecisionToken_ShouldBeValid()
        {
            var model = new AunnCoo {Discount = 12.34M};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Discount");
            var contract = member.ExposeContract();

            var token = new ValueScalePrecisionToken(contract, 2, 4);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();

            model = new AunnCoo {Discount = 2.34M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();

            model = new AunnCoo {Discount = -2.34M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();

            model = new AunnCoo {Discount = 0.34M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }
        
        [Fact]
        public void AunnCoo_Discount_Nullable_ScalePrecisionToken_ShouldBeValid()
        {
            var model = new AunnCoo {NullableDiscount = 12.34M};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("NullableDiscount");
            var contract = member.ExposeContract();

            var token = new ValueScalePrecisionToken(contract, 2, 4);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();

            model = new AunnCoo {NullableDiscount = 2.34M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();

            model = new AunnCoo {NullableDiscount = -2.34M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();

            model = new AunnCoo {NullableDiscount = 0.34M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }
        
        [Fact]
        public void AunnCoo_Discount_ScalePrecisionToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Discount = 123.456778M};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Discount");
            var contract = member.ExposeContract();

            var token = new ValueScalePrecisionToken(contract, 2, 4);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {Discount = 12.3414M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {Discount = 12.344M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {Discount = 1.344M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {Discount = 156.3M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {Discount = 65.430M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("Discount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
        
        [Fact]
        public void AunnCoo_Discount_Nullable_ScalePrecisionToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {NullableDiscount = 123.456778M};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("NullableDiscount");
            var contract = member.ExposeContract();

            var token = new ValueScalePrecisionToken(contract, 2, 4);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {NullableDiscount = 12.3414M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {NullableDiscount = 12.344M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {NullableDiscount = 1.344M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {NullableDiscount = 156.3M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();

            model = new AunnCoo {NullableDiscount = 65.430M};
            context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            member = context.GetValue("NullableDiscount");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
        
    }
}