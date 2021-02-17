using System;
using System.Collections.Generic;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public interface IValueContract
    {
        ObjectValueKind ObjectValueKind { get; }
        
        Type DeclaringType { get; }
        
        Type MemberType { get; }

        string MemberName { get; }
        
        bool IsBasicType { get; }

        object GetValue(object value);

        public bool IncludeAnnotations { get; }

        IReadOnlyCollection<Attribute> Attributes { get; }

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

        IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;
    }
}