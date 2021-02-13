using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Strategies
{
    public abstract class ValidationStrategy<T> : IValidationStrategy<T>, ICorrectStrategy<T>
    {
        private readonly List<CorrectValueRuleBuilder<T>> _memberValueRuleBuilders;
        private readonly object _builderLockObj = new();
        private readonly ObjectContract _contract;

        protected ValidationStrategy()
        {
            SourceType = typeof(T);

            _memberValueRuleBuilders = new List<CorrectValueRuleBuilder<T>>();
            _contract = ObjectContractManager.Resolve<T>();
        }

        public Type SourceType { get; }

        IEnumerable<CorrectValueRuleBuilder<T>> ICorrectStrategy<T>.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }

        protected IValueRuleBuilder<T> RuleFor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            lock (_builderLockObj)
            {
                var builder = _memberValueRuleBuilders.FirstOrDefault(b => b.MemberName == name);
                if (builder is null)
                {
                    builder = new CorrectValueRuleBuilder<T>(_contract.GetValueContract(name));
                    _memberValueRuleBuilders.Add(builder);
                }

                return builder;
            }
        }

        protected IValueRuleBuilder<T> RuleFor(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));
            return RuleFor(propertyInfo.Name);
        }

        protected IValueRuleBuilder<T> RuleFor(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));
            return RuleFor(fieldInfo.Name);
        }

        protected IValueRuleBuilder<T> RuleFor<TVal>(Expression<Func<T, TVal>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            lock (_builderLockObj)
            {
                var name = PropertySelector.GetPropertyName(expression);
                var builder = _memberValueRuleBuilders.FirstOrDefault(b => b.MemberName == name);
                if (builder is null)
                {
                    builder = new CorrectValueRuleBuilder<T, TVal>(_contract.GetValueContract(name));
                    _memberValueRuleBuilders.Add(builder);
                }

                return builder;
            }
        }
    }
}