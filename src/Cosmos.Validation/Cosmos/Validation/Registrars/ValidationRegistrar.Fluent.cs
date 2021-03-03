using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    internal class FluentValidationRegistrar : IFluentValidationRegistrar
    {
        private readonly string _name;
        private readonly IValidationRegistrar _parentRegistrar;
        private readonly ObjectContract _objectContract;

        public FluentValidationRegistrar(Type type, IValidationRegistrar parentRegistrar)
        {
            _name = string.Empty;
            SourceType = type;
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _objectContract = ObjectContractManager.Resolve(type);
            Rules = new List<CorrectValueRule>();
        }

        public FluentValidationRegistrar(Type type, string name, IValidationRegistrar parentRegistrar)
        {
            _name = name;
            SourceType = type;
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _objectContract = ObjectContractManager.Resolve(type);
            Rules = new List<CorrectValueRule>();
        }

        public Type SourceType { get; }

        public string Name => string.IsNullOrEmpty(_name) ? "Anonymous Registrar" : _name;

        public bool IsAnonymous => string.IsNullOrEmpty(_name);

        private List<CorrectValueRule> Rules { get; set; }

        #region ForMember

        public IValueFluentValidationRegistrar ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            var valueContract = _objectContract.GetValueContract(memberName);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Member named '{memberName}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this);
        }

        public IValueFluentValidationRegistrar ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            var valueContract = _objectContract.GetValueContract(propertyInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Property named '{propertyInfo.Name}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this);
        }

        public IValueFluentValidationRegistrar ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));

            var valueContract = _objectContract.GetValueContract(fieldInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Field named '{fieldInfo.Name}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this);
        }

        #endregion

        #region AndForType

        public IFluentValidationRegistrar AndForType(Type type)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForType(type);
        }

        public IFluentValidationRegistrar AndForType(Type type, string name)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForType(type, name);
        }

        public IFluentValidationRegistrar<T> AndForType<T>()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForType<T>();
        }

        public IFluentValidationRegistrar<T> AndForType<T>(string name)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForType<T>(name);
        }

        #endregion

        #region AndForStrategy

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForStrategy<TStrategy>(mode);
            return this;
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<T>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForStrategy<TStrategy, T>(mode);
            return this;
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForStrategy(strategy, mode);
            return this;
        }

        public IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            _parentRegistrar.ForStrategy(strategy, mode);
            return this;
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForStrategy<TStrategy>(name, mode);
            return this;
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<T>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForStrategy<TStrategy, T>(name, mode);
            return this;
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForStrategy(strategy, name, mode);
            return this;
        }

        public IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForStrategy(strategy, name, mode);
            return this;
        }

        #endregion
        
        #region RegisterValidator

        public IFluentValidationRegistrar AndForValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForValidator<TValidator>();
            return this;
        }

        public IFluentValidationRegistrar AndForValidator<TValidator, T>() where TValidator : CustomValidator<T>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForValidator<TValidator, T>();
            return this;
        }

        public IFluentValidationRegistrar AndForValidator(CustomValidator validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForValidator(validator);
            return this;
        }

        public IFluentValidationRegistrar AndForValidator<T>(CustomValidator<T> validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForValidator(validator);
            return this;
        }

        #endregion

        #region Build

        internal void BuildMySelf()
        {
            var registrar = (ICorrectRegistrar) _parentRegistrar;
            foreach (var rule in Rules)
                registrar.BuildForMember(rule);
        }

        public void Build()
        {
            BuildMySelf();
            _parentRegistrar.Build();
        }

        public ValidationHandler TempBuild()
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild();
        }

        public ValidationHandler TempBuild(ValidationOptions options)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(options);
        }

        public ValidationHandler TempBuild(Action<ValidationOptions> optionsAct)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(optionsAct);
        }

        #endregion
    }
}