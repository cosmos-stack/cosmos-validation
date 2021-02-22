using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    public static class ObjectContractManager
    {
        private static readonly Dictionary<int, ObjectContract> _objectContracts = new();
        private static readonly object _lockObj = new();

        public static ObjectContract Resolve(Type type)
        {
            if (type is null)
                return default;

            var hash = type.GetHashCode();

            if (_objectContracts.ContainsKey(hash))
                return _objectContracts[hash];

            lock (_lockObj)
            {
                if (_objectContracts.ContainsKey(hash))
                    return _objectContracts[hash];

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

                var contract = new ObjectContract(type, valueContracts);

                _objectContracts[hash] = contract;

                return contract;
            }
        }

        public static ObjectContract Resolve<T>()
        {
            return Resolve(typeof(T));
        }

        internal static void InitTypeFor<T>()
        {
            if (!typeof(T).IsBasicType())
                Resolve<T>();
        }
    }
}