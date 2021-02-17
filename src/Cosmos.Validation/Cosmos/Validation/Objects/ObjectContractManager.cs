using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    public static class ObjectContractManager
    {
        private static readonly ConcurrentDictionary<Type, ObjectContract> _objectContracts = new();
        
        public static ObjectContract Resolve(Type type)
        {
            if (type is null)
                return default;

            if (_objectContracts.TryGetValue(type, out var contract))
                return contract;

            Dictionary<string, ObjectValueContract> valueContracts = new();

            if (type.IsBasicType())
            {
                valueContracts.Add(ObjectValueContract.BASIC_TYPE, new ObjectValueContract(type));
            }
            else
            {
                foreach (var property in type.GetProperties())
                    valueContracts.Add(property.Name, new ObjectValueContract(type, property));

                foreach (var field in type.GetFields())
                    valueContracts.Add(field.Name, new ObjectValueContract(type, field));
            }

            contract = new ObjectContract(type, valueContracts);

            if (_objectContracts.TryAdd(type, contract))
                return contract;

            return default;
        }

        public static ObjectContract Resolve<T>()
        {
            return Resolve(typeof(T));
        }
    }
}