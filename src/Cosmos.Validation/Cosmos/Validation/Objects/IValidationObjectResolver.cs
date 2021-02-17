using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    public interface IValidationObjectResolver
    {
        ObjectContext Resolve<T>(T instance);
        ObjectContext Resolve<T>(T instance, string instanceName);
        ObjectContext Resolve<T>(IDictionary<string, object> keyValueCollections);
        ObjectContext Resolve<T>(IDictionary<string, object> keyValueCollections, string instanceName);
        ObjectContext Resolve(Type type, object instance);
        ObjectContext Resolve(Type type, object instance, string instanceName);
        ObjectContext Resolve(Type type, IDictionary<string, object> keyValueCollections);
        ObjectContext Resolve(Type type, IDictionary<string, object> keyValueCollections, string instanceName);
    }
}