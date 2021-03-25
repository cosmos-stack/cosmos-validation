using System;

namespace Cosmos.Validation.Internals.Rules
{
    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information.
    /// </summary>
    internal class CorrectWaitForMessageValueRuleBuilder : IWaitForMessageValueRuleBuilder
    {
        private readonly CorrectValueRuleBuilder _builder;
        private readonly Func<object, bool> _func;
        private readonly Predicate<object> _predicate;

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder builder, Func<object, bool> func)
        {
            _builder = builder;
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder builder, Predicate<object> predicate)
        {
            _builder = builder;
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IPredicateValueRuleBuilder WithMessage(string message)
        {
            Func<object, CustomVerifyResult> realFunc;

            if (_predicate is null)
            {
                realFunc = o => _func.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};
            }
            else
            {
                realFunc = o => _predicate.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};
            }

            _builder.Must(realFunc);

            return _builder;
        }
    }

    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class CorrectWaitForMessageValueRuleBuilder<T> : IWaitForMessageValueRuleBuilder<T>
    {
        private readonly CorrectValueRuleBuilder<T> _builder;
        private readonly Func<object, bool> _func;
        private readonly Predicate<object> _predicate;

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder<T> builder, Func<object, bool> func)
        {
            _builder = builder;
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder<T> builder, Predicate<object> predicate)
        {
            _builder = builder;
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IPredicateValueRuleBuilder<T> WithMessage(string message)
        {
            Func<object, CustomVerifyResult> realFunc;

            if (_predicate is null)
            {
                realFunc = o => _func.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};
            }
            else
            {
                realFunc = o => _predicate.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};
            }

            _builder.Must(realFunc);

            return _builder;
        }
    }

    /// <summary>
    /// The lower-level interface of ValueRuleBuilder. Used to wait for verification information.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    internal class CorrectWaitForMessageValueRuleBuilder<T, TVal> : IWaitForMessageValueRuleBuilder<T, TVal>
    {
        private readonly CorrectValueRuleBuilder<T, TVal> _builder;
        private readonly Func<TVal, bool> _func;
        private readonly Predicate<TVal> _predicate;

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder<T, TVal> builder, Func<TVal, bool> func)
        {
            _builder = builder;
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public CorrectWaitForMessageValueRuleBuilder(CorrectValueRuleBuilder<T, TVal> builder, Predicate<TVal> predicate)
        {
            _builder = builder;
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IPredicateValueRuleBuilder<T, TVal> WithMessage(string message)
        {
            Func<TVal, CustomVerifyResult> realFunc;

            if (_predicate is null)
            {
                realFunc = o => _func.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};
            }
            else
            {
                realFunc = o => _predicate.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message};
            }

            _builder.Must(realFunc);

            return _builder;
        }
    }
}