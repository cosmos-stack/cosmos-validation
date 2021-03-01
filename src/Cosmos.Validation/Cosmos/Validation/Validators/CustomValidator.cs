using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public abstract class CustomValidator : IValidator, ICorrectValidator
    {
        // ReSharper disable once InconsistentNaming
        protected readonly IValidationObjectResolver _objectResolver;

        protected CustomValidator(string name)
        {
            Name = name;
            _objectResolver = new BuildInObjectResolver();
        }

        protected CustomValidator(string name, IValidationObjectResolver objectResolver)
        {
            Name = name;
            _objectResolver = objectResolver ?? new BuildInObjectResolver();
        }

        public string Name { get; }

        public bool IsAnonymous => string.IsNullOrEmpty(Name);

        bool ICorrectValidator.IsTypeBinding => false;

        Type ICorrectValidator.SourceType => TypeClass.ObjectClazz;

        #region Verify

        public virtual VerifyResult Verify(Type declaringType, object instance)
        {
            var context = _objectResolver.Resolve(declaringType, instance);
            return VerifyImpl(context);
        }

        public virtual VerifyResult VerifyViaContext(ObjectContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyImpl(context);
        }

        protected abstract VerifyResult VerifyImpl(ObjectContext context);

        #endregion

        #region VerifyOne

        public virtual VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);
            return VerifyOneImpl(valueContext);
        }

        public virtual VerifyResult VerifyOneViaContext(ObjectValueContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyOneImpl(context);
        }

        protected abstract VerifyResult VerifyOneImpl(ObjectValueContext context);

        #endregion

        #region VerifyMany

        public virtual VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        #endregion
    }
}