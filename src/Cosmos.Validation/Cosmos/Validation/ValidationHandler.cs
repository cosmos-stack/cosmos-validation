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

        internal ValidationHandler(IEnumerable<IProject> projects,IValidationObjectResolver objectResolver)
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
        
        public VerifyResult Verify(Type type,object instance)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (_typedProjects.TryGetValue(type.GetHashCode(), out var result))
                return result.Verify(_objectResolver.Resolve(type, instance));
            
            return VerifyResult.UnexpectedType;
        }

        public VerifyResult Verify(Type type,object instance,string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Verify(type,instance);
            
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            
            if (_namedTypeProjects.TryGetValue((type.GetHashCode(), name.GetHashCode()), out var result))
                return result.Verify(_objectResolver.Resolve(type, instance));
            
            return VerifyResult.UnexpectedType;
        }

        public VerifyResult Verify<T>(T instance) => Verify(typeof(T), instance);

        public VerifyResult Verify<T>(T instance, string name) => Verify(typeof(T), instance, name);

        internal ValidationHandler Merge(IEnumerable<IProject> projects)
        {
            if (projects is not null)
                foreach (var project in projects)
                    UpdateProject(project);
            return this;
        }
    }
}