using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cosmos.Validation.Validators
{
    /// <summary>
    /// Interface of Cosmos Validator
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Name of validation
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Mark whether the validator is anonymous.
        /// </summary>
        bool IsAnonymous { get; }

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult Verify(Type declaringType, object instance);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection);

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections);
    }

    /// <summary>
    /// Interface of Cosmos Validator, a generic version.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T> : IValidator
    {
        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult Verify(T instance);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifyResult VerifyOne(object memberValue, string memberName);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithInstance(object memberValue, string memberName, T instance);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        VerifyResult VerifyOneWithInstance<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, T instance);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        VerifyResult VerifyOneWithDictionary(object memberValue, string memberName, IDictionary<string, object> keyValueCollection);

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        VerifyResult VerifyOneWithDictionary<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, IDictionary<string, object> keyValueCollection);

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections);
    }
}