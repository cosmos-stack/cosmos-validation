using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Strategies;

namespace Cosmos.Validation.Validators
{
    /// <summary>
    /// Strategy-based validator, used to quickly build the packaging of the strategy validator.
    /// </summary>
    /// <typeparam name="TStrategy"></typeparam>
    /// <typeparam name="T"></typeparam>
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

        /// <summary>
        /// Name of validation
        /// </summary>
        public string Name => $"Strategy Validator for '{typeof(TStrategy).GetFriendlyName()}'";

        /// <summary>
        /// Mark whether the validator is anonymous.
        /// </summary>
        public bool IsAnonymous => true;

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult Verify(Type declaringType, object instance)
        {
            return Handler.Verify(declaringType, instance);
        }

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult Verify(T instance)
        {
            return Handler.Verify(instance);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            return Handler.VerifyOne(declaringType, memberValue, memberName);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public VerifyResult VerifyOne(object memberValue, string memberName)
        {
            return Handler.VerifyOne<T>(memberValue, memberName);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue)
        {
            return Handler.VerifyOne(expression, memberValue);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
        {
            return Handler.VerifyOneWithInstance(declaringType, memberValue, memberName, instance);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance(object memberValue, string memberName, T instance)
        {
            return Handler.VerifyOneWithInstance(typeof(T), memberValue, memberName, instance);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, T instance)
        {
            return Handler.VerifyOneWithInstance(expression, memberValue, instance);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            return Handler.VerifyOneWithDictionary(declaringType, memberValue, memberName, keyValueCollection);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary(object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            return Handler.VerifyOneWithDictionary<T>(memberValue, memberName, keyValueCollection);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, IDictionary<string, object> keyValueCollection)
        {
            return Handler.VerifyOneWithDictionary(expression, memberValue, keyValueCollection);
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany(declaringType, keyValueCollections);
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany<T>(keyValueCollections);
        }
    }
}