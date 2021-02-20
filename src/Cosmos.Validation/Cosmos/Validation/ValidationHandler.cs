using System;
using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;

namespace Cosmos.Validation
{
    public class ValidationHandler
    {
        private readonly Dictionary<(int, int), IProject> _namedTypeProjects = new();
        private readonly Dictionary<int, IProject> _typedProjects = new();

        private readonly IValidationObjectResolver _objectResolver;

        internal ValidationHandler(IEnumerable<IProject> projects, IValidationObjectResolver objectResolver)
        {
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));

            if (projects is not null)
                foreach (var project in projects)
                    UpdateProject(project);
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

        #region Verify

        public VerifyResult Verify(Type declaringType, object instance)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            if (_typedProjects.TryGetValue(declaringType.GetHashCode(), out var result))
                return result.Verify(_objectResolver.Resolve(declaringType, instance));

            return VerifyResult.UnexpectedType;
        }

        public VerifyResult Verify(Type declaringType, object instance, string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return Verify(declaringType, instance);

            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            if (_namedTypeProjects.TryGetValue((declaringType.GetHashCode(), projectName.GetHashCode()), out var result))
                return result.Verify(_objectResolver.Resolve(declaringType, instance));

            return VerifyResult.UnexpectedType;
        }

        public VerifyResult Verify<T>(T instance) => Verify(typeof(T), instance);

        public VerifyResult Verify<T>(T instance, string projectName) => Verify(typeof(T), instance, projectName);

        #endregion

        #region VerifyOne

        public VerifyResult VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));
            
            if (memberType is null)
                throw new ArgumentNullException(nameof(memberType));

            if (_typedProjects.TryGetValue(declaringType.GetHashCode(), out var result))
            {
                var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
                var valueContext = ObjectValueContext.Create(memberValue, valueContract);
                return result.VerifyOne(valueContext);
            }

            return VerifyResult.UnexpectedType;
        }

        public VerifyResult VerifyOne(Type declaringType, Type memberType, object memberValue, string memberName, string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return VerifyOne(declaringType, memberType,memberValue,memberName);
            
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));
            
            if (memberType is null)
                throw new ArgumentNullException(nameof(memberType));

            if (_namedTypeProjects.TryGetValue((declaringType.GetHashCode(), projectName.GetHashCode()), out var result))
            {
                var valueContract = ObjectContractManager.Resolve(declaringType)?.GetValueContract(memberName);
                var valueContext = ObjectValueContext.Create(memberValue, valueContract);
                return result.VerifyOne(valueContext);
            }

            return VerifyResult.UnexpectedType;
        }

        public VerifyResult VerifyOne<TP, TM>(object memberValue, string memberName) 
            => VerifyOne(typeof(TP), typeof(TM), memberValue, memberName);

        public VerifyResult VerifyOne<TP, TM>(object memberValue, string memberName, string projectName) 
            => VerifyOne(typeof(TP), typeof(TM), memberValue, memberName, projectName);
        
        #endregion

        #region VerifyMany

        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));
            
            if (_typedProjects.TryGetValue(declaringType.GetHashCode(), out var result))
                return result.Verify(_objectResolver.Resolve(declaringType, keyValueCollections));

            return VerifyResult.UnexpectedType;
        } 
        
        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections, string projectName)
        {
            if (string.IsNullOrWhiteSpace(projectName))
                return VerifyMany(declaringType, keyValueCollections);

            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            if (_namedTypeProjects.TryGetValue((declaringType.GetHashCode(), projectName.GetHashCode()), out var result))
                return result.Verify(_objectResolver.Resolve(declaringType, keyValueCollections));

            return VerifyResult.UnexpectedType;
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