using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using CosmosStack.Collections;
using CosmosStack.Reflection;
using CosmosStack.Validation.Annotations;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable Object Contract <br />
    /// 可验证对象约定
    /// </summary>
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

        /// <inheritdoc />
        public Type Type { get; }

        /// <inheritdoc />
        public VerifiableObjectKind ObjectKind { get; }

        /// <inheritdoc />
        public bool IsBasicType { get; }

        #region WithInstance / WithDictionary

        /// <summary>
        /// With instance <br />
        /// 使用实例
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifiableObjectContext WithInstance(object instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));
            return new VerifiableObjectContext(instance, this);
        }

        /// <summary>
        /// With instance <br />
        /// 使用实例
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifiableObjectContext WithInstance(object instance, string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                return WithInstance(instance);
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));
            return new VerifiableObjectContext(instance, this, instanceName);
        }

        /// <summary>
        /// With dictionary <br />
        /// 使用字典
        /// </summary>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifiableObjectContext WithDictionary(IDictionary<string, object> keyValueCollection)
        {
            if (keyValueCollection is null)
                throw new ArgumentNullException(nameof(keyValueCollection));
            return new VerifiableObjectContext(keyValueCollection, this);
        }

        /// <summary>
        /// With dictionary <br />
        /// 使用字典
        /// </summary>
        /// <param name="keyValueCollection"></param>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <inheritdoc />
        public object GetValue(object instance, string memberName)
        {
            return GetMemberContract(memberName)?.GetValue(instance);
        }

        /// <inheritdoc />
        public object GetValue(object instance, int memberIndex)
        {
            return GetMemberContract(memberIndex)?.GetValue(instance);
        }

        /// <inheritdoc />
        public object GetValue(IDictionary<string, object> keyValueCollection, string memberName)
        {
            return GetMemberContract(memberName)?.GetValue(keyValueCollection);
        }

        /// <inheritdoc />
        public object GetValue(IDictionary<string, object> keyValueCollection, int memberIndex)
        {
            return GetMemberContract(memberIndex)?.GetValue(keyValueCollection);
        }

        #endregion

        #region MemberContract

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public VerifiableMemberContract GetMemberContract(int memberIndex)
        {
            if (memberIndex < 0 || memberIndex >= _valueKeys.Length)
                throw new ArgumentOutOfRangeException(nameof(memberIndex), memberIndex, $"Index '{memberIndex}' is out of range.");
            return GetMemberContract(_valueKeys[memberIndex]);
        }

        /// <inheritdoc />
        public IEnumerable<VerifiableMemberContract> GetMemberContracts()
        {
            if (_verifiableObjectContractImpl is not null)
                return _verifiableObjectContractImpl.GetMemberContracts();
            return _memberContracts.Values.AsReadOnly();
        }

        /// <inheritdoc />
        public bool ContainsMember(string memberName)
        {
            if (_verifiableObjectContractImpl is not null)
                return _verifiableObjectContractImpl.ContainsMember(memberName);
            if (ObjectKind == VerifiableObjectKind.BasicType)
                return false;
            return _memberContracts.ContainsKey(memberName);
        }

        #endregion

        #region Annotation / Attribute

        private bool IncludeAnnotationsForType { get; }

        /// <inheritdoc />
        public bool IncludeAnnotations =>
            (_verifiableObjectContractImpl?.IncludeAnnotations ?? IncludeAnnotationsForType) ||
            _memberContracts.Values.Any(x => x.IncludeAnnotations);

        /// <inheritdoc />
        public IReadOnlyCollection<Attribute> Attributes => _verifiableObjectContractImpl?.Attributes ?? _attributes;

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IEnumerable<VerifiableParamsAttribute> GetParameterAnnotations()
        {
            if (_verifiableObjectContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is VerifiableParamsAttribute annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _verifiableObjectContractImpl.GetParameterAnnotations())
                    yield return annotation;
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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
                if (attribute is VerifiableParamsAttribute)
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