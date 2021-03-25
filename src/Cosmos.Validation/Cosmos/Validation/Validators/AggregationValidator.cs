using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;

namespace Cosmos.Validation.Validators
{
    /// <summary>
    /// Aggregate validator. <br />
    /// The default built-in validator of Cosmos Validation is a collection of project validators based on types and rules, 
    /// annotation validators based on annotations, general custom validators based on registered custom validators, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AggregationValidator<T> : IValidator<T>, ICorrectValidator<T>
    {
        private readonly IValidationProjectManager _projectManager;
        private readonly IVerifiableObjectResolver _objectResolver;
        private readonly ICustomValidatorManager _customValidatorManager;
        private readonly Type _type;
        private readonly string _name;

        private readonly ValidationOptions _options;
        private readonly AnnotationValidator _annotationValidator;

        public AggregationValidator(
            IValidationProjectManager projectManager,
            IVerifiableObjectResolver objectResolver,
            ICustomValidatorManager customValidatorManager,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = customValidatorManager ?? throw new ArgumentNullException(nameof(customValidatorManager));
            _type = typeof(T);
            _name = string.Empty;
            _options = options;

            _annotationValidator = options.AnnotationEnabled ? AnnotationValidator.GetInstance(objectResolver, options) : null;
        }

        public AggregationValidator(
            string name,
            IValidationProjectManager projectManager,
            IVerifiableObjectResolver objectResolver,
            ICustomValidatorManager customValidatorManager,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = customValidatorManager ?? throw new ArgumentNullException(nameof(customValidatorManager));
            _type = typeof(T);
            _name = name;
            _options = options;

            _annotationValidator = options.AnnotationEnabled ? AnnotationValidator.GetInstance(objectResolver, options) : null;
        }

        /// <summary>
        /// Name of validation
        /// </summary>
        public string Name => string.IsNullOrEmpty(_name) ? "Anonymous Validator" : _name;

        /// <summary>
        /// Mark whether the validator is anonymous.
        /// </summary>
        public bool IsAnonymous => string.IsNullOrEmpty(_name);

        /// <inheritdoc />
        bool ICorrectValidator.IsTypeBinding => true;

        /// <inheritdoc />
        Type ICorrectValidator.SourceType => typeof(T);

        #region Verify

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult Verify(T instance)
        {
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null, result3 = null;
            var context = _objectResolver.Resolve(instance);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.Verify(context);

            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(context, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result3 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult IValidator.Verify(Type declaringType, object instance)
        {
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null, result3 = null;
            var context = _objectResolver.Resolve(declaringType, instance);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.Verify(context);

            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(context, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result3 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        #endregion

        #region VerifyOne

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public VerifyResult VerifyOne(object memberValue, string memberName)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue)
        {
            if (expression is null)
                return _options.ReturnNullReferenceOrSuccess();
            var memberName = PropertySelector.GetPropertyName(expression);
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            var memberContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance(object memberValue, string memberName, T instance)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance)));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, T instance)
        {
            if (expression is null)
                return _options.ReturnNullReferenceOrSuccess();
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(expression);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance)));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance)));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary(object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection)));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary<TVal>(Expression<Func<T, TVal>> expression, TVal memberValue, IDictionary<string, object> keyValueCollection)
        {
            if (expression is null)
                return _options.ReturnNullReferenceOrSuccess();
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(expression);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection)));
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOneInternal(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection)));
        }

        private VerifyResult VerifyOneInternal(VerifiableMemberContext memberContext)
        {
            VerifyResult result1 = null, result2 = null, result3 = null;
            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyOne(memberContext);
            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(memberContext, _customValidatorManager.ResolveAll());
            if (_options.AnnotationEnabled)
                result3 = _annotationValidator.VerifyOne(memberContext);
            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();
            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        #endregion

        #region VerifyMany

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null, result3 = null;
            var context = _objectResolver.Resolve<T>(keyValueCollections);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyMany(context.GetValueMap());

            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(context.GetValueMap(), _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result3 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        VerifyResult IValidator.VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            if (declaringType is null)
                return _options.ReturnNullReferenceOrSuccess();

            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null, result3 = null;
            var context = _objectResolver.Resolve(declaringType, keyValueCollections);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyMany(context.GetValueMap());

            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(context.GetValueMap(), _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result3 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        #endregion
    }
}