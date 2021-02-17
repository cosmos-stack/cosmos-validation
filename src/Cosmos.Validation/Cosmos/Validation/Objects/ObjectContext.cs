using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public class ObjectContext
    {
        private readonly object _targetObject;
        private readonly IDictionary<string, object> _keyValueRef;
        private readonly ObjectContract _contract;

        private readonly bool _directMode;

        public ObjectContext(object targetObject, ObjectContract contract)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = true;
            _keyValueRef = null;
            InstanceName = "Instance";
        }

        public ObjectContext(object targetObject, ObjectContract contract, string instanceName)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = true;
            _keyValueRef = null;
            InstanceName = instanceName;
        }

        public ObjectContext(IDictionary<string, object> keyValueCollections, ObjectContract contract)
        {
            _targetObject = null;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = false;
            _keyValueRef = keyValueCollections ?? throw new ArgumentNullException(nameof(keyValueCollections));
            InstanceName = "KeyValueCollection";
        }

        public ObjectContext(IDictionary<string, object> keyValueCollections, ObjectContract contract, string instanceName)
        {
            _targetObject = null;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = false;
            _keyValueRef = keyValueCollections ?? throw new ArgumentNullException(nameof(keyValueCollections));
            InstanceName = instanceName;
        }

        #region GetValue

        public ObjectValueContext GetValue(string name)
        {
            var contract = _contract.GetValueContract(name);

            if (contract is null)
                return default;

            return new ObjectValueContext(this, contract, _directMode);
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

                yield return new ObjectValueContext(this, member, _directMode);
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
                    yield return new ObjectValueContext(this, member, _directMode);
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
                    yield return new ObjectValueContext(this, member, _directMode);
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
                    yield return new ObjectValueContext(this, member, _directMode);
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
                    yield return new ObjectValueContext(this, member, _directMode);
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
                    yield return new ObjectValueContext(this, member, _directMode);
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
                    yield return new ObjectValueContext(this, member, _directMode);
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
                    yield return new ObjectValueContext(this, member, _directMode);
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

        #region ToValueContexts

        public IEnumerable<ObjectValueContext> ToValueContexts()
        {
            foreach (var contract in GetAllMembers())
                yield return new ObjectValueContext(this, contract, _directMode);
        }
        
        public IDictionary<string,ObjectValueContext> ToValueContextMap()
        {
            var map = new Dictionary<string, ObjectValueContext>();

            foreach (var contract in GetAllMembers())
            {
                map[contract.MemberName] = new ObjectValueContext(this, contract, _directMode);
            }

            return map;
        }

        #endregion

        public Type Type => _contract.Type;

        public ObjectKind ObjectKind => _contract.ObjectKind;

        public bool IsBasicType() => ObjectKind == ObjectKind.BasicType;

        public object Instance => _targetObject;

        public IDictionary<string, object> KeyValueCollection => _keyValueRef;

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