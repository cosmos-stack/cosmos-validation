using System;

namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May expose rule package interface <br />
    /// 标记注册器可被导出规则包
    /// </summary>
    public interface IMayExposeRulePackage
    {
        /// <summary>
        /// Expose rule package <br />
        /// 导出规则包
        /// </summary>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        VerifyRulePackage ExposeRulePackage<T>(string projectName = "");

        /// <summary>
        /// Expose rule package <br />
        /// 导出规则包
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        VerifyRulePackage ExposeRulePackage(Type declaringType, string projectName = "");
    }

    /// <summary>
    /// May expose unregistered rule package interface <br />
    /// 标记注册器可被导出未注册的规则包
    /// </summary>
    public interface IMayExposeUnregisteredRulePackage
    {
        /// <summary>
        /// Expose unregistered rule package <br />
        /// 导出未注册的规则包
        /// </summary>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        VerifyRulePackage ExposeUnregisteredRulePackage<T>(string projectName = "");

        /// <summary>
        /// Expose unregistered rule package <br />
        /// 导出未注册的规则包
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        VerifyRulePackage ExposeUnregisteredRulePackage(Type declaringType, string projectName = "");
    }

    /// <summary>
    /// May expose rule package for type interface <br />
    /// 标记注册器可针对类型导出规则包
    /// </summary>
    public interface IMayExposeRulePackageForType
    {
        /// <summary>
        /// Expose rule package <br />
        /// 导出规则包
        /// </summary>
        /// <returns></returns>
        VerifyRulePackage ExposeRulePackage();
    }

    /// <summary>
    /// May expose unregistered rule package for type interface <br />
    /// 标记注册器可针对类型导出未注册的规则包
    /// </summary>
    public interface IMayExposeUnregisteredRulePackageForType
    {
        /// <summary>
        /// Expose unregistered rule package <br />
        /// 导出未注册的规则包
        /// </summary>
        /// <returns></returns>
        VerifyRulePackage ExposeUnregisteredRulePackage();
    }
}