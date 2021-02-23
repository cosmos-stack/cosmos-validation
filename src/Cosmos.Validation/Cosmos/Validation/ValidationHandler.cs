using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cosmos.Collections;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    public class ValidationHandler
    {
        private readonly Dictionary<(int, int), IProject> _namedTypeProjects = new();
        private readonly Dictionary<int, IProject> _typedProjects = new();

        private readonly IValidationObjectResolver _objectResolver;
        private readonly ValidationOptions _options;

        internal ValidationHandler(IEnumerable<IProject> projects, IValidationObjectResolver objectResolver, ValidationOptions options)
        {
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (projects is not null)
                foreach (var project in projects)
                    UpdateProject(project);

            AnnotationValidator = options.AnnotationEnabled
                ? AnnotationValidator.GetInstance(_objectResolver, _options)
                : default;
        }

        private void UpdateProject(IProject project)
        {
            if (project is not null)
            {
                switch (project.Class)
                {
                    case ProjectClass.Typed:
                        _typedProjects.AddValueOrOverride(project.Type.GetHashCode(), project);
                        break;

                    case ProjectClass.Named:
                        _namedTypeProjects.AddValueOrOverride((project.Type.GetHashCode(), project.Name.GetHashCode()), project);
                        break;

                    default:
                        throw new InvalidOperationException("Unknown validation project.");
                }
            }
        }

        private AnnotationValidator AnnotationValidator { get; set; }

        #region Verify

        public VerifyResult Verify(Type declaringType, object instance)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));
            return Verify(_objectResolver.Resolve(declaringType, instance), "");
        }

        public VerifyResult Verify(Type declaringType, object instance, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));
            return Verify(_objectResolver.Resolve(declaringType, instance), projectName);
        }

        public VerifyResult Verify<T>(T instance) => Verify(typeof(T), instance);

        public VerifyResult Verify<T>(T instance, string projectName) => Verify(typeof(T), instance, projectName);

        internal VerifyResult Verify(ObjectContext context, string projectName = "")
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            IProject project;
            VerifyResult result1 = null, result2 = null;

            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (_typedProjects.TryGetValue(context.Type.GetHashCode(), out project))
                    result1 = project.Verify(context, _options);
            }
            else
            {
                if (_namedTypeProjects.TryGetValue((context.Type.GetHashCode(), projectName.GetHashCode()), out project))
                    result1 = project.Verify(context, _options);
            }

            if (_options.AnnotationEnabled)
                result2 = AnnotationValidator.Verify(context);

            if (result1 is null && result2 is null)
                return _options.ReturnUnexpectedTypeOrSuccess();

            if (result2 is null) return result1;
            if (result1 is null) return result2;
            return VerifyResult.Merge(result1, result2);
        }

        #endregion

        #region VerifyOne

        public VerifyResult VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            if (memberType is null)
                throw new ArgumentNullException(nameof(memberType));

            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);

            return VerifyOne(valueContext, declaringType, "");
        }

        public VerifyResult VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            if (memberType is null)
                throw new ArgumentNullException(nameof(memberType));

            var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);

            return VerifyOne(valueContext, declaringType, projectName);
        }

        public VerifyResult VerifyOne<T, TVal>(object memberValue, string memberName) => VerifyOne(typeof(T), typeof(TVal), memberValue, memberName);

        public VerifyResult VerifyOne<T, TVal>(object memberValue, string memberName, string projectName) => VerifyOne(typeof(T), typeof(TVal), memberValue, memberName, projectName);

        public VerifyResult VerifyOne<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var valueContract = ObjectContractManager.Resolve<T>()?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);

            return VerifyOne(valueContext, typeof(T),"");
        }

        public VerifyResult VerifyOne<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, string projectName)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var valueContract = ObjectContractManager.Resolve<T>()?.GetValueContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = ObjectValueContext.Create(memberValue, valueContract);

            return VerifyOne(valueContext, typeof(T), projectName);
        }

        internal VerifyResult VerifyOne(ObjectValueContext context, Type declaringType = default, string projectName = "")
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            IProject project;
            VerifyResult result1 = null, result2 = null;

            if (string.IsNullOrWhiteSpace(projectName))
            {
                var t = declaringType ?? context.DeclaringType;
                if (_typedProjects.TryGetValue(t.GetHashCode(), out project))
                    result1 = project.VerifyOne(context, _options);
            }
            else
            {
                var t = declaringType ?? context.DeclaringType;
                if (_namedTypeProjects.TryGetValue((t.GetHashCode(), projectName.GetHashCode()), out project))
                    result1 = project.VerifyOne(context, _options);
            }

            if (_options.AnnotationEnabled)
                result2 = AnnotationValidator.VerifyOne(context);

            if (result1 is null && result2 is null)
                return _options.ReturnUnexpectedTypeOrSuccess();

            if (result2 is null) return result1;
            if (result1 is null) return result2;
            return VerifyResult.Merge(result1, result2);
        }

        #endregion

        #region VerifyMany

        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var context = _objectResolver.Resolve(declaringType, keyValueCollections);

            return VerifyMany(context,"");
        }

        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var context = _objectResolver.Resolve(declaringType, keyValueCollections);

            return VerifyMany(context, projectName);
        }

        public VerifyResult VerifyMany<T>(IDictionary<string, object> keyValueCollections) => VerifyMany(typeof(T), keyValueCollections);

        public VerifyResult VerifyMany<T>(IDictionary<string, object> keyValueCollections, string projectName) => VerifyMany(typeof(T), keyValueCollections, projectName);

        internal VerifyResult VerifyMany(ObjectContext context, string projectName = "")
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            IProject project;
            VerifyResult result1 = null, result2 = null;

            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (_typedProjects.TryGetValue(context.Type.GetHashCode(), out project))
                    result1 = project.VerifyMany(context.GetValueMap(), _options);
            }
            else
            {
                if (_namedTypeProjects.TryGetValue((context.Type.GetHashCode(), projectName.GetHashCode()), out project))
                    result1 = project.VerifyMany(context.GetValueMap(), _options);
            }

            if (_options.AnnotationEnabled)
                result2 = AnnotationValidator.VerifyMany(context);

            if (result1 is null && result2 is null)
                return _options.ReturnUnexpectedTypeOrSuccess();

            if (result2 is null) return result1;
            if (result1 is null) return result2;
            return VerifyResult.Merge(result1, result2);
        }

        #endregion

        internal ValidationHandler Merge(IEnumerable<IProject> projects)
        {
            if (projects is not null)
                foreach (var project in projects)
                    UpdateProject(project);
            return this;
        }
    }
}