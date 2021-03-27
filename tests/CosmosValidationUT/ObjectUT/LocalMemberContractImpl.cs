using System;
using System.Collections.Generic;
using Cosmos.Date;
using Cosmos.Reflection;
using Cosmos.Validation;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;

namespace CosmosValidationUT.ObjectUT
{
    public class LocalMemberContractImpl : ICustomVerifiableMemberContractImpl
    {
        public LocalMemberContractImpl(string memberName)
        {
            MemberName = memberName;
            MemberType = memberName switch
            {
                "Name" => TypeClass.StringClazz,
                "Length" => TypeClass.LongClazz,
                "Width" => TypeClass.LongClazz,
                "CreateTime" => TypeClass.DateTimeClazz,
                "Email" => TypeClass.StringClazz,
                _ => default
            };

            MemberKind = memberName switch
            {
                "Name" => VerifiableMemberKind.Property,
                "Length" => VerifiableMemberKind.Property,
                "Width" => VerifiableMemberKind.Property,
                "CreateTime" => VerifiableMemberKind.Field,
                "Email" => VerifiableMemberKind.Property,
                _ => default
            };
        }

        public string MemberName { get; }
        public Type DeclaringType => typeof(NiceBoat);
        public Type MemberType { get; }
        public bool IsBasicType => true;

        public VerifiableMemberKind MemberKind { get; }

        public object GetValue(object instance)
        {
            return MemberName switch
            {
                "Name" => "Nice",
                "Length" => 10,
                "Width" => 100,
                "CreateTime" => DateTimeFactory.Create(2020, 12, 21),
                "Email" => "nice@boat.com",
                _ => default
            };
        }

        public object GetValue(IDictionary<string, object> keyValueCollection)
        {
            return MemberName switch
            {
                "Name" => "Nice",
                "Length" => 10,
                "Width" => 100,
                "CreateTime" => DateTimeFactory.Create(2020, 12, 21),
                "Email" => "nice@boat.com",
                _ => default
            };
        }

        public bool IncludeAnnotations => false;
        public IReadOnlyCollection<Attribute> Attributes { get; }

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VerifiableParamsAttribute> GetParameterAnnotations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations(bool excludeFlagAnnotation = false, bool excludeObjectContextVerifiableAnnotation = false, bool excludeStrongVerifiableAnnotation = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations(bool excludeFlagAnnotation = false, bool excludeObjectContextVerifiableAnnotation = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IObjectContextVerifiableAnnotation> GetObjectContextVerifiableAnnotations(bool excludeFlagAnnotation = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFlagAnnotation> GetFlagAnnotations(bool excludeVerifiableAnnotation = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IVerifiable> GetVerifiableAnnotations(bool excludeFlagAnnotation = false)
        {
            throw new NotImplementedException();
        }

        public bool HasAttributeDefined<TAttr>() where TAttr : Attribute
        {
            throw new NotImplementedException();
        }

        public bool HasAttributeDefined<TAttr1, TAttr2>() where TAttr1 : Attribute where TAttr2 : Attribute
        {
            throw new NotImplementedException();
        }

        public bool HasAttributeDefined<TAttr1, TAttr2, TAttr3>() where TAttr1 : Attribute where TAttr2 : Attribute where TAttr3 : Attribute
        {
            throw new NotImplementedException();
        }

        public bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4>() where TAttr1 : Attribute where TAttr2 : Attribute where TAttr3 : Attribute where TAttr4 : Attribute
        {
            throw new NotImplementedException();
        }

        public bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>() where TAttr1 : Attribute where TAttr2 : Attribute where TAttr3 : Attribute where TAttr4 : Attribute where TAttr5 : Attribute
        {
            throw new NotImplementedException();
        }

        public bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>() where TAttr1 : Attribute where TAttr2 : Attribute where TAttr3 : Attribute where TAttr4 : Attribute where TAttr5 : Attribute where TAttr6 : Attribute
        {
            throw new NotImplementedException();
        }

        public bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>() where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute
            where TAttr7 : Attribute
        {
            throw new NotImplementedException();
        }
    }
}