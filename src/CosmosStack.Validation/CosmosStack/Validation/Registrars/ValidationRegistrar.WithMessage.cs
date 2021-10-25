using System;
using System.Linq.Expressions;
using System.Reflection;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Strategies;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information.
    /// </summary>
    internal class ValidationRegistrarWithMessage : IWaitForMessageValidationRegistrar
    {
        private readonly IValidationRegistrar _rootRegistrar;
        private readonly ValueValidationRegistrar _registrar;
        private readonly Func<object, bool> _func;
        private readonly Predicate<object> _predicate;
        private string _name;

        public ValidationRegistrarWithMessage(ValueValidationRegistrar registrar, IValidationRegistrar rootRegistrar, Func<object, bool> func)
        {
            _registrar = registrar;
            _rootRegistrar = rootRegistrar ?? throw new ArgumentNullException(nameof(rootRegistrar));
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public ValidationRegistrarWithMessage(ValueValidationRegistrar registrar, IValidationRegistrar rootRegistrar, Predicate<object> predicate)
        {
            _registrar = registrar;
            _rootRegistrar = rootRegistrar ?? throw new ArgumentNullException(nameof(rootRegistrar));
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        public IWaitForMessageValidationRegistrar WithName(string operationName)
        {
            _name = operationName;
            return this;
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IPredicateValidationRegistrar WithMessage(string message)
        {
            Func<object, CustomVerifyResult> realFunc;

            if (_predicate is null)
            {
                realFunc = o => _func.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true, OperationName = _name}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message, OperationName = _name};
            }
            else
            {
                realFunc = o => _predicate.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true, OperationName = _name}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message, OperationName = _name};
            }

            _registrar.Must(realFunc);

            return _registrar;
        }

        #region Condition

        public IValueFluentValidationRegistrar And()
        {
            return WithMessage(string.Empty).And();
        }

        public IValueFluentValidationRegistrar Or()
        {
            return WithMessage(string.Empty).Or();
        }

        #endregion

        #region Activation Conditions

        public IValueFluentValidationRegistrar When(Func<object, bool> condition)
        {
            return WithMessage(string.Empty).When(condition);
        }

        public IValueFluentValidationRegistrar When(Func<object, object, bool> condition)
        {
            return WithMessage(string.Empty).When(condition);
        }

        public IValueFluentValidationRegistrar Unless(Func<object, bool> condition)
        {
            return WithMessage(string.Empty).Unless(condition);
        }

        public IValueFluentValidationRegistrar Unless(Func<object, object, bool> condition)
        {
            return WithMessage(string.Empty).Unless(condition);
        }

        #endregion

        #region AndForMember

        public IValueFluentValidationRegistrar AndForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar AndForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar AndForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append)
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

        public IValidationRegistrar TakeEffectAndBack()
        {
            TakeEffect();
            return _rootRegistrar;
        }

        #endregion

        #region ExposeVerifyRulePackage

        public VerifyRulePackage ExposeRulePackage()
        {
            return WithMessage(string.Empty).ExposeRulePackage();
        }

        public VerifyRulePackage ExposeUnregisteredRulePackage()
        {
            return WithMessage(string.Empty).ExposeUnregisteredRulePackage();
        }

        #endregion
    }

    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ValidationRegistrarWithMessage<T> : IWaitForMessageValidationRegistrar<T>
    {
        private readonly IValidationRegistrar _rootRegistrar;
        private readonly ValueValidationRegistrar<T> _registrar;
        private readonly Func<object, bool> _func;
        private readonly Predicate<object> _predicate;
        private string _name;

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T> registrar, IValidationRegistrar rootRegistrar, Func<object, bool> func)
        {
            _registrar = registrar;
            _rootRegistrar = rootRegistrar ?? throw new ArgumentNullException(nameof(rootRegistrar));
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T> registrar, IValidationRegistrar rootRegistrar, Predicate<object> predicate)
        {
            _registrar = registrar;
            _rootRegistrar = rootRegistrar ?? throw new ArgumentNullException(nameof(rootRegistrar));
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        public IWaitForMessageValidationRegistrar<T> WithName(string operationName)
        {
            _name = operationName;
            return this;
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IPredicateValidationRegistrar<T> WithMessage(string message)
        {
            Func<object, CustomVerifyResult> realFunc;

            if (_predicate is null)
            {
                realFunc = o => _func.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true, OperationName = _name}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message, OperationName = _name};
            }
            else
            {
                realFunc = o => _predicate.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true, OperationName = _name}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message, OperationName = _name};
            }

            _registrar.Must(realFunc);

            return _registrar;
        }

        #region Condition

        public IValueFluentValidationRegistrar<T> And()
        {
            return WithMessage(string.Empty).And();
        }

        public IValueFluentValidationRegistrar<T> Or()
        {
            return WithMessage(string.Empty).Or();
        }

        #endregion

        #region Activation Conditions

        public IValueFluentValidationRegistrar<T> When(Func<object, bool> condition)
        {
            return WithMessage(string.Empty).When(condition);
        }

        public IValueFluentValidationRegistrar<T> When(Func<T, object, bool> condition)
        {
            return WithMessage(string.Empty).When(condition);
        }

        public IValueFluentValidationRegistrar<T> Unless(Func<object, bool> condition)
        {
            return WithMessage(string.Empty).Unless(condition);
        }

        public IValueFluentValidationRegistrar<T> Unless(Func<T, object, bool> condition)
        {
            return WithMessage(string.Empty).Unless(condition);
        }

        #endregion

        #region AndForMember

        public IValueFluentValidationRegistrar<T> AndForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(fieldInfo, mode);
        }

        public IValueFluentValidationRegistrar<T, TVal2> AndForMember<TVal2>(Expression<Func<T, TVal2>> expression, VerifyRuleMode mode = VerifyRuleMode.Append)
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

        public IValidationRegistrar TakeEffectAndBack()
        {
            TakeEffect();
            return _rootRegistrar;
        }

        #endregion

        #region ExposeVerifyRulePackage

        public VerifyRulePackage ExposeRulePackage()
        {
            return WithMessage(string.Empty).ExposeRulePackage();
        }

        public VerifyRulePackage ExposeUnregisteredRulePackage()
        {
            return WithMessage(string.Empty).ExposeUnregisteredRulePackage();
        }

        #endregion
    }

    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    internal class ValidationRegistrarWithMessage<T, TVal> : IWaitForMessageValidationRegistrar<T, TVal>
    {
        private readonly IValidationRegistrar _rootRegistrar;
        private readonly ValueValidationRegistrar<T, TVal> _registrar;
        private readonly Func<TVal, bool> _func;
        private readonly Predicate<TVal> _predicate;
        private string _name;

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T, TVal> registrar, IValidationRegistrar rootRegistrar, Func<TVal, bool> func)
        {
            _registrar = registrar;
            _rootRegistrar = rootRegistrar ?? throw new ArgumentNullException(nameof(rootRegistrar));
            _func = func ?? throw new ArgumentNullException(nameof(func));
            _predicate = null;
        }

        public ValidationRegistrarWithMessage(ValueValidationRegistrar<T, TVal> registrar, IValidationRegistrar rootRegistrar, Predicate<TVal> predicate)
        {
            _registrar = registrar;
            _rootRegistrar = rootRegistrar ?? throw new ArgumentNullException(nameof(rootRegistrar));
            _func = null;
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        public IWaitForMessageValidationRegistrar<T, TVal> WithName(string operationName)
        {
            _name = operationName;
            return this;
        }

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IPredicateValidationRegistrar<T, TVal> WithMessage(string message)
        {
            Func<TVal, CustomVerifyResult> realFunc;

            if (_predicate is null)
            {
                realFunc = o => _func.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true, OperationName = _name}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message, OperationName = _name};
            }
            else
            {
                realFunc = o => _predicate.Invoke(o)
                    ? new CustomVerifyResult {VerifyResult = true, OperationName = _name}
                    : new CustomVerifyResult {VerifyResult = false, ErrorMessage = message, OperationName = _name};
            }

            _registrar.Must(realFunc);

            return _registrar;
        }

        #region Condition

        public IValueFluentValidationRegistrar<T, TVal> And()
        {
            return WithMessage(string.Empty).And();
        }

        public IValueFluentValidationRegistrar<T, TVal> Or()
        {
            return WithMessage(string.Empty).Or();
        }

        #endregion

        #region Activation Conditions

        public IValueFluentValidationRegistrar<T, TVal> When(Func<TVal, bool> condition)
        {
            return WithMessage(string.Empty).When(condition);
        }

        public IValueFluentValidationRegistrar<T, TVal> When(Func<T, TVal, bool> condition)
        {
            return WithMessage(string.Empty).When(condition);
        }

        public IValueFluentValidationRegistrar<T, TVal> Unless(Func<TVal, bool> condition)
        {
            return WithMessage(string.Empty).Unless(condition);
        }

        public IValueFluentValidationRegistrar<T, TVal> Unless(Func<T, TVal, bool> condition)
        {
            return WithMessage(string.Empty).Unless(condition);
        }

        #endregion

        #region AndForMember

        public IValueFluentValidationRegistrar<T> AndForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            return WithMessage(string.Empty).AndForMember(fieldInfo, mode);
        }

        public IValueFluentValidationRegistrar<T, TVal2> AndForMember<TVal2>(Expression<Func<T, TVal2>> expression, VerifyRuleMode mode = VerifyRuleMode.Append)
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

        public IValidationRegistrar TakeEffectAndBack()
        {
            TakeEffect();
            return _rootRegistrar;
        }

        #endregion

        #region ExposeVerifyRulePackage

        public VerifyRulePackage ExposeRulePackage()
        {
            return WithMessage(string.Empty).ExposeRulePackage();
        }

        public VerifyRulePackage ExposeUnregisteredRulePackage()
        {
            return WithMessage(string.Empty).ExposeUnregisteredRulePackage();
        }

        #endregion
    }
}