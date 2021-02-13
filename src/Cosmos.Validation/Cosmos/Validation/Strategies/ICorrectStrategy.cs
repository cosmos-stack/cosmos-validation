using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Rules;

namespace Cosmos.Validation.Strategies
{
    internal interface ICorrectStrategy
    {
        Type SourceType { get; }
        
        IEnumerable<CorrectValueRuleBuilder> GetValueRuleBuilders();
    }

    internal interface ICorrectStrategy<T>
    {
        Type SourceType { get; }
        
        IEnumerable<CorrectValueRuleBuilder<T>> GetValueRuleBuilders();
    }
}