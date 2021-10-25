using System;
using System.Collections.Generic;
using CosmosStack.Validation.Internals.Rules;

namespace CosmosStack.Validation.Strategies
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