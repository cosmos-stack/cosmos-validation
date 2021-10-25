using System;
using System.Collections.Generic;
using System.Reflection;
using CosmosStack.Validation.Annotations;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// An interface for Verifiable Object Contract <br />
    /// 可验证对象约定的接口
    /// </summary>
    public interface IVerifiableObjectContract
    {
        /// <summary>
        /// Type <br />
        /// 类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Is basic type <br />
        /// 是否为基本类型
        /// </summary>
        bool IsBasicType { get; }

        /// <summary>
        /// Object Kind <br />
        /// 对象种类
        /// </summary>
        VerifiableObjectKind ObjectKind { get; }

        /// <summary>
        /// Get value from the given instance by member name <br />
        /// 根据给定的成员名称，从给定的实例中获取值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        object GetValue(object instance, string memberName);

        /// <summary>
        /// Get value from the given instance by member index <br />
        /// 根据给定的成员索引值，从给定的实例中获取值
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="memberIndex"></param>
        /// <returns></returns>
        object GetValue(object instance, int memberIndex);

        /// <summary>
        /// Get value from dictionary by member name <br />
        /// 根据给定的成员名称，从给定的字典中获取值
        /// </summary>
        /// <param name="keyValueCollection"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        object GetValue(IDictionary<string, object> keyValueCollection, string memberName);

        /// <summary>
        /// Get value from dictionary by member index <br />
        /// 根据给定的成员索引值，从给定的字典中获取值
        /// </summary>
        /// <param name="keyValueCollection"></param>
        /// <param name="memberIndex"></param>
        /// <returns></returns>
        object GetValue(IDictionary<string, object> keyValueCollection, int memberIndex);

        /// <summary>
        /// Get member contract <br />
        /// 获取成员约定
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifiableMemberContract GetMemberContract(string memberName);

        /// <summary>
        /// Get member contract <br />
        /// 获取成员约定
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        VerifiableMemberContract GetMemberContract(PropertyInfo propertyInfo);

        /// <summary>
        /// Get member contract <br />
        /// 获取成员约定
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        VerifiableMemberContract GetMemberContract(FieldInfo fieldInfo);

        /// <summary>
        /// Get member contract <br />
        /// 获取成员约定
        /// </summary>
        /// <param name="memberIndex"></param>
        /// <returns></returns>
        VerifiableMemberContract GetMemberContract(int memberIndex);

        /// <summary>
        /// Is contain member <br />
        /// 检查是否包含给定名称的成员
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        bool ContainsMember(string memberName);
        
        /// <summary>
        /// Get all member contracts <br />
        /// 获取所有成员约定
        /// </summary>
        /// <returns></returns>
        IEnumerable<VerifiableMemberContract> GetMemberContracts();
        
        /// <summary>
        /// Is include annotation <br />
        /// 标记是否包含注解
        /// </summary>
        bool IncludeAnnotations { get; }

        /// <summary>
        /// Get attributes <br />
        /// 获取特性集合
        /// </summary>
        IReadOnlyCollection<Attribute> Attributes { get; }

        /// <summary>
        /// Get attributes <br />
        /// 获取特性集合
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;

        // /// <summary>
        // /// Get attributes <br />
        // /// 获取特性集合
        // /// </summary>
        // /// <returns></returns>
        // AttributeCollection GetAttributeCollection();
        //
        // /// <summary>
        // /// Get attributes <br />
        // /// 获取特性集合
        // /// </summary>
        // /// <returns></returns>
        // AttributeCollection GetAttributeCollection<TAttribute>() where TAttribute : Attribute;

        /// <summary>
        /// Get parameter annotations <br />
        /// 获取参数注解
        /// </summary>
        /// <returns></returns>
        IEnumerable<VerifiableParamsAttribute> GetParameterAnnotations();

        /// <summary>
        /// Get quiet verifiable annotations <br />
        /// 获取安静验证注解
        /// </summary>
        /// <returns></returns>
        IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations();

        /// <summary>
        /// Get strong verifiable annotations <br />
        /// 获取强验证注解
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations();
    }
}