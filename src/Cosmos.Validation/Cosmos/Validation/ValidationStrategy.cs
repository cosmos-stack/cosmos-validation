using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Strategies;

namespace Cosmos.Validation
{
    public class ValidationStrategy: IValidationStrategy, ICorrectStrategy
    {
        private readonly List<CorrectValueRuleBuilder> _memberValueRuleBuilders;
        private readonly object _builderLockObj = new object();
        
        
        
        public Type SourceType { get; }

        IEnumerable<CorrectValueRuleBuilder> ICorrectStrategy.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }
    }
}