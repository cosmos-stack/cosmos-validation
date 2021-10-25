using System;
using System.Collections.Generic;

// ReSharper disable InconsistentlySynchronizedField
// ReSharper disable InconsistentNaming

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable Object Contract Manager <br />
    /// 可验证对象约定管理器
    /// </summary>
    public static class VerifiableObjectContractManager
    {
        private static readonly Dictionary<int, VerifiableObjectContract> _objectContracts = new();
        private static readonly object _lockObj = new();

        /// <summary>
        /// Resolve <br />
        /// 解析对象约定
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Resolve <br />
        /// 解析对象约定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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