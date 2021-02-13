using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    public class ValidationProvider : IValidationProvider, ICorrectProvider
    {
        private readonly IValidationProjectManager _projectManager;
        private readonly IValidationObjectResolver _objectResolver;
        private readonly CustomValidatorManager _customValidatorManager;

        public ValidationProvider(
            IValidationProjectManager projectManager,
            IValidationObjectResolver objectResolver)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = new CustomValidatorManager();
        }

        internal const string DefaultName = "Default Validation Provider";

        internal static bool IsDefault(string name) => name == DefaultName;

        public IValidator Resolve(Type type)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);

#if NETFRAMEWORK
            return TypeVisit.CreateInstance<IValidator>(v, _projectManager, _objectResolver);
#else
            var arguments = new List<ArgumentDescriptor>
            {
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IValidationObjectResolver))
            };

            return TypeVisit.CreateInstance<IValidator>(v, arguments);
#endif
        }

        public IValidator Resolve(Type type, string name)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);
#if NETFRAMEWORK
            return TypeVisit.CreateInstance<IValidator>(v, name, _projectManager, _objectResolver);
#else
            var arguments = new List<ArgumentDescriptor>
            {
                new("name", name, TypeClass.StringClazz),
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IValidationObjectResolver))
            };

            return TypeVisit.CreateInstance<IValidator>(v, arguments);
#endif
        }

        public IValidator Resolve<T>() => Resolve(typeof(T));

        public IValidator Resolve<T>(string name) => Resolve(typeof(T), name);

        IValidationProjectManager ICorrectProvider.ExposeProjectManager()
        {
            return _projectManager;
        }

        IValidationObjectResolver ICorrectProvider.ExposeObjectResolver()
        {
            return _objectResolver;
        }

        CustomValidatorManager ICorrectProvider.ExposeCustomValidatorManager()
        {
            return _customValidatorManager;
        }

        void ICorrectProvider.RegisterValidator<TValidator>()
        {
            _customValidatorManager.Register<TValidator>();
        }

        void ICorrectProvider.RegisterValidator<TValidator, T>()
        {
            _customValidatorManager.Register<TValidator, T>();
        }

        void ICorrectProvider.RegisterValidator(CustomValidator validator)
        {
            _customValidatorManager.Register(validator);
        }

        void ICorrectProvider.RegisterValidator<T>(CustomValidator<T> validator)
        {
            _customValidatorManager.Register(validator);
        }
    }
}