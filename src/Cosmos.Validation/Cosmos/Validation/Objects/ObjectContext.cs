using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public class ObjectContext
    {
        private readonly object _targetObject;
        private readonly ObjectContract _contract;

        public ObjectContext(object targetObject, ObjectContract contract)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            InstanceName = "Instance";
        }

        public ObjectContext(object targetObject, ObjectContract contract, string instanceName)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            InstanceName = instanceName;
        }

        #region GetValue

        public ObjectValueContext GetValue(string name)
        {
            var contract = _contract.GetValueContract(name);

            if (contract is null)
                return default;

            return new ObjectValueContext(this, contract);
        }

        public IEnumerable<ObjectValueContext> GetAllValues()
        {
            foreach (var contract in GetAllMembers())
                yield return new ObjectValueContext(this, contract);
        }

        #endregion

        #region GetValuesWithAttribute

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute()
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                yield return new ObjectValueContext(this, member);
            }
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr>()
            where TAttr : Attribute
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                if (member.HasAttributeDefined<TAttr>())
                    yield return new ObjectValueContext(this, member);
            }
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                if (member.HasAttributeDefined<TAttr1, TAttr2>())
                    yield return new ObjectValueContext(this, member);
            }
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                if (member.HasAttributeDefined<TAttr1, TAttr2, TAttr3>())
                    yield return new ObjectValueContext(this, member);
            }
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                if (member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4>())
                    yield return new ObjectValueContext(this, member);
            }
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                if (member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>())
                    yield return new ObjectValueContext(this, member);
            }
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                if (member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>())
                    yield return new ObjectValueContext(this, member);
            }
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute
            where TAttr7 : Attribute
        {
            var members = GetAllMembers();

            foreach (var member in members)
            {
                if (!member.IncludeAnnotations)
                    continue;

                if (member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>())
                    yield return new ObjectValueContext(this, member);
            }
        }

        #endregion

        #region GetMember

        public IEnumerable<ObjectValueContract> GetAllMembers()
        {
            return _contract.GetAllValueContracts();
        }

        public IEnumerable<string> GetAllMemberNames()
        {
            return GetAllMembers().Select(x => x.MemberName);
        }

        #endregion

        public Type Type => _contract.Type;

        public ObjectKind ObjectKind => _contract.ObjectKind;

        public bool IsBasicType() => ObjectKind == ObjectKind.BasicType;

        public object Instance => _targetObject;

        public string InstanceName { get; }

        #region Annotations

        public bool IncludeAnnotations => _contract.IncludeAnnotations;

        public IReadOnlyCollection<Attribute> Attributes => _contract.Attributes;

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations() => _contract.GetParameterAnnotations();

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations() => _contract.GetQuietVerifiableAnnotations();

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations() => _contract.GetStrongVerifiableAnnotations();

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute => _contract.GetAttributes<TAttribute>();

        #endregion
    }
}