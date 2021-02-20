using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    internal class BuildInObjectResolver : IValidationObjectResolver
    {        
        public ObjectContext Resolve<T>(T instance)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ConvertToObjectContext();
            
            var contract = ObjectContractManager.Resolve<T>();

            return new ObjectContext(instance, contract);
        }

        public ObjectContext Resolve<T>(T instance, string instanceName)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ConvertToObjectContext();
            
            var contract = ObjectContractManager.Resolve<T>();

            return new ObjectContext(instance, contract, instanceName);
        }

        public ObjectContext Resolve<T>(IDictionary<string, object> keyValueCollections)
        {
            var contract = ObjectContractManager.Resolve<T>();

            return new ObjectContext(keyValueCollections, contract);
        }

        public ObjectContext Resolve<T>(IDictionary<string, object> keyValueCollections, string instanceName)
        {
            var contract = ObjectContractManager.Resolve<T>();

            return new ObjectContext(keyValueCollections, contract, instanceName);
        }

        public ObjectContext Resolve(Type type, object instance)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ConvertToObjectContext();
            
            var contract = ObjectContractManager.Resolve(type);

            return new ObjectContext(instance, contract);
        }

        public ObjectContext Resolve(Type type, object instance, string instanceName)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ConvertToObjectContext();
            
            var contract = ObjectContractManager.Resolve(type);

            return new ObjectContext(instance, contract, instanceName);
        }

        public ObjectContext Resolve(Type type, IDictionary<string, object> keyValueCollections)
        {
            var contract = ObjectContractManager.Resolve(type);

            return new ObjectContext(keyValueCollections, contract);
        }

        public ObjectContext Resolve(Type type, IDictionary<string, object> keyValueCollections, string instanceName)
        {
            var contract = ObjectContractManager.Resolve(type);

            return new ObjectContext(keyValueCollections, contract, instanceName);
        }
    }
}