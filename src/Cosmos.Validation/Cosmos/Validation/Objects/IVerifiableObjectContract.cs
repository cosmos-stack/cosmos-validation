using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public interface IVerifiableObjectContract
    {
        Type Type { get; }
        bool IsBasicType { get; }
        VerifiableObjectKind ObjectKind { get; }
        VerifiableObjectContext WithInstance(object instance);
        VerifiableObjectContext WithInstance(object instance, string instanceName);
        VerifiableObjectContext WithDictionary(IDictionary<string, object> keyValueCollection);
        VerifiableObjectContext WithDictionary(IDictionary<string, object> keyValueCollection, string instanceName);
        object GetValue(object instance, string memberName);
        object GetValue(object instance, int memberIndex);
        object GetValue(IDictionary<string, object> keyValueCollection, string memberName);
        object GetValue(IDictionary<string, object> keyValueCollection, int memberIndex);
        VerifiableMemberContract GetMemberContract(string memberName);
        VerifiableMemberContract GetMemberContract(PropertyInfo propertyInfo);
        VerifiableMemberContract GetMemberContract(FieldInfo fieldInfo);
        VerifiableMemberContract GetMemberContract(int memberIndex);
        IEnumerable<VerifiableMemberContract> GetMemberContracts();
        bool IncludeAnnotations { get; }
        IReadOnlyCollection<Attribute> Attributes { get; }
        IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;
        IEnumerable<ValidationParameterAttribute> GetParameterAnnotations();
        IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations();
        IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations();
    }
}