using System;
using System.Collections.Generic;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Strategies;

namespace CosmosValidationUT.Fakes
{
    public abstract class FakeValidationStrategy : IValidationStrategy, IFakeCorrectStrategy
    {
        private readonly List<FakeValueRuleBuilder> _memberValueRuleBuilders;
        private readonly ObjectContract _contract;

        protected FakeValidationStrategy(Type type)
        {
            SourceType = type ?? throw new ArgumentNullException(nameof(type));

            _memberValueRuleBuilders = new List<FakeValueRuleBuilder>();
            _contract = ObjectContractManager.Resolve(type);
        }

        public Type SourceType { get; }

        IEnumerable<FakeValueRuleBuilder> IFakeCorrectStrategy.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }
    }
}