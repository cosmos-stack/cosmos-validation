using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Strategies;

namespace Cosmos.Validation.Validators
{
    public sealed class StrategyValidator<TStrategy> : IValidator
        where TStrategy : class, IValidationStrategy, new()
    {
        public StrategyValidator()
        {
            Handler = ValidationHandler.CreateByStrategy<TStrategy>();
        }

        private ValidationHandler Handler { get; }

        public string Name => $"Strategy Validator for '{typeof(TStrategy).GetFriendlyName()}'";

        public bool IsAnonymous => true;

        public VerifyResult Verify(Type declaringType, object instance)
        {
            return Handler.Verify(declaringType, instance);
        }

        public VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            return Handler.VerifyOne(declaringType, memberValue, memberName);
        }

        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany(declaringType, keyValueCollections);
        }
    }
}