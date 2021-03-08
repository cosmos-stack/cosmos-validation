using System;
using System.Linq.Expressions;
using System.Reflection;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

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

        #region AndForMember

        public IValueFluentValidationRegistrar AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(fieldInfo, mode);
        }

        #endregion

        #region AndForType

        public IFluentValidationRegistrar AndForType(Type type)
        {
            return WithMessage(string.Empty).AndForType(type);
        }

        public IFluentValidationRegistrar AndForType(Type type, string name)
        {
            return WithMessage(string.Empty).AndForType(type, name);
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>()
        {
            return WithMessage(string.Empty).AndForType<TType>();
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>(string name)
        {
            return WithMessage(string.Empty).AndForType<TType>(name);
        }

        #endregion

        #region AndForStrategy

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy, TType>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy, TType>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, name, mode);
        }

        #endregion

        #region AndForCustomValidator

        public IFluentValidationRegistrar AndForCustomValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            return WithMessage(string.Empty).AndForCustomValidator<TValidator>();
        }

        public IFluentValidationRegistrar AndForCustomValidator<TValidator, TType>() where TValidator : CustomValidator<TType>, new()
        {
            return WithMessage(string.Empty).AndForCustomValidator<TValidator, TType>();
        }

        public IFluentValidationRegistrar AndForCustomValidator(CustomValidator validator)
        {
            return WithMessage(string.Empty).AndForCustomValidator(validator);
        }

        public IFluentValidationRegistrar AndForCustomValidator<TType>(CustomValidator<TType> validator)
        {
            return WithMessage(string.Empty).AndForCustomValidator(validator);
        }

        #endregion

        #region Build

        public void Build()
        {
            WithMessage(string.Empty).Build();
        }

        public ValidationHandler TempBuild()
        {
            return WithMessage(string.Empty).TempBuild();
        }

        public ValidationHandler TempBuild(ValidationOptions options)
        {
            return WithMessage(string.Empty).TempBuild(options);
        }

        public ValidationHandler TempBuild(Action<ValidationOptions> optionsAct)
        {
            return WithMessage(string.Empty).TempBuild(optionsAct);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver)
        {
            return WithMessage(string.Empty).TempBuild(objectResolver);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            return WithMessage(string.Empty).TempBuild(objectResolver, options);
        }

        #endregion

        #region TakeEffect

        public void TakeEffect()
        {
            WithMessage(string.Empty).TakeEffect();
        }

        #endregion
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

        #region AndForMember

        public IValueFluentValidationRegistrar<T> AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(fieldInfo, mode);
        }

        public IValueFluentValidationRegistrar<T, TVal2> AndForMember<TVal2>(Expression<Func<T, TVal2>> expression, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(expression, mode);
        }

        #endregion

        #region AndForType

        public IFluentValidationRegistrar AndForType(Type type)
        {
            return WithMessage(string.Empty).AndForType(type);
        }

        public IFluentValidationRegistrar AndForType(Type type, string name)
        {
            return WithMessage(string.Empty).AndForType(type, name);
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>()
        {
            return WithMessage(string.Empty).AndForType<TType>();
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>(string name)
        {
            return WithMessage(string.Empty).AndForType<TType>(name);
        }

        #endregion

        #region AndForStrategy

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy, TType>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy, TType>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, name, mode);
        }

        #endregion

        #region AndForCustomValidator

        public IFluentValidationRegistrar AndForCustomValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            return WithMessage(string.Empty).AndForCustomValidator<TValidator>();
        }

        public IFluentValidationRegistrar AndForCustomValidator<TValidator, TType>() where TValidator : CustomValidator<TType>, new()
        {
            return WithMessage(string.Empty).AndForCustomValidator<TValidator, TType>();
        }

        public IFluentValidationRegistrar AndForCustomValidator(CustomValidator validator)
        {
            return WithMessage(string.Empty).AndForCustomValidator(validator);
        }

        public IFluentValidationRegistrar AndForCustomValidator<TType>(CustomValidator<TType> validator)
        {
            return WithMessage(string.Empty).AndForCustomValidator(validator);
        }

        #endregion

        #region Build

        public void Build()
        {
            WithMessage(string.Empty).Build();
        }

        public ValidationHandler TempBuild()
        {
            return WithMessage(string.Empty).TempBuild();
        }

        public ValidationHandler TempBuild(ValidationOptions options)
        {
            return WithMessage(string.Empty).TempBuild(options);
        }

        public ValidationHandler TempBuild(Action<ValidationOptions> optionsAct)
        {
            return WithMessage(string.Empty).TempBuild(optionsAct);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver)
        {
            return WithMessage(string.Empty).TempBuild(objectResolver);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            return WithMessage(string.Empty).TempBuild(objectResolver, options);
        }

        #endregion

        #region TakeEffect

        public void TakeEffect()
        {
            WithMessage(string.Empty).TakeEffect();
        }

        #endregion
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

        #region AndForMember

        public IValueFluentValidationRegistrar<T> AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(fieldInfo, mode);
        }

        public IValueFluentValidationRegistrar<T, TVal2> AndForMember<TVal2>(Expression<Func<T, TVal2>> expression, ValueRuleMode mode = ValueRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(expression, mode);
        }

        #endregion

        #region AndForType

        public IFluentValidationRegistrar AndForType(Type type)
        {
            return WithMessage(string.Empty).AndForType(type);
        }

        public IFluentValidationRegistrar AndForType(Type type, string name)
        {
            return WithMessage(string.Empty).AndForType(type, name);
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>()
        {
            return WithMessage(string.Empty).AndForType<TType>();
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>(string name)
        {
            return WithMessage(string.Empty).AndForType<TType>(name);
        }

        #endregion

        #region AndForStrategy

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy, TType>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            return WithMessage(string.Empty).AndForStrategy<TStrategy, TType>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            return WithMessage(string.Empty).AndForStrategy(strategy, name, mode);
        }

        #endregion

        #region AndForCustomValidator

        public IFluentValidationRegistrar AndForCustomValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            return WithMessage(string.Empty).AndForCustomValidator<TValidator>();
        }

        public IFluentValidationRegistrar AndForCustomValidator<TValidator, TType>() where TValidator : CustomValidator<TType>, new()
        {
            return WithMessage(string.Empty).AndForCustomValidator<TValidator, TType>();
        }

        public IFluentValidationRegistrar AndForCustomValidator(CustomValidator validator)
        {
            return WithMessage(string.Empty).AndForCustomValidator(validator);
        }

        public IFluentValidationRegistrar AndForCustomValidator<TType>(CustomValidator<TType> validator)
        {
            return WithMessage(string.Empty).AndForCustomValidator(validator);
        }

        #endregion

        #region Build

        public void Build()
        {
            WithMessage(string.Empty).Build();
        }

        public ValidationHandler TempBuild()
        {
            return WithMessage(string.Empty).TempBuild();
        }

        public ValidationHandler TempBuild(ValidationOptions options)
        {
            return WithMessage(string.Empty).TempBuild(options);
        }

        public ValidationHandler TempBuild(Action<ValidationOptions> optionsAct)
        {
            return WithMessage(string.Empty).TempBuild(optionsAct);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver)
        {
            return WithMessage(string.Empty).TempBuild(objectResolver);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            return WithMessage(string.Empty).TempBuild(objectResolver, options);
        }

        #endregion

        #region TakeEffect

        public void TakeEffect()
        {
            WithMessage(string.Empty).TakeEffect();
        }

        #endregion
    }
}