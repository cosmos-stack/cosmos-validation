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
    public class ObjectValueContract
    {
        internal const string BASIC_TYPE = "BasicType";
        
        private readonly Type _declaringType;
        private readonly PropertyInfo _propertyInfo;
        private readonly FieldInfo _fieldInfo;

        private readonly Attribute[] _attributes;

        private readonly ICustomAttributeReflectorProvider _reflectorProvider;

        public ObjectValueContract(Type declaringType, PropertyInfo property)
        {
            _declaringType = declaringType;
            ObjectValueKind = ObjectValueKind.Property;

            _propertyInfo = property;
            _fieldInfo = null;

            _reflectorProvider = property.GetReflector();
            _attributes = _reflectorProvider.GetCustomAttributes();

            IncludeAnnotations = HasValidationAnnotationDefined(_attributes);
        }

        public ObjectValueContract(Type declaringType, FieldInfo field)
        {
            _declaringType = declaringType;
            ObjectValueKind = ObjectValueKind.Field;

            _propertyInfo = null;
            _fieldInfo = field;

            _reflectorProvider = field.GetReflector();
            _attributes = _reflectorProvider.GetCustomAttributes();

            IncludeAnnotations = HasValidationAnnotationDefined(_attributes);
        }

        public ObjectValueContract(Type declaringType)
        {
            _declaringType = declaringType;
            ObjectValueKind = ObjectValueKind.ValueType;

            _propertyInfo = null;
            _fieldInfo = null;

            _reflectorProvider = null;
            _attributes = Arrays.Empty<Attribute>();

            IncludeAnnotations = false;
        }

        public ObjectValueKind ObjectValueKind { get; }

        public Type DeclaringType => _declaringType;

        public Type MemberType
        {
            get
            {
                return ObjectValueKind switch
                {
                    ObjectValueKind.Property => _propertyInfo.PropertyType,
                    ObjectValueKind.Field => _fieldInfo.FieldType,
                    ObjectValueKind.ValueType => _declaringType,
                    _ => throw new InvalidOperationException("Unknown ObjectIn type")
                };
            }
        }

        public string MemberName
        {
            get
            {
                return ObjectValueKind switch
                {
                    ObjectValueKind.Property => _propertyInfo.Name,
                    ObjectValueKind.Field => _fieldInfo.Name,
                    ObjectValueKind.ValueType => "ValueType",
                    _ => throw new InvalidOperationException("Unknown ObjectIn type")
                };
            }
        }

        public object GetValue(object value)
        {
            switch (ObjectValueKind)
            {
                case ObjectValueKind.Property:
                    return P(_propertyInfo)(value);

                case ObjectValueKind.Field:
                    return F(_fieldInfo)(value);
                
                case ObjectValueKind.ValueType:
                    return value;

                default:
                    throw new InvalidOperationException("Unknown ObjectInfo type.");
            }
        }

        #region Annotations

        public bool IncludeAnnotations { get; }

        public IReadOnlyCollection<Attribute> Attributes => _attributes;

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations()
        {
            foreach (var attribute in _attributes)
                if (attribute is ValidationParameterAttribute annotation)
                    yield return annotation;
        }

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false,
            bool excludeStrongVerifiableAnnotation = false)
        {
            foreach (var attribute in _attributes)
            {
                if (attribute is IQuietVerifiableAnnotation annotation)
                {
                    var skipAndGoNext = false;

                    if (excludeFlagAnnotation && annotation is IFlagAnnotation)
                        skipAndGoNext = true;

                    if (!skipAndGoNext && excludeObjectContextVerifiableAnnotation && annotation is IObjectContextVerifiableAnnotation)
                        skipAndGoNext = true;

                    if (!skipAndGoNext && excludeStrongVerifiableAnnotation && annotation is IStrongVerifiableAnnotation)
                        skipAndGoNext = true;

                    if (!skipAndGoNext)
                        yield return annotation;
                }
            }
        }

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false)
        {
            foreach (var attribute in _attributes)
            {
                if (attribute is IStrongVerifiableAnnotation annotation)
                {
                    var skipAndGoNext = false;

                    if (excludeFlagAnnotation && annotation is IFlagAnnotation)
                        skipAndGoNext = true;

                    if (!skipAndGoNext && excludeObjectContextVerifiableAnnotation && annotation is IObjectContextVerifiableAnnotation)
                        skipAndGoNext = true;

                    if (!skipAndGoNext)
                        yield return annotation;
                }
            }
        }

        public IEnumerable<IObjectContextVerifiableAnnotation> GetObjectContextVerifiableAnnotations(
            bool excludeFlagAnnotation = false)
        {
            foreach (var attribute in _attributes)
            {
                if (attribute is IObjectContextVerifiableAnnotation annotation)
                {
                    var skipAndGoNext = false;

                    if (excludeFlagAnnotation && annotation is IFlagAnnotation)
                        skipAndGoNext = true;

                    if (!skipAndGoNext)
                        yield return annotation;
                }
            }
        }

        public IEnumerable<IFlagAnnotation> GetFlagAnnotations(
            bool excludeVerifiableAnnotation = false)
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

        public IEnumerable<IVerifiable> GetVerifiableAnnotations(
            bool excludeFlagAnnotation = false)
        {
            foreach (var attribute in _attributes)
            {
                if (attribute is IVerifiable annotation)
                {
                    if (excludeFlagAnnotation)
                    {
                        if (annotation is not IFlagAnnotation)
                            yield return annotation;
                    }
                    else
                    {
                        yield return annotation;
                    }
                }
            }
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

        internal bool HasAttributeDefined<TAttr>()
            where TAttr : Attribute
        {
            return _attributes.OfType<TAttr>().Any();
        }

        internal bool HasAttributeDefined<TAttr1, TAttr2>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
        {
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

        private static Func<PropertyInfo, Func<object, object>> P => property => property.GetValue;

        private static Func<FieldInfo, Func<object, object>> F => field => field.GetValue;
    }
}