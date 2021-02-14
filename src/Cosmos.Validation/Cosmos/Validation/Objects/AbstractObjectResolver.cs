using System;

namespace Cosmos.Validation.Objects
{
    public abstract class AbstractObjectResolver : IValidationObjectResolver
    {
        public abstract ObjectContext Resolve<T>(T instance);
        public abstract ObjectContext Resolve<T>(T instance, string instanceName);
        public abstract ObjectContext Resolve(Type type, object instance);
        public abstract ObjectContext Resolve(Type type, object instance, string instanceName);
    }
}