using System;

namespace Cosmos.Validation.Objects
{
    internal class BuildInObjectResolver : IValidationObjectResolver
    {
        public ObjectContext Resolve<T>(T instance)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ToObjectContext();
            
            var contract = ObjectContractManager.Resolve<T>();

            return new ObjectContext(instance, contract);
        }

        public ObjectContext Resolve<T>(T instance, string instanceName)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ToObjectContext();
            
            var contract = ObjectContractManager.Resolve<T>();

            return new ObjectContext(instance, contract, instanceName);
        }

        public ObjectContext Resolve(Type type, object instance)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ToObjectContext();
            
            var contract = ObjectContractManager.Resolve(type);

            return new ObjectContext(instance, contract);
        }

        public ObjectContext Resolve(Type type, object instance, string instanceName)
        {
            if (instance is ObjectContext context) return context;
            if (instance is ObjectValueContext valueContext) return valueContext.ToObjectContext();
            
            var contract = ObjectContractManager.Resolve(type);

            return new ObjectContext(instance, contract, instanceName);
        }
    }
}