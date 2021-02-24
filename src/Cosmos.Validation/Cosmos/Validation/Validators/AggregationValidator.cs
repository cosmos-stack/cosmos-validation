using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;

namespace Cosmos.Validation.Validators
{
    public class AggregationValidator<T> : IValidator<T>, ICorrectValidator<T>
    {
        private readonly IValidationProjectManager _projectManager;
        private readonly IValidationObjectResolver _objectResolver;
        private readonly ICustomValidatorManager _customValidatorManager;
        private readonly Type _type;
        private readonly string _name;

        private readonly ValidationOptions _options;
        private readonly AnnotationValidator _annotationValidator;
        
        public AggregationValidator(
            IValidationProjectManager projectManager,
            IValidationObjectResolver objectResolver,
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
            IValidationObjectResolver objectResolver,
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

        public string Name => string.IsNullOrEmpty(_name) ? "Anonymous Validator" : _name;

        public bool IsAnonymous => string.IsNullOrEmpty(_name);

        bool ICorrectValidator.IsTypeBinding => true;

        bool ICorrectValidator.IsFluentValidator { get; set; } = false;

        #region Verify

        public VerifyResult Verify(T instance)
        {
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null;
            var context = _objectResolver.Resolve(instance);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.Verify(context, _options);
            else if (_options.CustomValidatorEnabled)
                result1 = CorrectEngine.ValidViaCustomValidators(context, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result2 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            if (result2 is null)
                return result1;

            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        VerifyResult IValidator.Verify(Type declaringType, object instance)
        {
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null;
            var context = _objectResolver.Resolve(declaringType, instance);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.Verify(context, _options);
            else if (_options.CustomValidatorEnabled)
                result1 = CorrectEngine.ValidViaCustomValidators(context, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result2 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            if (result2 is null)
                return result1;

            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        #endregion

        #region VerifyOne

        public VerifyResult VerifyOne(Type memberType, object memberValue, string memberName)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null;
            var valueContract = ObjectContractManager.Resolve<T>()?.GetValueContract(memberName);

            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);

            var valueContext = ObjectValueContext.Create(memberValue, valueContract);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyOne(valueContext, _options);
            else if (_options.CustomValidatorEnabled)
                result1 = CorrectEngine.ValidViaCustomValidators(valueContext, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result2 = _annotationValidator.VerifyOne(valueContext);

            if (result1 is null && result2 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            if (result2 is null)
                return result1;

            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        public VerifyResult VerifyOne<TVal>(Expression<Func<T, TVal>> propertySelector, object memberValue)
        {
            if (propertySelector is null)
                return _options.ReturnNullReferenceOrSuccess();

            var memberName = PropertySelector.GetPropertyName(propertySelector);

            VerifyResult result1 = null, result2 = null;
            var valueContract = ObjectContractManager.Resolve<T>()?.GetValueContract(memberName);

            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);

            var valueContext = ObjectValueContext.Create(memberValue, valueContract);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyOne(valueContext, _options);
            else if (_options.CustomValidatorEnabled)
                result1 = CorrectEngine.ValidViaCustomValidators(valueContext, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result2 = _annotationValidator.VerifyOne(valueContext);

            if (result1 is null && result2 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            if (result2 is null)
                return result1;

            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        VerifyResult IValidator.VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null;
            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);

            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);

            var valueContext = ObjectValueContext.Create(memberValue, valueContract);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyOne(valueContext, _options);
            else if (_options.CustomValidatorEnabled)
                result1 = CorrectEngine.ValidViaCustomValidators(valueContext, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result2 = _annotationValidator.VerifyOne(valueContext);

            if (result1 is null && result2 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            if (result2 is null)
                return result1;

            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        #endregion

        #region VerifyMany

        public VerifyResult VerifyMany(IDictionary<string, object> keyValueCollections)
        {
            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null;
            var context = _objectResolver.Resolve<T>(keyValueCollections);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyMany(context.GetValueMap(), _options);
            else if (_options.CustomValidatorEnabled)
                result1 = CorrectEngine.ValidViaCustomValidators(context.GetValueMap(), _customValidatorManager.ResolveAll());
            
            if (_options.AnnotationEnabled)
                result2 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            if (result2 is null)
                return result1;

            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        VerifyResult IValidator.VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            if (declaringType is null)
                return _options.ReturnNullReferenceOrSuccess();

            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();

            VerifyResult result1 = null, result2 = null;
            var context = _objectResolver.Resolve(declaringType, keyValueCollections);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyMany(context.GetValueMap(), _options);
            else if (_options.CustomValidatorEnabled)
                result1 = CorrectEngine.ValidViaCustomValidators(context.GetValueMap(), _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result2 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null)
                return _options.ReturnUnregisterProjectForSuchTypeOrSuccess();

            if (result2 is null)
                return result1;

            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        #endregion
    }
}