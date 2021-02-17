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

        public virtual VerifyResult Verify(Type type, object instance)
        {
            return VerifyImpl(_objectResolver.Resolve(type, instance));
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

        public virtual VerifyResult VerifyOne(Type type, object instance, string memberName)
        {
            return VerifyOneImpl(_objectResolver.Resolve(type, instance).GetValue(memberName));
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

        public virtual VerifyResult VerifyMany(Type type, IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve(type, keyValueCollections));
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

        public override VerifyResult Verify(Type type, object instance)
        {
            if (instance is T t)
                return Verify(t);
            return VerifyResult.UnexpectedType;
        }

        #endregion

        #region VerifyOne

        public VerifyResult VerifyOne(T instance, string memberName)
        {
            return VerifyOneImpl(_objectResolver.Resolve(instance).GetValue(memberName));
        }

        public override VerifyResult VerifyOne(Type type, object instance, string memberName)
        {
            if (instance is T t)
                return VerifyOne(t, memberName);
            return VerifyResult.UnexpectedType;
        }

        #endregion
        
        #region VerifyMany

        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve<T>(keyValueCollections));
        }
        
        #endregion
    }
}