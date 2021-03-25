using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    /// <summary>
    /// Custom validator.
    /// </summary>
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

        /// <summary>
        /// Name of validation
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Mark whether the validator is anonymous.
        /// </summary>
        public bool IsAnonymous => string.IsNullOrEmpty(Name);

        /// <inheritdoc />
        bool ICorrectValidator.IsTypeBinding => false;

        /// <inheritdoc />
        Type ICorrectValidator.SourceType => TypeClass.ObjectClazz;

        #region Verify

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual VerifyResult Verify(Type declaringType, object instance)
        {
            var context = _objectResolver.Resolve(declaringType, instance);
            return VerifyImpl(context);
        }

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual VerifyResult VerifyViaContext(VerifiableObjectContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyImpl(context);
        }

        protected abstract VerifyResult VerifyImpl(VerifiableObjectContext context);

        #endregion

        #region VerifyOne

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public virtual VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            var memberContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
        {
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        public virtual VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));
            return VerifyOneImpl(memberContext);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual VerifyResult VerifyOneViaContext(VerifiableMemberContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyOneImpl(context);
        }

        protected abstract VerifyResult VerifyOneImpl(VerifiableMemberContext context);

        #endregion

        #region VerifyMany

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public virtual VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyImpl(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        #endregion

        #region VerifyViaContext

        internal virtual VerifyResult VerifyViaContext(VerifiableOpsContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (context.OpsMode == VerifiableOpsMode.Object)
                return VerifyImpl(context.VerifiableObjectContext);
            if (context.OpsMode == VerifiableOpsMode.Member)
                return VerifyOneImpl(context.VerifiableMemberContext);
            return VerifyResult.Success;
        }

        #endregion
    }
}