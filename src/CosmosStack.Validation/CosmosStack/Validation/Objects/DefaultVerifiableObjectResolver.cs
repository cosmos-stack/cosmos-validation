using System;
using System.Collections.Generic;

namespace CosmosStack.Validation.Objects
{
   internal class DefaultVerifiableObjectResolver : IVerifiableObjectResolver
    {        
        public VerifiableObjectContext Resolve<T>(T instance)
        {
            if (instance is VerifiableObjectContext objectContext) return objectContext;
            if (instance is VerifiableMemberContext memberContext) return memberContext.ConvertToObjectContext();
            
            var contract = VerifiableObjectContractManager.Resolve<T>();

            return new VerifiableObjectContext(instance, contract);
        }

        public VerifiableObjectContext Resolve<T>(T instance, string instanceName)
        {
            if (instance is VerifiableObjectContext objectContext) return objectContext;
            if (instance is VerifiableMemberContext memberContext) return memberContext.ConvertToObjectContext();
            
            var contract = VerifiableObjectContractManager.Resolve<T>();

            return new VerifiableObjectContext(instance, contract, instanceName);
        }

        public VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection)
        {
            var contract = VerifiableObjectContractManager.Resolve<T>();

            return new VerifiableObjectContext(keyValueCollection, contract);
        }

        public VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection, string instanceName)
        {
            var contract = VerifiableObjectContractManager.Resolve<T>();

            return new VerifiableObjectContext(keyValueCollection, contract, instanceName);
        }

        public VerifiableObjectContext Resolve(Type declaringType, object instance)
        {
            if (instance is VerifiableObjectContext objectContext) return objectContext;
            if (instance is VerifiableMemberContext memberContext) return memberContext.ConvertToObjectContext();
            
            var contract = VerifiableObjectContractManager.Resolve(declaringType);

            return new VerifiableObjectContext(instance, contract);
        }

        public VerifiableObjectContext Resolve(Type declaringType, object instance, string instanceName)
        {
            if (instance is VerifiableObjectContext objectContext) return objectContext;
            if (instance is VerifiableMemberContext memberContext) return memberContext.ConvertToObjectContext();
            
            var contract = VerifiableObjectContractManager.Resolve(declaringType);

            return new VerifiableObjectContext(instance, contract, instanceName);
        }

        public VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection)
        {
            var contract = VerifiableObjectContractManager.Resolve(declaringType);

            return new VerifiableObjectContext(keyValueCollection, contract);
        }

        public VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection, string instanceName)
        {
            var contract = VerifiableObjectContractManager.Resolve(declaringType);

            return new VerifiableObjectContext(keyValueCollection, contract, instanceName);
        }
    }
}