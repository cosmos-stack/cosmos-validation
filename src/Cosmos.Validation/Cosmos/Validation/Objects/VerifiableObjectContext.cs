using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public class VerifiableObjectContext
    {
        private readonly object _targetObject;
        private readonly IDictionary<string, object> _keyValueRef;
        private readonly VerifiableObjectContract _contract;

        private readonly bool _directMode;

        public VerifiableObjectContext(object targetObject, VerifiableObjectContract contract)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = true;
            _keyValueRef = null;
            InstanceName = contract.IsBasicType ? VerifiableMemberContract.BASIC_TYPE : "Instance";
        }

        public VerifiableObjectContext(object targetObject, VerifiableObjectContract contract, string instanceName)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = true;
            _keyValueRef = null;
            InstanceName = instanceName;
        }

        public VerifiableObjectContext(IDictionary<string, object> keyValueCollection, VerifiableObjectContract contract)
        {
            _targetObject = null;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = false;
            _keyValueRef = keyValueCollection ?? throw new ArgumentNullException(nameof(keyValueCollection));
            InstanceName = "KeyValueCollection";
        }

        public VerifiableObjectContext(IDictionary<string, object> keyValueCollection, VerifiableObjectContract contract, string instanceName)
        {
            _targetObject = null;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = false;
            _keyValueRef = keyValueCollection ?? throw new ArgumentNullException(nameof(keyValueCollection));
            InstanceName = instanceName;
        }

        public string InstanceName { get; }
        
        public Type Type => _contract.Type;

        public object Instance => _targetObject;

        public IDictionary<string, object> KeyValueCollection => _keyValueRef;

        public VerifiableObjectKind ObjectKind => _contract.ObjectKind;

        public bool IsBasicType() => ObjectKind == VerifiableObjectKind.BasicType;

        #region Value

        public VerifiableMemberContext GetValue(string memberName)
        {
            var contract = _contract.GetMemberContract(memberName);

            if (contract is null)
                return default;

            return new VerifiableMemberContext(this, contract, _directMode);
        }

        public VerifiableMemberContext GetValue(int indexOfMember)
        {
            var contract = _contract.GetMemberContract(indexOfMember);

            if (contract is null)
                return default;

            return new VerifiableMemberContext(this, contract, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValues()
        {
            foreach (var contract in GetMembers())
                yield return new VerifiableMemberContext(this, contract, _directMode);
        }

        public IDictionary<string, VerifiableMemberContext> GetValueMap()
        {
            var map = new Dictionary<string, VerifiableMemberContext>();

            foreach (var contract in GetMembers())
            {
                map[contract.MemberName] = new VerifiableMemberContext(this, contract, _directMode);
            }

            return map;
        }

        #endregion

        #region Value with Attribute

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute()
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                select new VerifiableMemberContext(this, member, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr>()
            where TAttr : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr>()
                select new VerifiableMemberContext(this, member, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr1, TAttr2>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2>()
                select new VerifiableMemberContext(this, member, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3>()
                select new VerifiableMemberContext(this, member, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4>()
                select new VerifiableMemberContext(this, member, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>()
                select new VerifiableMemberContext(this, member, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>()
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
                select new VerifiableMemberContext(this, member, _directMode);
        }

        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>()
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
                select new VerifiableMemberContext(this, member, _directMode);
        }

        #endregion

        #region MemberContract

        public VerifiableMemberContract GetMember(string memberName)
        {
            return _contract.GetMemberContract(memberName);
        }

        public VerifiableMemberContract GetMember(int indexOfMember)
        {
            return _contract.GetMemberContract(indexOfMember);
        }

        public IEnumerable<VerifiableMemberContract> GetMembers()
        {
            return _contract.GetMemberContracts();
        }

        public IDictionary<string, VerifiableMemberContract> GetMemberMap()
        {
            var map = new Dictionary<string, VerifiableMemberContract>();

            foreach (var contract in GetMembers())
            {
                map[contract.MemberName] = contract;
            }

            return map;
        }

        #endregion

        #region Annotation / Attribute

        public bool IncludeAnnotations => _contract.IncludeAnnotations;

        public IReadOnlyCollection<Attribute> Attributes => _contract.Attributes;

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute => _contract.GetAttributes<TAttribute>();

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations() => _contract.GetParameterAnnotations();

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations() => _contract.GetQuietVerifiableAnnotations();

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations() => _contract.GetStrongVerifiableAnnotations();

        #endregion

        #region Expose

        internal ICustomVerifiableObjectContractImpl ExposeInternalImpl() => _contract.ExposeInternalImpl();

        #endregion
    }
}