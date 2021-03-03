using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    public interface IVerifiableObjectResolver
    {
        VerifiableObjectContext Resolve<T>(T instance);
        VerifiableObjectContext Resolve<T>(T instance, string instanceName);
        VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection);
        VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection, string instanceName);
        VerifiableObjectContext Resolve(Type declaringType, object instance);
        VerifiableObjectContext Resolve(Type declaringType, object instance, string instanceName);
        VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection);
        VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection, string instanceName);
    }
}