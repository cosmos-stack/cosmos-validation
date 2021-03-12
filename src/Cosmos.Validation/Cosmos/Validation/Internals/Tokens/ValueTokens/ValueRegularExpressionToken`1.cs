using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRegularExpressionToken<T> : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueRegularExpressionToken";
        readonly Func<T, Regex> _regexFunc;

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

        public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<T, string> expressionFunc) : base(contract)
        {
            _regexFunc = x => CreateRegex(expressionFunc(x));
        }

        public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<T, Regex> regexFunc) : base(contract)
        {
            _regexFunc = regexFunc;
        }

        public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<T, string> expressionFunc, RegexOptions options) : base(contract)
        {
            _regexFunc = x => CreateRegex(expressionFunc(x), options);
        }

        public override CorrectValueOps Ops => CorrectValueOps.RegularExpression_T1;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (context is null)
            {
                UpdateVal(verifyVal, value);
            }
            else
            {
                var regex = _regexFunc((T) context.Instance);

                if (regex is null || value is null || !regex.IsMatch((string) value))
                {
                    UpdateVal(verifyVal, value, regex?.ToString());
                }
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (context is null)
            {
                UpdateVal(verifyVal, value);
            }
            else
            {
                var regex = _regexFunc(context.GetParentInstance<T>());

                if (regex is null || value is null || !regex.IsMatch((string) value))
                {
                    UpdateVal(verifyVal, value, regex?.ToString());
                }
            }

            return verifyVal;
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
            return new(expression, options, TimeSpan.FromSeconds(2.0));
        }

        public override string ToString() => NAME;
    }
}