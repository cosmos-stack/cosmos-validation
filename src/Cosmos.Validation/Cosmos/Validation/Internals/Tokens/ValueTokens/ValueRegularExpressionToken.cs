using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRegularExpressionToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRegularExpressionToken";
        readonly Func<object, Regex> _regexFunc;

        public ValueRegularExpressionToken(VerifiableMemberContract contract, string expression) : base(contract)
        {
            _regexFunc = x => CreateRegex(expression);
        }

        public ValueRegularExpressionToken(VerifiableMemberContract contract, Regex regex) : base(contract)
        {
            _regexFunc = x => regex;
        }

        public ValueRegularExpressionToken(VerifiableMemberContract contract, string expression, RegexOptions options) : base(contract)
        {
            _regexFunc = x => CreateRegex(expression, options);
        }

        // public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<object, string> expressionFunc) : base(contract)
        // {
        //     _regexFunc = x => CreateRegex(expressionFunc(x));
        // }
        //
        // public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<object, Regex> regexFunc) : base(contract)
        // {
        //     _regexFunc = regexFunc;
        // }
        //
        // public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<object, string> expressionFunc, RegexOptions options) : base(contract)
        // {
        //     _regexFunc = x => CreateRegex(expressionFunc(x), options);
        // }
        //
        // public ValueRegularExpressionToken(VerifiableMemberContract contract, Expression<Func<object, string>> expression) : base(contract)
        // {
        //     var expressionFunc = expression.Compile();
        //     _regexFunc = x => CreateRegex(expressionFunc(x));
        // }
        //
        // public ValueRegularExpressionToken(VerifiableMemberContract contract, Expression<Func<object, Regex>> expression) : base(contract)
        // {
        //     var regexFunc = expression.Compile();
        //     _regexFunc = regexFunc;
        // }
        //
        // public ValueRegularExpressionToken(VerifiableMemberContract contract, Expression<Func<object, string>> expression, RegexOptions options) : base(contract)
        // {
        //     var expressionFunc = expression.Compile();
        //     _regexFunc = x => CreateRegex(expressionFunc(x), options);
        // }

        public override CorrectValueOps Ops => CorrectValueOps.RegularExpression;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var regex = _regexFunc(value);

            if (regex is null || value is null || !regex.IsMatch((string) value))
            {
                UpdateVal(val, value, regex?.ToString());
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string expression = null)
        {
            var message = "The regular expression match failed.";
            if (!string.IsNullOrWhiteSpace(expression))
                message += $" The current expression is: {expression}.";

            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message);
        }

        private static Regex CreateRegex(string expression, RegexOptions options = RegexOptions.None)
        {
            return new Regex(expression, options, TimeSpan.FromSeconds(2.0));
        }

        public override string ToString() => NAME;
    }
}