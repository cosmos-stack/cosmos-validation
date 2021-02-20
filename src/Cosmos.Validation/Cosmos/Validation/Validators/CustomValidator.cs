using System;
using System.Collections.Generic;
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

        #region Verify

        public virtual VerifyResult Verify(Type declaringType, object instance)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, instance));
        }

        protected abstract VerifyResult VerifyImpl(ObjectContext context);

        public VerifyResult VerifyViaContext(ObjectContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyImpl(context);
        }

        #endregion

        #region VerifyOne

        public virtual VerifyResult VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName)
        {
            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);
            return VerifyOneImpl(valueContext);
        }

        protected abstract VerifyResult VerifyOneImpl(ObjectValueContext context);

        public VerifyResult VerifyOneViaContext(ObjectValueContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyOneImpl(context);
        }

        #endregion

        #region VerifyMany

        public virtual VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        #endregion
    }

    public abstract class CustomValidator<T> : CustomValidator, IValidator<T>, ICorrectValidator<T>
    {
        protected CustomValidator(string name) : base(name) { }

        protected CustomValidator(string name, IValidationObjectResolver objectResolver)
            : base(name, objectResolver) { }

        #region Verify

        public virtual VerifyResult Verify(T instance)
        {
            return VerifyImpl(_objectResolver.Resolve(instance));
        }

        VerifyResult IValidator.Verify(Type declaringType, object instance)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, instance));
        }

        #endregion

        #region Verify

        public VerifyResult VerifyOne(Type memberType, object memberValue, string memberName)
        {
            var valueContract = ObjectContractManager.Resolve<T>()?.GetValueContract(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);
            return VerifyOneImpl(valueContext);
        }

        VerifyResult IValidator.VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName)
        {
            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
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