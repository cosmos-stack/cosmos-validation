using System;
using System.Collections.Generic;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Strategies;

namespace CosmosValidationUT.Fakes
{
    public abstract class FakeValidationStrategy<T> : IValidationStrategy<T>, IFakeCorrectStrategy<T>
    {
        private readonly List<FakeValueRuleBuilder<T>> _memberValueRuleBuilders;
        private readonly ObjectContract _contract;

        protected FakeValidationStrategy()
        {
            SourceType = typeof(T);

            _memberValueRuleBuilders = new List<FakeValueRuleBuilder<T>>();
            _contract = ObjectContractManager.Resolve<T>();
        }

        public Type SourceType { get; }

        IEnumerable<FakeValueRuleBuilder<T>> IFakeCorrectStrategy<T>.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }
    }
}