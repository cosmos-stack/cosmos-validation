using System;

namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May register for type interface <br />
    /// 标记可为类型注册
    /// </summary>
    public interface IMayRegisterForType
    {
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IFluentValidationRegistrar ForType(Type type);
        
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IFluentValidationRegistrar ForType(Type type, string name);
        
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar<T> ForType<T>();
        
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar<T> ForType<T>(string name);
    }

    /// <summary>
    /// May register for type continue interface <br />
    /// 标记可继续为类型注册
    /// </summary>
    public interface IMayContinueRegisterForType
    {
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IFluentValidationRegistrar AndForType(Type type);
        
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IFluentValidationRegistrar AndForType(Type type, string name);
        
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar<T> AndForType<T>();
        
        /// <summary>
        /// Register for type <br />
        /// 为类型注册
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar<T> AndForType<T>(string name);
    }
}