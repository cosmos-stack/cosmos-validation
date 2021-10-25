using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CosmosStack.Validation.Internals.Rules;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Strategies
{
    /// <summary>
    /// Validation strategy base <br />
    /// 验证策略基类
    /// </summary>
    public abstract class ValidationStrategy : IValidationStrategy, ICorrectStrategy
    {
        private readonly List<CorrectValueRuleBuilder> _memberValueRuleBuilders;
        private readonly object _builderLockObj = new();
        private readonly VerifiableObjectContract _contract;

        protected ValidationStrategy(Type type)
        {
            SourceType = type ?? throw new ArgumentNullException(nameof(type));

            _memberValueRuleBuilders = new List<CorrectValueRuleBuilder>();
            _contract = VerifiableObjectContractManager.Resolve(type);
        }

        public Type SourceType { get; }

        IEnumerable<CorrectValueRuleBuilder> ICorrectStrategy.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }

        protected IValueRuleBuilder ForMember(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            lock (_builderLockObj)
            {
                var builder = _memberValueRuleBuilders.FirstOrDefault(b => b.MemberName == name);
                if (builder is null)
                {
                    builder = new CorrectValueRuleBuilder(_contract.GetMemberContract(name));
                    _memberValueRuleBuilders.Add(builder);
                }

                return builder;
            }
        }

        protected IValueRuleBuilder ForMember(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));
            return ForMember(propertyInfo.Name);
        }

        protected IValueRuleBuilder ForMember(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));
            return ForMember(fieldInfo.Name);
        }
    }
}