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
        string Name { get; }
        bool IsAnonymous { get; }
        VerifyResult Verify(Type declaringType, object instance);
        VerifyResult VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName);
        VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections);
    }

    public interface IValidator<T> : IValidator
    {
        VerifyResult Verify(T instance);
        VerifyResult VerifyOne(Type memberType, object memberValue, string memberName);
        VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> propertySelector, object memberValue);
        VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections);
    }
}