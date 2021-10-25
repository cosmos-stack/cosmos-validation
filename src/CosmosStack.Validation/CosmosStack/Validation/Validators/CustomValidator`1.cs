using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CosmosStack.Validation.Internals;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// Custom validator. <br />
    /// 自定义验证器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CustomValidator<T> : CustomValidator, IValidator<T>, ICorrectValidator<T>
    {
        protected CustomValidator(string name) : base(name) { }

        protected CustomValidator(string name, IVerifiableObjectResolver objectResolver)
            : base(name, objectResolver) { }

        bool ICorrectValidator.IsTypeBinding => true;

        Type ICorrectValidator.SourceType => typeof(T);

        #region Verify

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual VerifyResult Verify(T instance)
        {
            if (instance is VerifiableObjectContext objectContext)
                return VerifyImpl(objectContext);
            if (instance is VerifiableMemberContext memberContext)
                return VerifyOneImpl(memberContext);
            if (instance is IDictionary<string, object> keyValueCollection)
                return VerifyImpl(_objectResolver.Resolve<T>(keyValueCollection));
            return VerifyImpl(_objectResolver.Resolve(instance));
        }

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult IValidator.Verify(Type declaringType, object instance)
        {
            if (instance is VerifiableObjectContext objectContext)
                return VerifyImpl(objectContext);
            if (instance is VerifiableMemberContext memberContext)
                return VerifyOneImpl(memberContext);
            if (instance is IDictionary<string, object> keyValueCollection)
                return VerifyImpl(_objectResolver.Resolve<T>(keyValueCollection));
            return VerifyImpl(_objectResolver.Resolve(declaringType, instance));
        }

        #endregion

        #region VerifyOne

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public virtual VerifyResult VerifyOne(object memberValue, string memberName)
        {
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public virtual VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue)
        {
            var memberName = PropertySelector.GetPropertyName(expression);
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            var memberContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual VerifyResult VerifyOneWithInstance(object memberValue, string memberName, T instance)
        {
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public virtual VerifyResult VerifyOneWithInstance<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, T instance)
        {
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(expression);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
        {
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        public virtual VerifyResult VerifyOneWithDictionary(object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public virtual VerifyResult VerifyOneWithDictionary<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, IDictionary<string, object> keyValueCollection)
        {
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(expression);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));
            return VerifyOneImpl(memberContext);
        }

        #endregion

        #region VerifyMany

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve<T>(keyValueCollections));
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        #endregion
    }
}