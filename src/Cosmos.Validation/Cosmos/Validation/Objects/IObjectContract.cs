using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public interface IObjectContract
    {
        ObjectContext WithInstance(object instance);
        ObjectContext WithInstance(object instance, string instanceName);
        ObjectContext WithDictionary(IDictionary<string, object> keyValueCollections);
        ObjectContext WithDictionary(IDictionary<string, object> keyValueCollections, string instanceName);
        ObjectValueContract GetValueContract(string name);
        ObjectValueContract GetValueContract(PropertyInfo propertyInfo);
        ObjectValueContract GetValueContract(FieldInfo fieldInfo);
        ObjectValueContract GetValueContract(int index);
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