using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using Cosmos.Collections;
using Cosmos.Reflection;
using Cosmos.Validation.Annotations;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace Cosmos.Validation.Objects
{
    public class ObjectContract : IObjectContract
    {
        private readonly Type _type;
        private readonly Dictionary<string, ObjectValueContract> _valueContracts;
        private readonly string[] _valueKeys;

        private readonly Attribute[] _attributes;

        private readonly ICustomAttributeReflectorProvider _reflectorProvider;
        private readonly ICustomObjectContractImpl _objectContractImpl;

        public ObjectContract(
            Type type,
            Dictionary<string, ObjectValueContract> valueContracts)
        {
            _objectContractImpl = null;
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _valueContracts = valueContracts ?? throw new ArgumentNullException(nameof(valueContracts));
            _valueKeys = _valueContracts.Keys.ToArray();
            _reflectorProvider = _type.GetReflector();
            _attributes = _reflectorProvider.GetCustomAttributes();

            IncludeAnnotationsForType = HasValidationAnnotationDefined(_attributes);
            ObjectKind = type.GetObjectKind();
        }

        public ObjectContract(ICustomObjectContractImpl objectContractImpl)
        {
            _objectContractImpl = objectContractImpl ?? throw new ArgumentNullException(nameof(objectContractImpl));
            _type = objectContractImpl.Type;
            _valueContracts = objectContractImpl.GetValueContractMap();
            _valueKeys = _valueContracts.Keys.ToArray();
            _reflectorProvider = null;
            _attributes = Arrays.Empty<Attribute>();

            IncludeAnnotationsForType = false;
            ObjectKind = objectContractImpl.ObjectKind;
        }

        public Type Type => _type;

        public ObjectKind ObjectKind { get; }

        public bool IsBasicType() => ObjectKind == ObjectKind.BasicType;

        #region GetValueContract

        public ObjectValueContract GetValueContract(string name)
        {
            if (_objectContractImpl is not null)
                return _objectContractImpl.GetValueContract(name);
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
            if (_objectContractImpl is not null)
                return _objectContractImpl.GetValueContract(propertyInfo);
            if (_valueContracts.TryGetValue(propertyInfo.Name, out var contract) && contract.ObjectValueKind == ObjectValueKind.Property)
                return contract;
            return default;
        }

        public ObjectValueContract GetValueContract(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                return default;
            if (_objectContractImpl is not null)
                return _objectContractImpl.GetValueContract(fieldInfo);
            if (_valueContracts.TryGetValue(fieldInfo.Name, out var contract) && contract.ObjectValueKind == ObjectValueKind.Field)
                return contract;
            return default;
        }

        public ObjectValueContract GetValueContract(int index)
        {
            if (index < 0 || index >= _valueKeys.Length)
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Index '{index}' is out of range.");
            return GetValueContract(_valueKeys[index]);
        }

        public IEnumerable<ObjectValueContract> GetAllValueContracts()
        {
            if (_objectContractImpl is not null)
                return _objectContractImpl.GetAllValueContracts();
            return _valueContracts.Values.AsReadOnly();
        }

        #endregion

        #region With

        public ObjectContext WithInstance(object instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));
            return new ObjectContext(instance, this);
        }

        public ObjectContext WithInstance(object instance, string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                return WithInstance(instance);
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));
            return new ObjectContext(instance, this, instanceName);
        }

        public ObjectContext WithDictionary(IDictionary<string, object> keyValueCollections)
        {
            if (keyValueCollections is null)
                throw new ArgumentNullException(nameof(keyValueCollections));
            return new ObjectContext(keyValueCollections, this);
        }

        public ObjectContext WithDictionary(IDictionary<string, object> keyValueCollections, string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                return WithDictionary(keyValueCollections);
            if (keyValueCollections is null)
                throw new ArgumentNullException(nameof(keyValueCollections));
            return new ObjectContext(keyValueCollections, this, instanceName);
        }

        #endregion

        #region Annotations

        private bool IncludeAnnotationsForType { get; }

        public bool IncludeAnnotations =>
            _objectContractImpl?.IncludeAnnotations ?? IncludeAnnotationsForType || _valueContracts.Values.Any(x => x.IncludeAnnotations);

        public IReadOnlyCollection<Attribute> Attributes => _objectContractImpl?.Attributes ?? _attributes;

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations()
        {
            if (_objectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is ValidationParameterAttribute annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _objectContractImpl.GetParameterAnnotations())
                    yield return annotation;
            }
        }

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations()
        {
            if (_objectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is IQuietVerifiableAnnotation annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _objectContractImpl.GetQuietVerifiableAnnotations())
                    yield return annotation;
            }
        }

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations()
        {
            if (_objectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is IStrongVerifiableAnnotation annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _objectContractImpl.GetStrongVerifiableAnnotations())
                    yield return annotation;
            }
        }

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            if (_objectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is TAttribute t)
                        yield return t;
            }
            else
            {
                foreach (var attribute in _objectContractImpl.GetAttributes<TAttribute>())
                    yield return attribute;
            }
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