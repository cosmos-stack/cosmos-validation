using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Date;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;

namespace CosmosValidationUT.ObjectUT
{
    public class LocalObjectContractImpl : ICustomVerifiableObjectContractImpl
    {
        public Type Type => typeof(NiceBoat);
        public bool IsBasicType => false;
        public VerifiableObjectKind ObjectKind => VerifiableObjectKind.StructureType;

        public object GetValue(object instance, string memberName)
        {
            return memberName switch
            {
                "Name" => "Nice",
                "Length" => 10,
                "Width" => 100,
                "CreateTime" => DateTimeFactory.Create(2020, 12, 21),
                "Email" => "nice@boat.com",
                _ => default
            };
        }

        public object GetValue(object instance, int memberIndex)
        {
            return memberIndex switch
            {
                0 => "Nice",
                1 => 10,
                2 => 100,
                3 => "nice@boat.com",
                4 => DateTimeFactory.Create(2020, 12, 21),
                _ => default
            };
        }

        public object GetValue(IDictionary<string, object> keyValueCollection, string memberName)
        {
            return memberName switch
            {
                "Name" => "Nice",
                "Length" => 10,
                "Width" => 100,
                "CreateTime" => DateTimeFactory.Create(2020, 12, 21),
                "Email" => "nice@boat.com",
                _ => default
            };
        }

        public object GetValue(IDictionary<string, object> keyValueCollection, int memberIndex)
        {
            return memberIndex switch
            {
                0 => "Nice",
                1 => 10,
                2 => 100,
                3 => "nice@boat.com",
                4 => DateTimeFactory.Create(2020, 12, 21),
                _ => default
            };
        }

        public VerifiableMemberContract GetMemberContract(string memberName)
        {
            return new(new LocalMemberContractImpl(memberName));
        }

        public VerifiableMemberContract GetMemberContract(PropertyInfo propertyInfo)
        {
            return null;
        }

        public VerifiableMemberContract GetMemberContract(FieldInfo fieldInfo)
        {
            return null;
        }

        public VerifiableMemberContract GetMemberContract(int memberIndex)
        {
            return null;
        }

        public bool ContainsMember(string memberName)
        {
            return memberName switch
            {
                "Name" => true,
                "Length" => true,
                "Width" => true,
                "CreateTime" => true,
                "Email" => true,
                _ => false
            };
        }

        public IEnumerable<VerifiableMemberContract> GetMemberContracts()
        {
            return null;
        }

        public bool IncludeAnnotations => false;
        public IReadOnlyCollection<Attribute> Attributes { get; }

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, VerifiableMemberContract> GetMemberContractMap()
        {
            return new Dictionary<string, VerifiableMemberContract>
            {
                {"Name", null}, {"Length", null}, {"Width", null}, {"CreateTime", null}, {"Email", null}
            };
        }
    }
}