using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Registrars.Interfaces;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    internal class FluentValidationRegistrar : IFluentValidationRegistrar
    {
        private readonly string _name;
        private readonly IValidationRegistrar _parentRegistrar;
        private readonly VerifiableObjectContract _verifiableObjectContract;

        private ICorrectRegistrar ParentRgPtr => (ICorrectRegistrar) _parentRegistrar;

        public FluentValidationRegistrar(Type type, IValidationRegistrar parentRegistrar)
        {
            _name = string.Empty;
            SourceType = type;
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _verifiableObjectContract = VerifiableObjectContractManager.Resolve(type);
            Rules = new List<CorrectValueRule>();
        }

        public FluentValidationRegistrar(Type type, string name, IValidationRegistrar parentRegistrar)
        {
            _name = name;
            SourceType = type;
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _verifiableObjectContract = VerifiableObjectContractManager.Resolve(type);
            Rules = new List<CorrectValueRule>();
        }

        public Type SourceType { get; }

        public string Name => string.IsNullOrEmpty(_name) ? "Anonymous Registrar" : _name;

        public bool IsAnonymous => string.IsNullOrEmpty(_name);

        private List<CorrectValueRule> Rules { get; set; }

        #region ForMember

        public IValueFluentValidationRegistrar ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            var valueContract = _verifiableObjectContract.GetMemberContract(memberName);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Member named '{memberName}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this, _parentRegistrar);
        }

        public IValueFluentValidationRegistrar ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            var valueContract = _verifiableObjectContract.GetMemberContract(propertyInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Property named '{propertyInfo.Name}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this, _parentRegistrar);
        }

        public IValueFluentValidationRegistrar ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));

            var valueContract = _verifiableObjectContract.GetMemberContract(fieldInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Field named '{fieldInfo.Name}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this, _parentRegistrar);
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

        #region AndForCustomValidator

        public IFluentValidationRegistrar AndForCustomValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForCustomValidator<TValidator>();
            return this;
        }

        public IFluentValidationRegistrar AndForCustomValidator<TValidator, T>() where TValidator : CustomValidator<T>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForCustomValidator<TValidator, T>();
            return this;
        }

        public IFluentValidationRegistrar AndForCustomValidator(CustomValidator validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForCustomValidator(validator);
            return this;
        }

        public IFluentValidationRegistrar AndForCustomValidator<T>(CustomValidator<T> validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            _parentRegistrar.ForCustomValidator(validator);
            return this;
        }

        #endregion

        #region Build

        internal void BuildMySelf()
        {
            foreach (var rule in Rules)
                ParentRgPtr.BuildForMember(rule);
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

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(objectResolver);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(objectResolver, options);
        }

        #endregion

        #region TakeEffect

        public void TakeEffect()
        {
            BuildMySelf();
            _parentRegistrar.TakeEffect();
        }

        public IValidationRegistrar TakeEffectAndBack()
        {
            TakeEffect();
            return _parentRegistrar;
        }
        
        #endregion
    }
}