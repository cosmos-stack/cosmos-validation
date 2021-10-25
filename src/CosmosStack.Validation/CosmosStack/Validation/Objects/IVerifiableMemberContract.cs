using System;
using System.Collections.Generic;
using CosmosStack.Validation.Annotations;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// An interface for Verifiable Member Contract <br />
    /// 可验证成员约定的接口
    /// </summary>
    public interface IVerifiableMemberContract
    {
        /// <summary>
        /// Member name <br />
        /// 成员名
        /// </summary>
        string MemberName { get; }

        /// <summary>
        /// Declaring type <br />
        /// 声明类型
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// Member type <br />
        /// 成员类型
        /// </summary>
        Type MemberType { get; }

        /// <summary>
        /// Is basic type <br />
        /// 是否为基本类型
        /// </summary>
        bool IsBasicType { get; }

        /// <summary>
        /// Member kind <br />
        /// 成员类型
        /// </summary>
        VerifiableMemberKind MemberKind { get; }

        /// <summary>
        /// Get value from the given instance <br />
        /// 从给定的实例中获取值
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        object GetValue(object instance);

        /// <summary>
        /// Get value from dictionary <br />
        /// 从给定的字典中获取值
        /// </summary>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        object GetValue(IDictionary<string, object> keyValueCollection);

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
        /// <param name="excludeFlagAnnotation"></param>
        /// <param name="excludeObjectContextVerifiableAnnotation"></param>
        /// <param name="excludeStrongVerifiableAnnotation"></param>
        /// <returns></returns>
        IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false,
            bool excludeStrongVerifiableAnnotation = false);

        /// <summary>
        /// Get strong verifiable annotations <br />
        /// 获取强验证注解
        /// </summary>
        /// <param name="excludeFlagAnnotation"></param>
        /// <param name="excludeObjectContextVerifiableAnnotation"></param>
        /// <returns></returns>
        IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false);

        /// <summary>
        /// Get ObjectContext verifiable annotations <br />
        /// 获取带上下文的可验证注解
        /// </summary>
        /// <param name="excludeFlagAnnotation"></param>
        /// <returns></returns>
        IEnumerable<IObjectContextVerifiableAnnotation> GetObjectContextVerifiableAnnotations(
            bool excludeFlagAnnotation = false);

        /// <summary>
        /// Get flag annotations <br />
        /// 获取带 Flag 的注解
        /// </summary>
        /// <param name="excludeVerifiableAnnotation"></param>
        /// <returns></returns>
        IEnumerable<IFlagAnnotation> GetFlagAnnotations(
            bool excludeVerifiableAnnotation = false);

        /// <summary>
        /// Get verifiable annotations <br />
        /// 获取可验证注解
        /// </summary>
        /// <param name="excludeFlagAnnotation"></param>
        /// <returns></returns>
        IEnumerable<IVerifiable> GetVerifiableAnnotations(
            bool excludeFlagAnnotation = false);
    }
}