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
        private readonly ICustomValidatorManager _customValidatorManager;

        private ValidationOptions _options;

        static ValidationProvider()
        {
#if !NETFRAMEWORK
            NatashaInitializer.InitializeAndPreheating();
#endif
        }

        public ValidationProvider(
            IValidationProjectManager projectManager,
            IValidationObjectResolver objectResolver,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = new CustomValidatorManager();
            _options = options ?? new ValidationOptions();
        }

        internal const string DefaultName = "Default Validation Provider";
        internal const string MainName = "Main Validation Provider";

        internal static bool IsDefault(string name) => name == DefaultName;

        string ICorrectProvider.Name { get; set; } = DefaultName;

        public IValidator Resolve(Type type)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);
#if !NETFRAMEWORK
            var args = new List<ArgumentDescriptor>
            {
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IValidationObjectResolver)),
                new("customValidatorManager", _customValidatorManager, typeof(ICustomValidatorManager)),
                new("options", _options, typeof(ValidationOptions))
            };
            
            return TypeVisit.CreateInstance<IValidator>(v, args);
#else
            return TypeVisit.CreateInstance<IValidator>(v, _projectManager, _objectResolver, _customValidatorManager, _options);
#endif
        }

        public IValidator Resolve(Type type, string name)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);
#if !NETFRAMEWORK
            var args = new List<ArgumentDescriptor>
            {
                new("name", name, typeof(string)),
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IValidationObjectResolver)),
                new("customValidatorManager", _customValidatorManager, typeof(ICustomValidatorManager)),
                new("options", _options, typeof(ValidationOptions))
            };
            
            return TypeVisit.CreateInstance<IValidator>(v, args);
#else
            return TypeVisit.CreateInstance<IValidator>(v, name, _projectManager, _objectResolver, _customValidatorManager, _options);
#endif
        }

        public IValidator<T> Resolve<T>() => (IValidator<T>) Resolve(typeof(T));

        public IValidator<T> Resolve<T>(string name) => (IValidator<T>) Resolve(typeof(T), name);

        IValidationProjectManager ICorrectProvider.ExposeProjectManager() => _projectManager;

        IValidationObjectResolver ICorrectProvider.ExposeObjectResolver() => _objectResolver;

        ICustomValidatorManager ICorrectProvider.ExposeCustomValidatorManager() => _customValidatorManager;

        ValidationOptions ICorrectProvider.ExposeValidationOptions() => _options;

        public void UpdateOptions(ValidationOptions options)
        {
            if (options is not null)
                _options = options;
        }

        public void UpdateOptions(Action<ValidationOptions> optionAct)
        {
            optionAct?.Invoke(_options);
        }
    }
}