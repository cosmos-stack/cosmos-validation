using System;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation
{
    /// <summary>
    /// An interface of validation provider <br />
    /// 验证服务提供者程序接口
    /// </summary>
    public interface IValidationProvider
    {
        /// <summary>
        /// Resolve a validator based on a given type. <br />
        /// 根据给定的类型解析验证器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IValidator Resolve(Type type);
        
        /// <summary>
        /// Resolve a validator based on a given type and name. <br />
        /// 根据给定的类型和名称解析验证器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IValidator Resolve(Type type, string name);
        
        /// <summary>
        /// Resolve a validator based on a given type. <br />
        /// 根据给定的类型解析验证器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidator<T> Resolve<T>();
        
        /// <summary>
        /// Resolve a validator based on a given type and name. <br />
        /// 根据给定的类型和名称解析验证器
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidator<T> Resolve<T>(string name);

        /// <summary>
        /// Override the configuration of the validator. <br />
        /// 覆盖验证选项
        /// </summary>
        /// <param name="options"></param>
        void UpdateOptions(ValidationOptions options);
        
        /// <summary>
        /// Update the configuration of the validator. <br />
        /// 覆盖验证选项
        /// </summary>
        /// <param name="optionAct"></param>
        void UpdateOptions(Action<ValidationOptions> optionAct);
    }
}