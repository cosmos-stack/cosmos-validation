using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public interface IObjectContract
    {
        ObjectValueContract GetValueContract(string name);
        ObjectValueContract GetValueContract(PropertyInfo propertyInfo);
        ObjectValueContract GetValueContract(FieldInfo fieldInfo);
        IEnumerable<ObjectValueContract> GetAllValueContracts();
        Type Type { get; }
        ObjectKind ObjectKind { get; }
        bool IsBasicType();
        bool IncludeAnnotations { get; }
        IReadOnlyCollection<Attribute> Attributes { get; }
        IEnumerable<ValidationParameterAttribute> GetParameterAnnotations();
        IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations();
        IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations();
        IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;
    }
}