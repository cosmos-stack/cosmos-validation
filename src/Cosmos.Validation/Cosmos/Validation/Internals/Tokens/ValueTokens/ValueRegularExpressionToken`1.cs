using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Regular expression token, a generic version
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ValueRegularExpressionToken<T> : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueRegularExpressionToken";
        readonly Func<T, Regex> _regexFunc;

        /// <inheritdoc />
        public ValueRegularExpressionToken(VerifiableMemberContract contract, string expression) : base(contract)
        {
            _regexFunc = x => CreateRegex(expression);
        }

        /// <inheritdoc />
        public ValueRegularExpressionToken(VerifiableMemberContract contract, Regex regex) : base(contract)
        {
            _regexFunc = x => regex;
        }

        /// <inheritdoc />
        public ValueRegularExpressionToken(VerifiableMemberContract contract, string expression, RegexOptions options) : base(contract)
        {
            _regexFunc = x => CreateRegex(expression, options);
        }

        /// <inheritdoc />
        public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<T, string> expressionFunc) : base(contract)
        {
            _regexFunc = x => CreateRegex(expressionFunc(x));
        }

        /// <inheritdoc />
        public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<T, Regex> regexFunc) : base(contract)
        {
            _regexFunc = regexFunc;
        }

        /// <inheritdoc />
        public ValueRegularExpressionToken(VerifiableMemberContract contract, Func<T, string> expressionFunc, RegexOptions options) : base(contract)
        {
            _regexFunc = x => CreateRegex(expressionFunc(x), options);
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => NAME;

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public override bool MutuallyExclusive => false;

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        /// <summary>
        /// Verification for VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var value = GetValueFrom(context);
            
            if (!IsActivate(context.Instance, value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

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

        /// <summary>
        /// Verification for VerifiableMemberContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var value = GetValueFrom(context);
            
            if(!IsActivate(value))
                return CorrectVerifyVal.Ignore;

            var verifyVal = CreateVerifyVal();

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
    }
}