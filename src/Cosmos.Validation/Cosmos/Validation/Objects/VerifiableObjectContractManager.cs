using System;
using System.Collections.Generic;

// ReSharper disable InconsistentlySynchronizedField
// ReSharper disable InconsistentNaming

namespace Cosmos.Validation.Objects
{
    public static class VerifiableObjectContractManager
    {
        private static readonly Dictionary<int, VerifiableObjectContract> _objectContracts = new();
        private static readonly object _lockObj = new();

        public static VerifiableObjectContract Resolve(Type type)
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

                Dictionary<string, VerifiableMemberContract> valueContracts = new();

                if (type.IsBasicType())
                {
                    valueContracts.Add(VerifiableMemberContract.BASIC_TYPE, new VerifiableMemberContract(type));
                }
                else
                {
                    foreach (var property in type.GetProperties())
                        valueContracts.Add(property.Name, new VerifiableMemberContract(type, property));

                    foreach (var field in type.GetFields())
                        valueContracts.Add(field.Name, new VerifiableMemberContract(type, field));
                }

                var contract = new VerifiableObjectContract(type, valueContracts);

                _objectContracts[hash] = contract;

                return contract;
            }
        }

        public static VerifiableObjectContract Resolve<T>()
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