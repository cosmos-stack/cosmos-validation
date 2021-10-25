using System;
using System.Collections.Generic;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// An interface for custom validator manager
    /// </summary>
    public interface ICustomValidatorManager
    {
        /// <summary>
        /// Register custom validator <br />
        /// 注册自定义验证器
        /// </summary>
        /// <typeparam name="TValidator"></typeparam>
        void Register<TValidator>() where TValidator : CustomValidator, new();

        /// <summary>
        /// Register custom validator <br />
        /// 注册自定义验证器
        /// </summary>
        /// <typeparam name="TValidator"></typeparam>
        /// <typeparam name="T"></typeparam>
        void Register<TValidator, T>() where TValidator : CustomValidator<T>, new();

        /// <summary>
        /// Register custom validator <br />
        /// 注册自定义验证器
        /// </summary>
        /// <param name="validator"></param>
        void Register(CustomValidator validator);

        /// <summary>
        /// Register custom validator <br />
        /// 注册自定义验证器
        /// </summary>
        /// <param name="validator"></param>
        /// <typeparam name="T"></typeparam>
        void Register<T>(CustomValidator<T> validator);

        /// <summary>
        /// Resolve custom validator <br />
        /// 解析自定义验证器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        CustomValidator Resolve(string name);

        /// <summary>
        /// Resolve custom validator <br />
        /// 解析自定义验证器
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        CustomValidator<T> Resolve<T>(string name);

        /// <summary>
        /// Resolve all custom validators <br />
        /// 解析所有自定义验证器
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomValidator> ResolveAll();

        /// <summary>
        /// Resolve empty custom validators <br />
        /// 解析空自定义验证器
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomValidator> ResolveEmpty();

        /// <summary>
        /// Resolve custom validator by filter <br />
        /// 根据筛选条件解析自定义验证器
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<CustomValidator> ResolveBy(Func<CustomValidator, bool> filter);
    }
}