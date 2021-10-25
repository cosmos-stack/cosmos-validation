using System;
using System.Collections.Generic;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Abstract Verifiable Object Resolver <br />
    /// 抽象的可验证对象解析器
    /// </summary>
    public abstract class AbstractVerifiableObjectResolver : IVerifiableObjectResolver
    {
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve<T>(T instance);
        
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve<T>(T instance, string instanceName);
        
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection);
        
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection, string instanceName);
        
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve(Type declaringType, object instance);
        
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve(Type declaringType, object instance, string instanceName);
        
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection);
        
        /// <inheritdoc />
        public abstract VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection, string instanceName);
    }
}