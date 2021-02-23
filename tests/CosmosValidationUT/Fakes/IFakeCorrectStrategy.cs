using System;
using System.Collections.Generic;

namespace CosmosValidationUT.Fakes
{
    public interface IFakeCorrectStrategy
    {
        Type SourceType { get; }

        IEnumerable<FakeValueRuleBuilder> GetValueRuleBuilders();
    }

    public interface IFakeCorrectStrategy<T>
    {
        Type SourceType { get; }

        IEnumerable<FakeValueRuleBuilder<T>> GetValueRuleBuilders();
    }
}