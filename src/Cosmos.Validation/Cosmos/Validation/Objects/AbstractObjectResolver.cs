using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    public abstract class AbstractObjectResolver : IValidationObjectResolver
    {
        public abstract ObjectContext Resolve<T>(T instance);
        public abstract ObjectContext Resolve<T>(T instance, string instanceName);
        public abstract ObjectContext Resolve<T>(IDictionary<string, object> keyValueCollections);
        public abstract ObjectContext Resolve<T>(IDictionary<string, object> keyValueCollections, string instanceName);
        public abstract ObjectContext Resolve(Type type, object instance);
        public abstract ObjectContext Resolve(Type type, object instance, string instanceName);
        public abstract ObjectContext Resolve(Type type, IDictionary<string, object> keyValueCollections);
        public abstract ObjectContext Resolve(Type type, IDictionary<string, object> keyValueCollections, string instanceName);
    }
}