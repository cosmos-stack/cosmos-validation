using System;

namespace Cosmos.Validation.Registrars
{
    internal class ValidationRegistrarWithMessage : IWaitForMessageValidationRegistrar
    {
        private readonly ValueValidationRegistrar _registrar;
        private readonly Func<object, bool> _func;
        private readonly Predicate<object> _predicate;

        public ValidationRegistrarWithMessage(ValueValidationRegistrar registrar, Func<object, bool> func)
        {
            _registrar = registrar;
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public ValidationRegistrarWithMessage(ValueValidationRegistrar registrar, Predicate<object> predicate)
        {
            _registrar = registrar;
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IValueFluentValidationRegistrar WithMessage(string message)
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

            _registrar.Must(realFunc);

            return _registrar;
        }
    }

    internal class ValidationRegistrarWithMessage<T> : IWaitForMessageValidationRegistrar<T>
    {
        private readonly ValueValidationRegistrar<T> _registrar;
        private readonly Func<object, bool> _func;
        private readonly Predicate<object> _predicate;

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T> registrar, Func<object, bool> func)
        {
            _registrar = registrar;
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T> registrar, Predicate<object> predicate)
        {
            _registrar = registrar;
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IValueFluentValidationRegistrar<T> WithMessage(string message)
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

            _registrar.Must(realFunc);

            return _registrar;
        }
    }

    internal class ValidationRegistrarWithMessage<T, TVal> : IWaitForMessageValidationRegistrar<T, TVal>
    {
        private readonly ValueValidationRegistrar<T, TVal> _registrar;
        private readonly Func<TVal, bool> _func;
        private readonly Predicate<TVal> _predicate;

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func)
        {
            _registrar = registrar;
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T, TVal> registrar, Predicate<TVal> predicate)
        {
            _registrar = registrar;
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IValueFluentValidationRegistrar<T, TVal> WithMessage(string message)
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

            _registrar.Must(realFunc);

            return _registrar;
        }
    }
}