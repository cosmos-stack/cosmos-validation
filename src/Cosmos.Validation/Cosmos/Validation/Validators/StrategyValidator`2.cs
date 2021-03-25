using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Strategies;

namespace Cosmos.Validation.Validators
{
    public sealed class StrategyValidator<TStrategy, T> : IValidator<T>
        where TStrategy : class, IValidationStrategy<T>, new()
    {
        public StrategyValidator()
        {
            Handler = ValidationHandler.CreateByStrategy<TStrategy, T>();
        }

        public StrategyValidator(ValidationOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));
            Handler = ValidationHandler.CreateByStrategy<TStrategy, T>(options);
        }

        private ValidationHandler Handler { get; }

        public string Name => $"Strategy Validator for '{typeof(TStrategy).GetFriendlyName()}'";

        public bool IsAnonymous => true;

        public VerifyResult Verify(Type declaringType, object instance)
        {
            return Handler.Verify(declaringType, instance);
        }

        public VerifyResult Verify(T instance)
        {
            return Handler.Verify(instance);
        }

        public VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            return Handler.VerifyOne(declaringType, memberValue, memberName);
        }

        public VerifyResult VerifyOne(object memberValue, string memberName)
        {
            return Handler.VerifyOne<T>(memberValue, memberName);
        }

        public VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue)
        {
            return Handler.VerifyOne(expression, memberValue);
        }

        public VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
        {
            return Handler.VerifyOneWithInstance(declaringType, memberValue, memberName, instance);
        }

        public VerifyResult VerifyOneWithInstance(object memberValue, string memberName, T instance)
        {
            return Handler.VerifyOneWithInstance(typeof(T), memberValue, memberName, instance);
        }

        public VerifyResult VerifyOneWithInstance<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, T instance)
        {
            return Handler.VerifyOneWithInstance(expression, memberValue, instance);
        }

        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany(declaringType, keyValueCollections);
        }

        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany<T>(keyValueCollections);
        }
    }
}