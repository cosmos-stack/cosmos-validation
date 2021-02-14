using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using Cosmos.Collections;
using Cosmos.Reflection;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public class ObjectContract
    {
        private readonly Type _type;
        private readonly Dictionary<string, ObjectValueContract> _valueContracts;

        private readonly Attribute[] _attributes;

        private readonly ICustomAttributeReflectorProvider _reflectorProvider;

        public ObjectContract(
            Type type,
            Dictionary<string, ObjectValueContract> valueContracts)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _valueContracts = valueContracts ?? throw new ArgumentNullException(nameof(valueContracts));
            _reflectorProvider = _type.GetReflector();
            _attributes = _reflectorProvider.GetCustomAttributes();

            IncludeAnnotationsForType = HasValidationAnnotationDefined(_attributes);
            ObjectKind = type.GetObjectKind();
        }

        #region GetValueContract

        public ObjectValueContract GetValueContract(string name)
        {
            if (ObjectKind == ObjectKind.BasicType)
                return _valueContracts[ObjectValueContract.BASIC_TYPE];
            if (_valueContracts.TryGetValue(name, out var contract))
                return contract;
            return default;
        }

        public ObjectValueContract GetValueContract(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                return default;
            if (_valueContracts.TryGetValue(propertyInfo.Name, out var contract) && contract.ObjectValueKind == ObjectValueKind.Property)
                return contract;
            return default;
        }

        public ObjectValueContract GetValueContract(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                return default;
            if (_valueContracts.TryGetValue(fieldInfo.Name, out var contract) && contract.ObjectValueKind == ObjectValueKind.Field)
                return contract;
            return default;
        }

        public IEnumerable<ObjectValueContract> GetAllValueContracts()
        {
            return _valueContracts.Values.AsReadOnly();
        }

        #endregion

        public Type Type => _type;

        public ObjectKind ObjectKind { get; }

        public bool IsBasicType() => ObjectKind == ObjectKind.BasicType;

        #region Annotations

        private bool IncludeAnnotationsForType { get; }
        public bool IncludeAnnotations => IncludeAnnotationsForType || _valueContracts.Values.Any(x => x.IncludeAnnotations);

        public IReadOnlyCollection<Attribute> Attributes => _attributes;

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations()
        {
            foreach (var attribute in _attributes)
                if (attribute is ValidationParameterAttribute annotation)
                    yield return annotation;
        }

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations()
        {
            foreach (var attribute in _attributes)
                if (attribute is IQuietVerifiableAnnotation annotation)
                    yield return annotation;
        }

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations()
        {
            foreach (var attribute in _attributes)
                if (attribute is IStrongVerifiableAnnotation annotation)
                    yield return annotation;
        }

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            foreach (var attribute in _attributes)
                if (attribute is TAttribute t)
                    yield return t;
        }

        private static bool HasValidationAnnotationDefined(Attribute[] attributes)
        {
            foreach (var attribute in attributes)
            {
                if (attribute is ValidationParameterAttribute)
                    return true;
                if (Types.IsInterfaceDefined<Attribute, IFlagAnnotation>(attribute))
                    return true;
                if (Types.IsInterfaceDefined<Attribute, IVerifiable>(attribute))
                    return true;
            }

            return false;
        }

        #endregion
    }
}