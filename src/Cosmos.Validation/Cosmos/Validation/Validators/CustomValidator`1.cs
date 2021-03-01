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

        protected CustomValidator(string name, IValidationObjectResolver objectResolver)
            : base(name, objectResolver) { }

        bool ICorrectValidator.IsTypeBinding => true;

        Type ICorrectValidator.SourceType => typeof(T);

        #region Verify

        public virtual VerifyResult Verify(T instance)
        {
            if (instance is ObjectContext context)
                return VerifyImpl(context);
            if (instance is ObjectValueContext valueContext)
                return VerifyOneImpl(valueContext);
            if (instance is IDictionary<string, object> keyValueCollections)
                return VerifyImpl(_objectResolver.Resolve<T>(keyValueCollections));
            return VerifyImpl(_objectResolver.Resolve(instance));
        }

        VerifyResult IValidator.Verify(Type declaringType, object instance)
        {
            if (instance is ObjectContext context)
                return VerifyImpl(context);
            if (instance is ObjectValueContext valueContext)
                return VerifyOneImpl(valueContext);
            if (instance is IDictionary<string, object> keyValueCollections)
                return VerifyImpl(_objectResolver.Resolve<T>(keyValueCollections));
            return VerifyImpl(_objectResolver.Resolve(declaringType, instance));
        }

        #endregion

        #region VerifyOne

        public virtual VerifyResult VerifyOne(object memberValue, string memberName)
        {
            var valueContract = ObjectContractManager.Resolve<T>()?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);
            return VerifyOneImpl(valueContext);
        }

        public virtual VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> propertySelector, object memberValue)
        {
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var valueContract = ObjectContractManager.Resolve<T>()?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);
            return VerifyOneImpl(valueContext);
        }

        VerifyResult IValidator.VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);
            return VerifyOneImpl(valueContext);
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