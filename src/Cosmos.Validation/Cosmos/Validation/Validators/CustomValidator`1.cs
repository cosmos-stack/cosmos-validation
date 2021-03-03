using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public abstract class CustomValidator<T> : CustomValidator, IValidator<T>, ICorrectValidator<T>
    {
        protected CustomValidator(string name) : base(name) { }

        protected CustomValidator(string name, IVerifiableObjectResolver objectResolver)
            : base(name, objectResolver) { }

        bool ICorrectValidator.IsTypeBinding => true;

        Type ICorrectValidator.SourceType => typeof(T);

        #region Verify

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

        public virtual VerifyResult VerifyOne(object memberValue, string memberName)
        {
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        public virtual VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue)
        {
            var memberName = PropertySelector.GetPropertyName(expression);
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        VerifyResult IValidator.VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            var memberContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        #endregion

        #region VerifyMany

        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve<T>(keyValueCollections));
        }

        VerifyResult IValidator.VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        #endregion
    }
}