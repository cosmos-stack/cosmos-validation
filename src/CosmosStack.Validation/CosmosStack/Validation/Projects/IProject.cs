using System;
using System.Collections.Generic;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Projects
{
    /// <summary>
    /// Project interface <br />
    /// 项目接口
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// Name <br />
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type <br />
        /// 类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Project class <br />
        /// 项目类型
        /// </summary>
        ProjectClass Class { get; }

        /// <summary>
        /// Verify <br />
        /// 验证入口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        VerifyResult Verify(VerifiableObjectContext context);

        /// <summary>
        /// Verify one <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        VerifyResult VerifyOne(VerifiableMemberContext context);
        
        /// <summary>
        /// Verify many <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        VerifyResult VerifyMany(IDictionary<string, VerifiableMemberContext> keyValueCollections);

        /// <summary>
        /// Expose rules <br />
        /// 导出规则
        /// </summary>
        /// <returns></returns>
        VerifyRulePackage ExposeRules();
        
        /// <summary>
        /// Expose member rules <br />
        /// 导出成员规则
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifyMemberRulePackage ExposeMemberRules(string memberName);
    }
}