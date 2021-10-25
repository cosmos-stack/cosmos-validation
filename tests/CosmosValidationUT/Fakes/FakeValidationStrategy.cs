using System;
using System.Collections.Generic;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Strategies;

namespace CosmosValidationUT.Fakes
{
    public abstract class FakeValidationStrategy : IValidationStrategy, IFakeCorrectStrategy
    {
        private readonly List<FakeValueRuleBuilder> _memberValueRuleBuilders;
        private readonly VerifiableObjectContract _contract;

        protected FakeValidationStrategy(Type type)
        {
            SourceType = type ?? throw new ArgumentNullException(nameof(type));

            _memberValueRuleBuilders = new List<FakeValueRuleBuilder>();
            _contract = VerifiableObjectContractManager.Resolve(type);
        }

        public Type SourceType { get; }

        IEnumerable<FakeValueRuleBuilder> IFakeCorrectStrategy.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }
    }
}