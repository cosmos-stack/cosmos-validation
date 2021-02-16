using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Registrars
{
    internal class FluentValidationRegistrar<T> : IFluentValidationRegistrar<T>
    {
        private readonly string _name;
        private readonly IValidationRegistrar _parentRegistrar;
        private readonly ObjectContract _objectContract;

        public FluentValidationRegistrar(IValidationRegistrar parentRegistrar)
        {
            _name = string.Empty;
            SourceType = typeof(T);
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _objectContract = ObjectContractManager.Resolve<T>();
            Rules = new List<CorrectValueRule>();
        }

        public FluentValidationRegistrar(string name, IValidationRegistrar parentRegistrar)
        {
            _name = name;
            SourceType = typeof(T);
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _objectContract = ObjectContractManager.Resolve<T>();
            Rules = new List<CorrectValueRule>();
        }

        public Type SourceType { get; }

        public string Name => string.IsNullOrEmpty(_name) ? "Anonymous Registrar" : _name;

        public bool IsAnonymous => string.IsNullOrEmpty(_name);

        private List<CorrectValueRule> Rules { get; set; }

        #region For Member

        IValueFluentValidationRegistrar IFluentValidationRegistrar.ForMember(string memberName, ValueRuleMode mode)
        {
            var valueContract = _objectContract.GetValueContract(memberName);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Member named '{memberName}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this);
        }

        IValueFluentValidationRegistrar IFluentValidationRegistrar.ForMember(PropertyInfo propertyInfo, ValueRuleMode mode)
        {
            var valueContract = _objectContract.GetValueContract(propertyInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Property named '{propertyInfo.Name}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this);
        }

        IValueFluentValidationRegistrar IFluentValidationRegistrar.ForMember(FieldInfo fieldInfo, ValueRuleMode mode)
        {
            var valueContract = _objectContract.GetValueContract(fieldInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Field named '{fieldInfo.Name}'.");

            return new ValueValidationRegistrar(valueContract, Rules, mode, this);
        }

        public IValueFluentValidationRegistrar<T> ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            var valueContract = _objectContract.GetValueContract(memberName);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Member named '{memberName}'.");

            return new ValueValidationRegistrar<T>(valueContract, Rules, mode, this);
        }

        public IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            var valueContract = _objectContract.GetValueContract(propertyInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Property named '{propertyInfo.Name}'.");

            return new ValueValidationRegistrar<T>(valueContract, Rules, mode, this);
        }

        public IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));

            var valueContract = _objectContract.GetValueContract(fieldInfo);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Field named '{fieldInfo.Name}'.");

            return new ValueValidationRegistrar<T>(valueContract, Rules, mode, this);
        }

        public IValueFluentValidationRegistrar<T, TVal> ForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var memberName = PropertySelector.GetPropertyName(expression);
            var valueContract = _objectContract.GetValueContract(memberName);

            if (valueContract is null)
                throw new InvalidOperationException($"Cannot match such Member named '{memberName}'.");

            return new ValueValidationRegistrar<T, TVal>(valueContract, Rules, mode, this);
        }

        #endregion

        #region And for type

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

        public IFluentValidationRegistrar<TType> AndForType<TType>()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForType<TType>();
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>(string name)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForType<TType>(name);
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

        #endregion
    }
}