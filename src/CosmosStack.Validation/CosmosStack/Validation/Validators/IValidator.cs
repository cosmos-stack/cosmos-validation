using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// Interface of Cosmos Validator <br />
    /// 验证器接口
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Name of validation <br />
        /// 验证器名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Mark whether the validator is anonymous. <br />
        /// 标记是否为匿名验证器
        /// </summary>
        bool IsAnonymous { get; }

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult Verify(Type declaringType, object instance);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection);

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections);
    }

    /// <summary>
    /// Interface of Cosmos Validator, a generic version. <br />
    /// 验证器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T> : IValidator
    {
        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult Verify(T instance);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifyResult VerifyOne(object memberValue, string memberName);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithInstance(object memberValue, string memberName, T instance);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        VerifyResult VerifyOneWithInstance<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, T instance);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithDictionary(object memberValue, string memberName, IDictionary<string, object> keyValueCollection);

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        VerifyResult VerifyOneWithDictionary<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, IDictionary<string, object> keyValueCollection);

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections);
    }
}