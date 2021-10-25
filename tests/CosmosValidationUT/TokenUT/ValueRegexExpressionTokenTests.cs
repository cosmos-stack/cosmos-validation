using System;
using System.Text.RegularExpressions;
using CosmosStack.Validation.Internals.Tokens.ValueTokens;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.TokenUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.TokenUT
{
    [Trait("TokenUT", "ValueRegexExpressionTokenTests")]
    public class ValueRegexExpressionTokenTests
    {
        [Fact]
        public void AunnCoo_StringExpr_RegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken(contract, @"^(Al)\w+(e)$");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_StringExpr_RegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken(contract, @"^(Al)\w+(e)$");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Regex_RegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken(contract, new Regex(@"^(Al)\w+(e)$"));

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Regex_RegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken(contract, new Regex(@"^(Al)\w+(e)$"));

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_StringExprFunc_RegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<object, string> expression = obj =>
            {
                if (obj is AunnCoo aunnCoo)
                {
                    return aunnCoo.AunnRegexExpression;
                }

                return string.Empty;
            };

            var token = new ValueRegularExpressionToken(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_StringExprFunc_RegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<object, string> expression = obj =>
            {
                if (obj is AunnCoo aunnCoo)
                {
                    return aunnCoo.AunnRegexExpression;
                }

                return string.Empty;
            };

            var token = new ValueRegularExpressionToken(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_RegexFunc_RegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<object, Regex> expression = obj =>
            {
                if (obj is AunnCoo aunnCoo)
                {
                    return new Regex(aunnCoo.AunnRegexExpression);
                }

                return new Regex(string.Empty);
            };

            var token = new ValueRegularExpressionToken(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_RegexFunc_RegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<object, Regex> expression = obj =>
            {
                if (obj is AunnCoo aunnCoo)
                {
                    return new Regex(aunnCoo.AunnRegexExpression);
                }

                return new Regex(string.Empty);
            };

            var token = new ValueRegularExpressionToken(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }


        [Fact]
        public void AunnCoo_StringExpr_GenericRegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, @"^(Al)\w+(e)$");

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_StringExpr_GenericRegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, @"^(Al)\w+(e)$");

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_Regex_GenericRegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, new Regex(@"^(Al)\w+(e)$"));

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_Regex_GenericRegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, new Regex(@"^(Al)\w+(e)$"));

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_StringExprFunc_GenericRegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<AunnCoo, string> expression = obj => obj.AunnRegexExpression;

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_StringExprFunc_GenericRegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<AunnCoo, string> expression = obj => obj.AunnRegexExpression;

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void AunnCoo_RegexFunc_GenericRegexExprToken_ShouldBeValid()
        {
            var model = new AunnCoo {Name = "Alice", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<AunnCoo, Regex> expression = obj => new Regex(obj.AunnRegexExpression);

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeTrue();
            token.Valid(member).IsSuccess.ShouldBeTrue();
        }

        [Fact]
        public void AunnCoo_RegexFunc_GenericRegexExprToken_ShouldBeInvalid()
        {
            var model = new AunnCoo {Name = "AlicE", AunnRegexExpression = @"^(Al)\w+(e)$"};

            var context = VerifiableObjectContractManager.Resolve<AunnCoo>().WithInstance(model);
            var member = context.GetValue("Name");
            var contract = member.ExposeContract();
            Func<AunnCoo, Regex> expression = obj => new Regex(obj.AunnRegexExpression);

            var token = new ValueRegularExpressionToken<AunnCoo>(contract, expression);

            token.Valid(context).IsSuccess.ShouldBeFalse();
            token.Valid(member).IsSuccess.ShouldBeFalse();
        }
    }
}