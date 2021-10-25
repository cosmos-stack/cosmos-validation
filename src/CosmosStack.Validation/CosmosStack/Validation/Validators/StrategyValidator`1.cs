using System;
using System.Collections.Generic;
using CosmosStack.Validation.Internals.Extensions;
using CosmosStack.Validation.Strategies;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// Strategy-based validator, used to quickly build the packaging of the strategy validator. <br />
    /// 基于策略的验证器，用于快速构建策略验证器包
    /// </summary>
    /// <typeparam name="TStrategy"></typeparam>
    public sealed class StrategyValidator<TStrategy> : IValidator
        where TStrategy : class, IValidationStrategy, new()
    {
        public StrategyValidator()
        {
            Handler = ValidationHandler.CreateByStrategy<TStrategy>();
        }

        public StrategyValidator(ValidationOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));
            Handler = ValidationHandler.CreateByStrategy<TStrategy>(options);
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
        /// 字典验证入口
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
    }
}