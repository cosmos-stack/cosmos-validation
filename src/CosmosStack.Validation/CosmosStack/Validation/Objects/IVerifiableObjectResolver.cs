using System;
using System.Collections.Generic;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// An interface for verifiable object resolver <br />
    /// 可验证对象解析器
    /// </summary>
    public interface IVerifiableObjectResolver
    {
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        VerifiableObjectContext Resolve<T>(T instance);
        
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="instanceName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        VerifiableObjectContext Resolve<T>(T instance, string instanceName);
        
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection);
        
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="keyValueCollection"></param>
        /// <param name="instanceName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        VerifiableObjectContext Resolve<T>(IDictionary<string, object> keyValueCollection, string instanceName);
       
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifiableObjectContext Resolve(Type declaringType, object instance);
        
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        VerifiableObjectContext Resolve(Type declaringType, object instance, string instanceName);
       
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection);
      
        /// <summary>
        /// Resolve <br />
        /// 解析
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollection"></param>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        VerifiableObjectContext Resolve(Type declaringType, IDictionary<string, object> keyValueCollection, string instanceName);
    }
}