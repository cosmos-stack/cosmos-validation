using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CosmosStack.Validation.Internals.Extensions;
using CosmosStack.Validation.Strategies;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// Strategy-based validator, used to quickly build the packaging of the strategy validator. <br />
    /// 基于策略的验证器，用于快速构建策略验证器包
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
        /// Name of validation <br />
        /// 验证器名称
        /// </summary>
        public string Name => $"Strategy Validator for '{typeof(TStrategy).GetFriendlyName()}'";

        /// <summary>
        /// Mark whether the validator is anonymous. <br />
        /// 标记是否为匿名验证器
        /// </summary>
        public bool IsAnonymous => true;

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult Verify(Type declaringType, object instance)
        {
            return Handler.Verify(declaringType, instance);
        }

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult Verify(T instance)
        {
            return Handler.Verify(instance);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public VerifyResult VerifyOne(object memberValue, string memberName)
        {
            return Handler.VerifyOne<T>(memberValue, memberName);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify a member of the entity. <br />
        /// 成员验证入口
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
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany(declaringType, keyValueCollections);
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany<T>(keyValueCollections);
        }
    }
}