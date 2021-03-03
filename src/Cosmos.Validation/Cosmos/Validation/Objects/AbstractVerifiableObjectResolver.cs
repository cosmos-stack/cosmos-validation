using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    public abstract class AbstractVerifiableObjectResolver : IVerifiableObjectResolver
    {
        public abstract VerifiableObjectContext Resolve<T>(T instance);
        public abstract VerifiableObjectContext Resolve<T>(T instance, string instanceName);
        public abstract VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection);
        public abstract VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection, string instanceName);
        public abstract VerifiableObjectContext Resolve(Type declaringType, object instance);
        public abstract VerifiableObjectContext Resolve(Type declaringType, object instance, string instanceName);
        public abstract VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection);
        public abstract VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection, string instanceName);
    }
}