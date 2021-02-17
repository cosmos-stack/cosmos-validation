using System;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;

namespace Cosmos.Validation.Validators
{
    public class AggregationValidator<T> : IValidator<T>, ICorrectValidator<T>
    {
        private readonly IValidationProjectManager _projectManager;
        private readonly IValidationObjectResolver _objectResolver;
        private readonly Type _type;
        private readonly string _name;

        private readonly ValidationOptions _options;
        private readonly AnnotationValidator _annotationValidator;

        public AggregationValidator(
            IValidationProjectManager projectManager,
            IValidationObjectResolver objectResolver,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _type = typeof(T);
            _name = string.Empty;
            _options = options;

            _annotationValidator = options.IncludeAnnotation ? AnnotationValidator.GetInstance(objectResolver) : null;
        }

        public AggregationValidator(
            string name,
            IValidationProjectManager projectManager,
            IValidationObjectResolver objectResolver,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _type = typeof(T);
            _name = name;
            _options = options;

            _annotationValidator = options.IncludeAnnotation ? AnnotationValidator.GetInstance(objectResolver) : null;
        }

        #region Verify

        public VerifyResult Verify(T instance)
        {
            if (instance is null)
                return BuildInVerifyResults.NullObjectReference;

            VerifyResult result1 = null, result2 = null;
            var context = _objectResolver.Resolve(instance);

            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.Verify(context);

            if (_options.IncludeAnnotation)
                result2 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null)
                return BuildInVerifyResults.UnregisterProjectForSuchType;

            if (result2 is null)
                return result1;
            
            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        public VerifyResult Verify(Type type, object instance)
        {
            if (instance is null)
                return BuildInVerifyResults.NullObjectReference;
            
            VerifyResult result1 = null, result2 = null;
            var context =_objectResolver.Resolve(type, instance);
            
            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.Verify(context);
            
            if (_options.IncludeAnnotation)
                result2 = _annotationValidator.Verify(context);

            if (result1 is null && result2 is null)
                return BuildInVerifyResults.UnregisterProjectForSuchType;

            if (result2 is null)
                return result1;
            
            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        #endregion
        
        #region VerifyOne

        public VerifyResult VerifyOne(T instance, string memberName)
        {
            if (instance is null)
                return BuildInVerifyResults.NullObjectReference;
            
            VerifyResult result1 = null, result2 = null;
            var context = _objectResolver.Resolve(instance);
            var valueContext = context.GetValue(memberName);
            
            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyOne(valueContext);

            if (_options.IncludeAnnotation)
                result2 = _annotationValidator.VerifyOne(valueContext);

            if (result1 is null && result2 is null)
                return BuildInVerifyResults.UnregisterProjectForSuchType;

            if (result2 is null)
                return result1;
            
            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        public VerifyResult VerifyOne(Type type, object instance, string memberName)
        {
            if (instance is null)
                return BuildInVerifyResults.NullObjectReference;
            
            VerifyResult result1 = null, result2 = null;
            var context = _objectResolver.Resolve(instance);
            var valueContext = context.GetValue(memberName);
            
            if (_projectManager.TryResolve(_type, _name, out var project))
                result1 = project.VerifyOne(valueContext);

            if (_options.IncludeAnnotation)
                result2 = _annotationValidator.VerifyOne(valueContext);
            
            if (result1 is null && result2 is null)
                return BuildInVerifyResults.UnregisterProjectForSuchType;

            if (result2 is null)
                return result1;
            
            if (result1 is null)
                return result2;

            return VerifyResult.Merge(result1, result2);
        }

        #endregion

        public string Name => string.IsNullOrEmpty(_name) ? "Anonymous Validator" : _name;

        public bool IsAnonymous => string.IsNullOrEmpty(_name);
    }
}