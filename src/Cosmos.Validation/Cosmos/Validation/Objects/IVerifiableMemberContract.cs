using System;
using System.Collections.Generic;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public interface IVerifiableMemberContract
    {
        string MemberName { get; }        
        Type DeclaringType { get; }        
        Type MemberType { get; }        
        bool IsBasicType { get; }
        VerifiableMemberKind MemberKind { get; }
        object GetValue(object instance);
        object GetValue(IDictionary<string, object> keyValueCollection);
        public bool IncludeAnnotations { get; }
        IReadOnlyCollection<Attribute> Attributes { get; }
        IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;
        IEnumerable<ValidationParameterAttribute> GetParameterAnnotations();
        IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false,
            bool excludeStrongVerifiableAnnotation = false);
        IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false);
        IEnumerable<IObjectContextVerifiableAnnotation> GetObjectContextVerifiableAnnotations(
            bool excludeFlagAnnotation = false);
        IEnumerable<IFlagAnnotation> GetFlagAnnotations(
            bool excludeVerifiableAnnotation = false);
        IEnumerable<IVerifiable> GetVerifiableAnnotations(
            bool excludeFlagAnnotation = false);
    }
}