using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using CosmosStack.Collections;
using CosmosStack.Reflection;
using CosmosStack.Validation.Annotations;

// ReSharper disable InconsistentNaming
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable member contract <br />
    /// 可验证成员约定
    /// </summary>
    public class VerifiableMemberContract : IVerifiableMemberContract
    {
        internal const string BASIC_TYPE = "BasicType";

        private readonly PropertyInfo _propertyInfo;
        private readonly FieldInfo _fieldInfo;

        private readonly Attribute[] _attributes;

        private readonly ICustomAttributeReflectorProvider _reflectorProvider;
        private readonly ICustomVerifiableMemberContractImpl _verifiableMemberContractImpl;

        public VerifiableMemberContract(Type declaringType, PropertyInfo property)
        {
            _verifiableMemberContractImpl = null;
            DeclaringType = declaringType;
            MemberKind = VerifiableMemberKind.Property;
            IsBasicType = property.PropertyType.IsBasicType();

            _propertyInfo = property;
            _fieldInfo = null;

            _reflectorProvider = property.GetReflector();
            _attributes = _reflectorProvider.GetCustomAttributes();
            IncludeAnnotations = HasValidationAnnotationDefined(_attributes);
        }

        public VerifiableMemberContract(Type declaringType, FieldInfo field)
        {
            _verifiableMemberContractImpl = null;
            DeclaringType = declaringType;
            MemberKind = VerifiableMemberKind.Field;
            IsBasicType = field.FieldType.IsBasicType();

            _propertyInfo = null;
            _fieldInfo = field;

            _reflectorProvider = field.GetReflector();
            _attributes = _reflectorProvider.GetCustomAttributes();
            IncludeAnnotations = HasValidationAnnotationDefined(_attributes);
        }

        public VerifiableMemberContract(Type declaringType)
        {
            _verifiableMemberContractImpl = null;
            DeclaringType = declaringType;
            MemberKind = VerifiableMemberKind.Unknown;
            IsBasicType = declaringType.IsBasicType();

            _propertyInfo = null;
            _fieldInfo = null;

            _reflectorProvider = null;
            _attributes = Arrays.Empty<Attribute>();
            IncludeAnnotations = false;
        }

        public VerifiableMemberContract(ICustomVerifiableMemberContractImpl contractImpl)
        {
            _verifiableMemberContractImpl = contractImpl ?? throw new ArgumentNullException(nameof(contractImpl));
            DeclaringType = contractImpl.DeclaringType;
            MemberKind = VerifiableMemberKind.CustomContract;
            IsBasicType = contractImpl.IsBasicType;

            _propertyInfo = null;
            _fieldInfo = null;

            _reflectorProvider = null;
            _attributes = Arrays.Empty<Attribute>();
            IncludeAnnotations = contractImpl.IncludeAnnotations;
        }

        /// <inheritdoc />
        public string MemberName
        {
            get
            {
                return MemberKind switch
                {
                    VerifiableMemberKind.Property => _propertyInfo.Name,
                    VerifiableMemberKind.Field => _fieldInfo.Name,
                    VerifiableMemberKind.Unknown => BASIC_TYPE,
                    VerifiableMemberKind.CustomContract => _verifiableMemberContractImpl.MemberName,
                    _ => throw new InvalidOperationException("Unknown ObjectIn type")
                };
            }
        }

        /// <inheritdoc />
        public Type DeclaringType { get; }

        /// <inheritdoc />
        public Type MemberType
        {
            get
            {
                return MemberKind switch
                {
                    VerifiableMemberKind.Property => _propertyInfo.PropertyType,
                    VerifiableMemberKind.Field => _fieldInfo.FieldType,
                    VerifiableMemberKind.Unknown => DeclaringType,
                    VerifiableMemberKind.CustomContract => _verifiableMemberContractImpl.MemberType,
                    _ => throw new InvalidOperationException("Unknown ObjectIn type")
                };
            }
        }

        /// <inheritdoc />
        public bool IsBasicType { get; }

        /// <inheritdoc />
        public VerifiableMemberKind MemberKind { get; }

        #region Value

        /// <inheritdoc />
        public object GetValue(object instance)
        {
            if (MemberKind == VerifiableMemberKind.CustomContract)
                return _verifiableMemberContractImpl.GetValue(instance);

            if (instance is IDictionary<string, object> d)
                return GetValue(d);

            if (MemberKind == VerifiableMemberKind.Property)
                return P(_propertyInfo)(instance);

            if (MemberKind == VerifiableMemberKind.Field)
                return F(_fieldInfo)(instance);

            if (MemberKind == VerifiableMemberKind.Unknown)
                return instance;

            throw new InvalidOperationException("Unknown ObjectInfo type.");
        }

        /// <inheritdoc />
        public object GetValue(IDictionary<string, object> keyValueCollection)
        {
            if (keyValueCollection is null)
                return default;
            return D(MemberName)(keyValueCollection);
        }

        private static Func<PropertyInfo, Func<object, object>> P => property => property.GetValue;

        private static Func<FieldInfo, Func<object, object>> F => field => field.GetValue;

        private static Func<string, Func<IDictionary<string, object>, object>> D =>
            key => dictionary => dictionary.TryGetValue(key, out var result) ? result : default;

        #endregion

        #region Annotation / Attribute

        /// <inheritdoc />
        public bool IncludeAnnotations { get; }

        /// <inheritdoc />
        public IReadOnlyCollection<Attribute> Attributes => _verifiableMemberContractImpl?.Attributes ?? _attributes;

        /// <inheritdoc />
        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            if (_verifiableMemberContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is TAttribute t)
                        yield return t;
            }
            else
            {
                foreach (var annotation in _verifiableMemberContractImpl.GetAttributes<TAttribute>())
                    yield return annotation;
            }
        }

        // public AttributeCollection GetAttributeCollection()
        // {
        //     return new AttributeCollection(Attributes);
        // }
        //
        // public AttributeCollection GetAttributeCollection<TAttribute>() where TAttribute : Attribute
        // {
        //     return new AttributeCollection(GetAttributes<TAttribute>());
        // }

        /// <inheritdoc />
        public IEnumerable<VerifiableParamsAttribute> GetParameterAnnotations()
        {
            if (_verifiableMemberContractImpl is null)
            {
                foreach (var attribute in _attributes)
                    if (attribute is VerifiableParamsAttribute annotation)
                        yield return annotation;
            }
            else
            {
                foreach (var annotation in _verifiableMemberContractImpl.GetParameterAnnotations())
                    yield return annotation;
            }
        }

        /// <inheritdoc />
        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false,
            bool excludeStrongVerifiableAnnotation = false)
        {
            if (_verifiableMemberContractImpl is null)
            {
                foreach (var attribute in _attributes)
                {
                    if (attribute is IQuietVerifiableAnnotation annotation)
                    {
                        var skipAndGoNext = excludeFlagAnnotation && annotation is IFlagAnnotation;

                        if (!skipAndGoNext && excludeObjectContextVerifiableAnnotation && annotation is IObjectContextVerifiableAnnotation)
                            skipAndGoNext = true;

                        if (!skipAndGoNext && excludeStrongVerifiableAnnotation && annotation is IStrongVerifiableAnnotation)
                            skipAndGoNext = true;

                        if (!skipAndGoNext)
                            yield return annotation;
                    }
                }
            }
            else
            {
                foreach (var annotation in _verifiableMemberContractImpl.GetQuietVerifiableAnnotations(
                    excludeFlagAnnotation,
                    excludeObjectContextVerifiableAnnotation,
                    excludeStrongVerifiableAnnotation))
                    yield return annotation;
            }
        }

        /// <inheritdoc />
        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false)
        {
            if (_verifiableMemberContractImpl is null)
            {
                foreach (var attribute in _attributes)
                {
                    if (attribute is IStrongVerifiableAnnotation annotation)
                    {
                        var skipAndGoNext = excludeFlagAnnotation && annotation is IFlagAnnotation;

                        if (!skipAndGoNext && excludeObjectContextVerifiableAnnotation && annotation is IObjectContextVerifiableAnnotation)
                            skipAndGoNext = true;

                        if (!skipAndGoNext)
                            yield return annotation;
                    }
                }
            }
            else
            {
                foreach (var annotation in _verifiableMemberContractImpl.GetStrongVerifiableAnnotations(
                    excludeFlagAnnotation,
                    excludeObjectContextVerifiableAnnotation))
                    yield return annotation;
            }
        }

        /// <inheritdoc />
        public IEnumerable<IObjectContextVerifiableAnnotation> GetObjectContextVerifiableAnnotations(
            bool excludeFlagAnnotation = false)
        {
            if (_verifiableMemberContractImpl is null)
            {
                foreach (var attribute in _attributes)
                {
                    if (attribute is IObjectContextVerifiableAnnotation annotation)
                    {
                        var skipAndGoNext = excludeFlagAnnotation && annotation is IFlagAnnotation;

                        if (!skipAndGoNext)
                            yield return annotation;
                    }
                }
            }
            else
            {
                foreach (var annotation in _verifiableMemberContractImpl.GetObjectContextVerifiableAnnotations(
                    excludeFlagAnnotation))
                    yield return annotation;
            }
        }

        /// <inheritdoc />
        public IEnumerable<IFlagAnnotation> GetFlagAnnotations(
            bool excludeVerifiableAnnotation = false)
        {
            if (_verifiableMemberContractImpl is null)
            {
                foreach (var attribute in _attributes)
                {
                    if (attribute is IFlagAnnotation annotation)
                    {
                        if (excludeVerifiableAnnotation)
                        {
                            if (annotation is not IQuietVerifiableAnnotation &&
                                annotation is not IStrongVerifiableAnnotation &&
                                annotation is not IObjectContextVerifiableAnnotation)
                                yield return annotation;
                        }
                        else
                        {
                            yield return annotation;
                        }
                    }
                }
            }
            else
            {
                foreach (var annotation in _verifiableMemberContractImpl.GetFlagAnnotations(
                    excludeVerifiableAnnotation))
                    yield return annotation;
            }
        }

        /// <inheritdoc />
        public IEnumerable<IVerifiable> GetVerifiableAnnotations(
            bool excludeFlagAnnotation = false)
        {
            if (_verifiableMemberContractImpl is null)
            {
                foreach (var attribute in _attributes)
                {
                    if (attribute is IVerifiable annotation)
                    {
                        if (excludeFlagAnnotation)
                        {
                            if (annotation is not IFlagAnnotation)
                                yield return annotation;
                            else if (annotation is IQuietVerifiableAnnotation ||
                                     annotation is IStrongVerifiableAnnotation ||
                                     annotation is IObjectContextVerifiableAnnotation)
                                yield return annotation;
                        }
                        else
                        {
                            yield return annotation;
                        }
                    }
                }
            }
            else
            {
                foreach (var annotation in _verifiableMemberContractImpl.GetVerifiableAnnotations(
                    excludeFlagAnnotation))
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

        internal bool HasAttributeDefined<TAttr>()
            where TAttr : Attribute
        {
            if (_verifiableMemberContractImpl is not null)
                return _verifiableMemberContractImpl.HasAttributeDefined<TAttr>();
            return _attributes.OfType<TAttr>().Any();
        }

        internal bool HasAttributeDefined<TAttr1, TAttr2>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
        {
            if (_verifiableMemberContractImpl is not null)
                return _verifiableMemberContractImpl.HasAttributeDefined<TAttr1, TAttr2>();

            foreach (var attribute in _attributes)
            {
                switch (attribute)
                {
                    case TAttr1:
                    case TAttr2:
                        return true;
                }
            }

            return false;
        }

        internal bool HasAttributeDefined<TAttr1, TAttr2, TAttr3>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
        {
            if (_verifiableMemberContractImpl is not null)
                return _verifiableMemberContractImpl.HasAttributeDefined<TAttr1, TAttr2, TAttr3>();

            foreach (var attribute in _attributes)
            {
                switch (attribute)
                {
                    case TAttr1:
                    case TAttr2:
                    case TAttr3:
                        return true;
                }
            }

            return false;
        }

        internal bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
        {
            if (_verifiableMemberContractImpl is not null)
                return _verifiableMemberContractImpl.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4>();

            foreach (var attribute in _attributes)
            {
                switch (attribute)
                {
                    case TAttr1:
                    case TAttr2:
                    case TAttr3:
                    case TAttr4:
                        return true;
                }
            }

            return false;
        }

        internal bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
        {
            if (_verifiableMemberContractImpl is not null)
                return _verifiableMemberContractImpl.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>();

            foreach (var attribute in _attributes)
            {
                switch (attribute)
                {
                    case TAttr1:
                    case TAttr2:
                    case TAttr3:
                    case TAttr4:
                    case TAttr5:
                        return true;
                }
            }

            return false;
        }

        internal bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute
        {
            if (_verifiableMemberContractImpl is not null)
                return _verifiableMemberContractImpl.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>();

            foreach (var attribute in _attributes)
            {
                switch (attribute)
                {
                    case TAttr1:
                    case TAttr2:
                    case TAttr3:
                    case TAttr4:
                    case TAttr5:
                    case TAttr6:
                        return true;
                }
            }

            return false;
        }

        internal bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute
            where TAttr7 : Attribute
        {
            if (_verifiableMemberContractImpl is not null)
                return _verifiableMemberContractImpl.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>();

            foreach (var attribute in _attributes)
            {
                switch (attribute)
                {
                    case TAttr1:
                    case TAttr2:
                    case TAttr3:
                    case TAttr4:
                    case TAttr5:
                    case TAttr6:
                    case TAttr7:
                        return true;
                }
            }

            return false;
        }

        #endregion

        #region Expose

        internal ICustomVerifiableMemberContractImpl ExposeInternalImpl() => _verifiableMemberContractImpl;

        #endregion
    }
}