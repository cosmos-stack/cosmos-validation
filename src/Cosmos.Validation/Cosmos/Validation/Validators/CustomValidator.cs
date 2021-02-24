using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Registrars;

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
        
        bool ICorrectValidator.IsFluentValidator { get; set; } = false;

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

        public virtual VerifyResult VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName)
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

    public abstract class CustomValidator<T> : CustomValidator, IValidator<T>, ICorrectValidator<T>
    {
        static CustomValidator() => ObjectContractManager.InitTypeFor<T>();

        protected CustomValidator(string name) : base(name)
        {
            _registrar = ValidationRegistrar.Continue();
        }

        protected CustomValidator(string name, string providerName) : base(name)
        {
            _registrar = ValidationRegistrar.ContinueWithoutException(providerName);
        }

        protected CustomValidator(string name, IValidationObjectResolver objectResolver)
            : base(name, objectResolver)
        {
            _registrar = ValidationRegistrar.Continue();
        }

        protected CustomValidator(string name, string providerName, IValidationObjectResolver objectResolver)
            : base(name, objectResolver)
        {
            _registrar = ValidationRegistrar.ContinueWithoutException(providerName);
        }

        bool ICorrectValidator.IsTypeBinding => true;

        bool ICorrectValidator.IsFluentValidator { get; set; } = false;

        #region ValidationHandler

        private IValidationRegistrar _registrar;
        private bool _needToBuildInternalValidationHandler;
        private bool _hasBuildInternalValidationHandler;
        private ValidationHandler _internalValidationHandler;

        private ValidationHandler LocalTypedHandler
        {
            get
            {
                if (!_needToBuildInternalValidationHandler)
                    return default;

                if (!_hasBuildInternalValidationHandler)
                {
                    _internalValidationHandler = _registrar.TempBuild();
                    _hasBuildInternalValidationHandler = true;
                }

                return _internalValidationHandler;
            }
        }

        protected IValueFluentValidationRegistrar<T> ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            _needToBuildInternalValidationHandler = true;
            ((ICorrectValidator<T>) this).IsFluentValidator = true;
            return _registrar.ForType<T>().ForMember(memberName, mode);
        }

        protected IValueFluentValidationRegistrar<T, TVal> ForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append)
        {
            _needToBuildInternalValidationHandler = true;
            ((ICorrectValidator<T>) this).IsFluentValidator = true;
            return _registrar.ForType<T>().ForMember(expression, mode);
        }

        #endregion

        #region Verify

        public virtual VerifyResult Verify(T instance)
        {
            if (instance is ObjectContext context)
                return VerifyImpl(context);
            if (instance is ObjectValueContext valueContext)
                return VerifyOneImpl(valueContext);
            if (instance is IDictionary<string, object> keyValueCollections)
                return VerifyManyImpl(_objectResolver.Resolve<T>(keyValueCollections));
            return VerifyImpl(_objectResolver.Resolve(instance));
        }

        VerifyResult IValidator.Verify(Type declaringType, object instance)
        {
            if (instance is ObjectContext context)
                return VerifyImpl(context);
            if (instance is ObjectValueContext valueContext)
                return VerifyOneImpl(valueContext);
            if (instance is IDictionary<string, object> keyValueCollections)
                return VerifyManyImpl(_objectResolver.Resolve<T>(keyValueCollections));
            return VerifyImpl(_objectResolver.Resolve(declaringType, instance));
        }

        protected override VerifyResult VerifyImpl(ObjectContext context)
        {
            return LocalTypedHandler?.Verify(context, "") ?? VerifyResult.Success;
        }

        #endregion

        #region VerifyOne

        public virtual VerifyResult VerifyOne(Type memberType, object memberValue, string memberName)
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

        VerifyResult IValidator.VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName)
        {
            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);
            return VerifyOneImpl(valueContext);
        }

        protected override VerifyResult VerifyOneImpl(ObjectValueContext context)
        {
            return LocalTypedHandler?.VerifyOne(context) ?? VerifyResult.Success;
        }

        #endregion

        #region VerifyMany

        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            return VerifyManyImpl(_objectResolver.Resolve<T>(keyValueCollections));
        }

        VerifyResult IValidator.VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyManyImpl(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        protected virtual VerifyResult VerifyManyImpl(ObjectContext context)
        {
            return LocalTypedHandler?.VerifyMany(context, "") ?? VerifyResult.Success;
        }

        #endregion
    }
}