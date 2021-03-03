using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public abstract class CustomValidator : IValidator, ICorrectValidator
    {
        // ReSharper disable once InconsistentNaming
        protected readonly IVerifiableObjectResolver _objectResolver;

        protected CustomValidator(string name)
        {
            Name = name;
            _objectResolver = new DefaultVerifiableObjectResolver();
        }

        protected CustomValidator(string name, IVerifiableObjectResolver objectResolver)
        {
            Name = name;
            _objectResolver = objectResolver ?? new DefaultVerifiableObjectResolver();
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

        public virtual VerifyResult VerifyViaContext(VerifiableObjectContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyImpl(context);
        }

        protected abstract VerifyResult VerifyImpl(VerifiableObjectContext context);

        #endregion

        #region VerifyOne

        public virtual VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            var memberContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        public virtual VerifyResult VerifyOneViaContext(VerifiableMemberContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyOneImpl(context);
        }

        protected abstract VerifyResult VerifyOneImpl(VerifiableMemberContext context);

        #endregion

        #region VerifyMany

        public virtual VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        #endregion
    }
}