using System;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May build interface, but will not update to the global configuration. <br />
    /// 标记可被构建，但不会更新到全局配置中。
    /// </summary>
    public interface IMayTempBuild
    {
        /// <summary>
        /// Build <br />
        /// 构建
        /// </summary>
        /// <returns></returns>
        ValidationHandler TempBuild();
        
        /// <summary>
        /// Build <br />
        /// 构建
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        ValidationHandler TempBuild(ValidationOptions options);
        
        /// <summary>
        /// Build <br />
        /// 构建
        /// </summary>
        /// <param name="optionsAct"></param>
        /// <returns></returns>
        ValidationHandler TempBuild(Action<ValidationOptions> optionsAct);
        
        /// <summary>
        /// Build <br />
        /// 构建
        /// </summary>
        /// <param name="objectResolver"></param>
        /// <returns></returns>
        ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver);
        
        /// <summary>
        /// Build <br />
        /// 构建
        /// </summary>
        /// <param name="objectResolver"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options);
    }
}