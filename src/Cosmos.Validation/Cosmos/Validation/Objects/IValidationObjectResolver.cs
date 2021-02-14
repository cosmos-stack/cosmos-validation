using System;

namespace Cosmos.Validation.Objects
{
    public interface IValidationObjectResolver
    {
        ObjectContext Resolve<T>(T instance);
        ObjectContext Resolve<T>(T instance, string instanceName);
        ObjectContext Resolve(Type type, object instance);
        ObjectContext Resolve(Type type, object instance, string instanceName);
    }
}