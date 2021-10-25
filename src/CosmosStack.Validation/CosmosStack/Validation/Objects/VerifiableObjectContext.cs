using System;
using System.Collections.Generic;
using System.Linq;
using CosmosStack.Validation.Annotations;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable object context <br />
    /// 可验证对象上下文
    /// </summary>
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

            ParentContext = null;
            IsChildContext = false;
        }

        internal VerifiableObjectContext(object targetObject, VerifiableObjectContract contract, VerifiableObjectContext parentContext)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = true;
            _keyValueRef = null;
            InstanceName = contract.IsBasicType ? VerifiableMemberContract.BASIC_TYPE : "Instance";

            ParentContext = parentContext;
            IsChildContext = parentContext is not null;
        }

        public VerifiableObjectContext(object targetObject, VerifiableObjectContract contract, string instanceName)
        {
            _targetObject = targetObject;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = true;
            _keyValueRef = null;
            InstanceName = instanceName;

            ParentContext = null;
            IsChildContext = false;
        }

        public VerifiableObjectContext(IDictionary<string, object> keyValueCollection, VerifiableObjectContract contract)
        {
            _targetObject = null;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = false;
            _keyValueRef = keyValueCollection ?? throw new ArgumentNullException(nameof(keyValueCollection));
            InstanceName = "KeyValueCollection";

            ParentContext = null;
            IsChildContext = false;
        }

        public VerifiableObjectContext(IDictionary<string, object> keyValueCollection, VerifiableObjectContract contract, string instanceName)
        {
            _targetObject = null;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _directMode = false;
            _keyValueRef = keyValueCollection ?? throw new ArgumentNullException(nameof(keyValueCollection));
            InstanceName = instanceName;

            ParentContext = null;
            IsChildContext = false;
        }

        /// <summary>
        /// Instance name <br />
        /// 实例名称
        /// </summary>
        public string InstanceName { get; }

        /// <summary>
        /// Type <br />
        /// 类型
        /// </summary>
        public Type Type => _contract.Type;

        /// <summary>
        /// Verifiable object kind <br />
        /// 可验证对象种类
        /// </summary>
        public VerifiableObjectKind ObjectKind => _contract.ObjectKind;

        /// <summary>
        /// Is basic type <br />
        /// 是否为基本类型
        /// </summary>
        /// <returns></returns>
        public bool IsBasicType() => ObjectKind == VerifiableObjectKind.BasicType;

        #region Instance

        /// <summary>
        /// Gets instance <br />
        /// 获取实例
        /// </summary>
        public object Instance => _targetObject;

        /// <summary>
        /// Gets dictionary <br />
        /// 将实例转换为字典，并获取字典
        /// </summary>
        public IDictionary<string, object> KeyValueCollection => _keyValueRef;

        #endregion

        #region Parent Context

        /// <summary>
        /// Is child context <br />
        /// 标记当前上下文是否存在上一级
        /// </summary>
        public bool IsChildContext { get; }

        /// <summary>
        /// Parent context <br />
        /// 如果存在上一级上下文，返回之
        /// </summary>
        public VerifiableObjectContext ParentContext { get; }

        #endregion

        #region Value

        /// <summary>
        /// Get value from the given instance by member name <br />
        /// 根据给定的成员名称，从给定的实例中获取值
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public VerifiableMemberContext GetValue(string memberName)
        {
            var contract = _contract.GetMemberContract(memberName);

            if (contract is null)
                return default;

            return new VerifiableMemberContext(this, contract, _directMode);
        }

        /// <summary>
        /// Get value from the given instance by member index <br />
        /// 根据给定的成员索引值，从给定的实例中获取值
        /// </summary>
        /// <param name="indexOfMember"></param>
        /// <returns></returns>
        public VerifiableMemberContext GetValue(int indexOfMember)
        {
            var contract = _contract.GetMemberContract(indexOfMember);

            if (contract is null)
                return default;

            return new VerifiableMemberContext(this, contract, _directMode);
        }

        /// <summary>
        /// Get all values by <see cref="VerifiableMemberContext"/> <br />
        /// 以 <see cref="VerifiableMemberContext"/> 集合的方式返回所有值
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VerifiableMemberContext> GetValues()
        {
            foreach (var contract in GetMembers())
                yield return new VerifiableMemberContext(this, contract, _directMode);
        }

        /// <summary>
        /// Get all values by <see cref="VerifiableMemberContext"/> with key <br />
        /// 以 <see cref="VerifiableMemberContext"/> 集合的方式返回所有值，并将成员名作为键
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute()
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                select new VerifiableMemberContext(this, member, _directMode);
        }

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <typeparam name="TAttr"></typeparam>
        /// <returns></returns>
        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr>()
            where TAttr : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr>()
                select new VerifiableMemberContext(this, member, _directMode);
        }

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <returns></returns>
        public IEnumerable<VerifiableMemberContext> GetValuesWithAttribute<TAttr1, TAttr2>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
        {
            return from member in GetMembers()
                where member.IncludeAnnotations
                where member.HasAttributeDefined<TAttr1, TAttr2>()
                select new VerifiableMemberContext(this, member, _directMode);
        }

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <typeparam name="TAttr5"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <typeparam name="TAttr5"></typeparam>
        /// <typeparam name="TAttr6"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Get values with attribute <br />
        /// 返回所有包含注解的值，并以 <see cref="VerifiableMemberContext"/> 集合的方式返回
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <typeparam name="TAttr5"></typeparam>
        /// <typeparam name="TAttr6"></typeparam>
        /// <typeparam name="TAttr7"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Get member by member name <br />
        /// 根据给定的成员名，返回可验证成员约定
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public VerifiableMemberContract GetMember(string memberName)
        {
            return _contract.GetMemberContract(memberName);
        }

        /// <summary>
        /// Get member by member index <br />
        /// 根据给定的成员索引，返回可验证成员约定
        /// </summary>
        /// <param name="indexOfMember"></param>
        /// <returns></returns>
        public VerifiableMemberContract GetMember(int indexOfMember)
        {
            return _contract.GetMemberContract(indexOfMember);
        }

        /// <summary>
        /// Get all members <br />
        /// 返回所有的可验证成员约定
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VerifiableMemberContract> GetMembers()
        {
            return _contract.GetMemberContracts();
        }

        /// <summary>
        /// Get all members with key <br />
        /// 返回所有的可验证成员约定，并将成员名作为键
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, VerifiableMemberContract> GetMemberMap()
        {
            var map = new Dictionary<string, VerifiableMemberContract>();

            foreach (var contract in GetMembers())
            {
                map[contract.MemberName] = contract;
            }

            return map;
        }

        /// <summary>
        /// Is contain member <br />
        /// 检查是否包含给定名称的成员
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public bool ContainsMember(string memberName)
        {
            return _contract.ContainsMember(memberName);
        }

        #endregion

        #region Annotation / Attribute

        /// <summary>
        /// Is include annotation <br />
        /// 标记是否包含注解
        /// </summary>
        public bool IncludeAnnotations => _contract.IncludeAnnotations;

        /// <summary>
        /// Get attributes <br />
        /// 获取特性集合
        /// </summary>
        public IReadOnlyCollection<Attribute> Attributes => _contract.Attributes;

        /// <summary>
        /// Get attributes <br />
        /// 获取特性集合
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute => _contract.GetAttributes<TAttribute>();

        /// <summary>
        /// Get parameter annotations <br />
        /// 获取参数注解
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VerifiableParamsAttribute> GetParameterAnnotations() => _contract.GetParameterAnnotations();

        /// <summary>
        /// Get quiet verifiable annotations <br />
        /// 获取安静验证注解
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations() => _contract.GetQuietVerifiableAnnotations();

        /// <summary>
        /// Get strong verifiable annotations <br />
        /// 获取强验证注解
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations() => _contract.GetStrongVerifiableAnnotations();

        #endregion

        #region Expose

        internal ICustomVerifiableObjectContractImpl ExposeInternalImpl() => _contract.ExposeInternalImpl();

        #endregion
    }
}