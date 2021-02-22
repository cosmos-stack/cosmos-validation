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
            InstanceName = contract.IsBasicType() ? ObjectValueContract.BASIC_TYPE : "Instance";
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

        public Type Type => _contract.Type;

        public ObjectKind ObjectKind => _contract.ObjectKind;

        public bool IsBasicType() => ObjectKind == ObjectKind.BasicType;

        public object Instance => _targetObject;

        public IDictionary<string, object> KeyValueCollection => _keyValueRef;

        public string InstanceName { get; }

        #region GetValue

        public ObjectValueContext GetValue(string memberName)
        {
            var contract = _contract.GetValueContract(memberName);

            if (contract is null)
                return default;

            return new ObjectValueContext(this, contract, _directMode);
        }

        public ObjectValueContext GetValue(int indexOfMember)
        {
            var contract = _contract.GetValueContract(indexOfMember);

            if (contract is null)
                return default;

            return new ObjectValueContext(this, contract, _directMode);
        }

        public IEnumerable<ObjectValueContext> GetValues()
        {
            foreach (var contract in GetMembers())
                yield return new ObjectValueContext(this, contract, _directMode);
        }

        public IDictionary<string, ObjectValueContext> GetValueMap()
        {
            var map = new Dictionary<string, ObjectValueContext>();

            foreach (var contract in GetMembers())
            {
                map[contract.MemberName] = new ObjectValueContext(this, contract, _directMode);
            }

            return map;
        }

        #endregion

        #region GetValuesWithAttribute

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute()
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                select new ObjectValueContext(this, member, _directMode);
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr>()
            where TAttr : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr>()
                select new ObjectValueContext(this, member, _directMode);
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2>()
                select new ObjectValueContext(this, member, _directMode);
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3>()
                select new ObjectValueContext(this, member, _directMode);
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4>()
                select new ObjectValueContext(this, member, _directMode);
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>()
                select new ObjectValueContext(this, member, _directMode);
        }

        public IEnumerable<ObjectValueContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>()
                select new ObjectValueContext(this, member, _directMode);
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
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>()
                select new ObjectValueContext(this, member, _directMode);
        }

        #endregion

        #region GetMember

        public ObjectValueContract GetMember(string memberName)
        {
            return _contract.GetValueContract(memberName);
        }

        public ObjectValueContract GetMember(int indexOfMember)
        {
            return _contract.GetValueContract(indexOfMember);
        }

        public IEnumerable<ObjectValueContract> GetMembers()
        {
            return _contract.GetAllValueContracts();
        }

        public IDictionary<string, ObjectValueContract> GetMemberMap()
        {
            var map = new Dictionary<string, ObjectValueContract>();

            foreach (var contract in GetMembers())
            {
                map[contract.MemberName] = contract;
            }

            return map;
        }

        #endregion

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