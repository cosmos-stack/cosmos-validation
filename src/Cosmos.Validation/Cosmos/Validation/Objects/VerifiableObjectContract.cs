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
    public class VerifiableObjectContract : IVerifiableObjectContract
    {
        private readonly Dictionary<string, VerifiableMemberContract> _memberContracts;
        private readonly string[] _valueKeys;

        private readonly Attribute[] _attributes;

        private readonly ICustomAttributeReflectorProvider _reflectorProvider;
        private readonly ICustomVerifiableObjectContractImpl _verifiableObjectContractImpl;

        public VerifiableObjectContract(
            Type type,
            Dictionary<string, VerifiableMemberContract> memberContracts)
        {
            _verifiableObjectContractImpl = null;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            ObjectKind = type.GetObjectKind();
            IsBasicType = ObjectKind == VerifiableObjectKind.BasicType;
            
            _memberContracts = memberContracts ?? throw new ArgumentNullException(nameof(memberContracts));
            _valueKeys = _memberContracts.Keys.ToArray();
            
            _reflectorProvider = Type.GetReflector();
            _attributes = _reflectorProvider.GetCustomAttributes();
            IncludeAnnotationsForType = HasValidationAnnotationDefined(_attributes);
        }

        public VerifiableObjectContract(ICustomVerifiableObjectContractImpl contractImpl)
        {
            _verifiableObjectContractImpl = contractImpl ?? throw new ArgumentNullException(nameof(contractImpl));
            Type = contractImpl.Type;
            ObjectKind = contractImpl.ObjectKind;
            IsBasicType = contractImpl.IsBasicType;
            
            _memberContracts = contractImpl.GetMemberContractMap();
            _valueKeys = _memberContracts.Keys.ToArray();
            
            _reflectorProvider = null;
            _attributes = Arrays.Empty<Attribute>();
            IncludeAnnotationsForType = false;
        }

        public Type Type { get; }

        public VerifiableObjectKind ObjectKind { get; }

        public bool IsBasicType { get; }

        #region WithInstance / WithDictionary

        public VerifiableObjectContext WithInstance(object instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));
            return new VerifiableObjectContext(instance, this);
        }

        public VerifiableObjectContext WithInstance(object instance, string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                return WithInstance(instance);
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));
            return new VerifiableObjectContext(instance, this, instanceName);
        }

        public VerifiableObjectContext WithDictionary(IDictionary<string, object> keyValueCollection)
        {
            if (keyValueCollection is null)
                throw new ArgumentNullException(nameof(keyValueCollection));
            return new VerifiableObjectContext(keyValueCollection, this);
        }

        public VerifiableObjectContext WithDictionary(IDictionary<string, object> keyValueCollection, string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                return WithDictionary(keyValueCollection);
            if (keyValueCollection is null)
                throw new ArgumentNullException(nameof(keyValueCollection));
            return new VerifiableObjectContext(keyValueCollection, this, instanceName);
        }

        #endregion

        #region Value

        public object GetValue(object instance, string memberName)
        {
            return GetMemberContract(memberName)?.GetValue(instance);
        }

        public object GetValue(object instance, int memberIndex)
        {
            return GetMemberContract(memberIndex)?.GetValue(instance);
        }

        public object GetValue(IDictionary<string, object> keyValueCollection, string memberName)
        {
            return GetMemberContract(memberName)?.GetValue(keyValueCollection);
        }

        public object GetValue(IDictionary<string, object> keyValueCollection, int memberIndex)
        {
            return GetMemberContract(memberIndex)?.GetValue(keyValueCollection);
        }

        #endregion

        #region MemberContract

        public VerifiableMemberContract GetMemberContract(string memberName)
        {
            if (_verifiableObjectContractImpl is not null)
                return _verifiableObjectContractImpl.GetMemberContract(memberName);
            if (ObjectKind == VerifiableObjectKind.BasicType)
                return _memberContracts[VerifiableMemberContract.BASIC_TYPE];
            if (_memberContracts.TryGetValue(memberName, out var contract))
                return contract;
            return default;
        }

        public VerifiableMemberContract GetMemberContract(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                return default;
            if (_verifiableObjectContractImpl is not null)
                return _verifiableObjectContractImpl.GetMemberContract(propertyInfo);
            if (_memberContracts.TryGetValue(propertyInfo.Name, out var contract) && contract.MemberKind == VerifiableMemberKind.Property)
                return contract;
            return default;
        }

        public VerifiableMemberContract GetMemberContract(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                return default;
            if (_verifiableObjectContractImpl is not null)
                return _verifiableObjectContractImpl.GetMemberContract(fieldInfo);
            if (_memberContracts.TryGetValue(fieldInfo.Name, out var contract) && contract.MemberKind == VerifiableMemberKind.Field)
                return contract;
            return default;
        }

        public VerifiableMemberContract GetMemberContract(int memberIndex)
        {
            if (memberIndex < 0 || memberIndex >= _valueKeys.Length)
                throw new ArgumentOutOfRangeException(nameof(memberIndex), memberIndex, $"Index '{memberIndex}' is out of range.");
            return GetMemberContract(_valueKeys[memberIndex]);
        }

        public IEnumerable<VerifiableMemberContract> GetMemberContracts()
        {
            if (_verifiableObjectContractImpl is not null)
                return _verifiableObjectContractImpl.GetMemberContracts();
            return _memberContracts.Values.AsReadOnly();
        }

        #endregion

        #region Annotation / Attribute

        private bool IncludeAnnotationsForType { get; }

        public bool IncludeAnnotations =>
            (_verifiableObjectContractImpl?.IncludeAnnotations ?? IncludeAnnotationsForType) ||
            _memberContracts.Values.Any(x => x.IncludeAnnotations);

        public IReadOnlyCollection<Attribute> Attributes => _verifiableObjectContractImpl?.Attributes ?? _attributes;

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            if (_verifiableObjectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is TAttribute t)
                        yield return t;
            }
            else
            {
                foreach (var attribute in _verifiableObjectContractImpl.GetAttributes<TAttribute>())
                    yield return attribute;
            }
        }

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations()
        {
            if (_verifiableObjectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is ValidationParameterAttribute annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _verifiableObjectContractImpl.GetParameterAnnotations())
                    yield return annotation;
            }
        }

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations()
        {
            if (_verifiableObjectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is IQuietVerifiableAnnotation annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _verifiableObjectContractImpl.GetQuietVerifiableAnnotations())
                    yield return annotation;
            }
        }

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations()
        {
            if (_verifiableObjectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is IStrongVerifiableAnnotation annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _verifiableObjectContractImpl.GetStrongVerifiableAnnotations())
                    yield return annotation;
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

        #region Expose

        internal ICustomVerifiableObjectContractImpl ExposeInternalImpl() => _verifiableObjectContractImpl;

        #endregion
    }
}