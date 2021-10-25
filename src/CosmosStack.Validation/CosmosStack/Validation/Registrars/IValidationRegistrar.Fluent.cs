using System;
using System.Reflection;
using CosmosStack.Validation.Registrars.Interfaces;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Fluent Validation Register Interface <br />
    /// 流畅验证注册器接口
    /// </summary>
    public interface IFluentValidationRegistrar :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayRegisterForMember,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect,
        IMayUseRulePackageForType,
        IMayExposeRulePackageForType,
        IMayExposeUnregisteredRulePackageForType
    {
        /// <summary>
        /// Gets name <br />
        /// 获取名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Is anonymous <br />
        /// 标记是否为匿名
        /// </summary>
        bool IsAnonymous { get; }

        /// <summary>
        /// Source type <br />
        /// 源类型
        /// </summary>
        Type SourceType { get; }
    }

    /// <summary>
    /// Fluent Validation Register Interface <br />
    /// 流畅验证注册器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFluentValidationRegistrar<T> : IFluentValidationRegistrar,
        IMayRegisterForMember<T>
    {
        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        new IValueFluentValidationRegistrar<T> ForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        new IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        new IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}