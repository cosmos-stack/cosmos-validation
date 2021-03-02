using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Strategies;

namespace Cosmos.Validation.Validators
{
    public sealed class StrategyValidator : IValidator
    {
        public StrategyValidator(IValidationStrategy strategy)
        {
            if (strategy is null) throw new ArgumentNullException(nameof(strategy));
            Handler = ValidationHandler.CreateByStrategy(strategy);
            Name = $"Strategy Validator for '{strategy.GetType().GetFriendlyName()}'";
        }

        private ValidationHandler Handler { get; }

        public string Name { get; }

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

        public static IValidator By(IValidationStrategy strategy)
        {
            return new StrategyValidator(strategy);
        }

        public static IValidator By<TStrategy>() where TStrategy : class, IValidationStrategy, new()
        {
            return new StrategyValidator<TStrategy>();
        }

        public static IValidator<T> By<TStrategy, T>() where TStrategy : class, IValidationStrategy<T>, new()
        {
            return new StrategyValidator<TStrategy, T>();
        }
    }
}