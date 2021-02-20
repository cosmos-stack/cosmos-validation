using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

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