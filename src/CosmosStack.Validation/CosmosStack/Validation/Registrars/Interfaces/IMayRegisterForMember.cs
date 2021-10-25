using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May register for member interface <br />
    /// 标记可为成员注册
    /// </summary>
    public interface IMayRegisterForMember
    {
        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar ForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar ForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar ForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
    }

    /// <summary>
    /// May register for member interface <br />
    /// 标记可为成员注册
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMayRegisterForMember<T>
    {
        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> ForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> ForMember<TVal>(Expression<Func<T, TVal>> expression, VerifyRuleMode mode = VerifyRuleMode.Append);
    }

    /// <summary>
    /// May register for member continue interface <br />
    /// 标记可继续为成员注册
    /// </summary>
    public interface IMayContinueRegisterForMember
    {
        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar AndForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar AndForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar AndForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
    }

    /// <summary>
    /// May register for member continue interface <br />
    /// 标记可继续为成员注册
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMayContinueRegisterForMember<T>
    {
        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> AndForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);

        /// <summary>
        /// Register for member <br />
        /// 为成员注册
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> AndForMember<TVal>(Expression<Func<T, TVal>> expression, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}