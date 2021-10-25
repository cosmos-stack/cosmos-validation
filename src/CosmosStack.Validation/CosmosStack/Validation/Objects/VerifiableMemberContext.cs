using System;
using System.Collections.Generic;
using CosmosStack.Validation.Annotations;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable member context <br />
    /// 可验证成员上下文
    /// </summary>
    public class VerifiableMemberContext
    {
        private readonly VerifiableMemberContract _contract;
        private readonly VerifiableObjectContext _parentContext;
        private readonly ValueMode _valueMode;

        public VerifiableMemberContext(VerifiableObjectContext parentContext, VerifiableMemberContract contract, bool directMode)
        {
            _parentContext = parentContext;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _valueMode = directMode ? ValueMode.DirectType : ValueMode.Dictionary;
        }

        private VerifiableMemberContext(object value, VerifiableMemberContract contract, VerifiableObjectContext parentContext)
        {
            _parentContext = parentContext;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _valueMode = ValueMode.DirectValue;

            _hasGot = true;
            _valueCached = value;
        }

        /// <summary>
        /// Member name <br />
        /// 成员名
        /// </summary>
        public string MemberName => _contract.MemberName;

        /// <summary>
        /// Declaring type <br />
        /// 声明类型
        /// </summary>
        public Type DeclaringType => _contract.DeclaringType;

        /// <summary>
        /// Member type <br />
        /// 成员类型
        /// </summary>
        public Type MemberType => _contract.MemberType;

        /// <summary>
        /// Is basic type <br />
        /// 是否为基本类型
        /// </summary>
        public bool IsBasicType => _contract.IsBasicType;

        /// <summary>
        /// Member kind <br />
        /// 成员类型
        /// </summary>
        public VerifiableMemberKind MemberKind => _contract.MemberKind;

        #region Value

        private bool _hasGot;
        private object _valueCached;

        /// <summary>
        /// Gets value <br />
        /// 获取值
        /// </summary>
        public object Value
        {
            get
            {
                if (!_hasGot)
                {
                    RefreshValue();
                }

                return _valueCached;
            }
        }

        internal void RefreshValue()
        {
            _valueCached = _valueMode switch
            {
                ValueMode.DirectType => _contract.GetValue(_parentContext.Instance),
                ValueMode.Dictionary => _contract.GetValue(_parentContext.KeyValueCollection),
                _ => default
            };

            _hasGot = true;
        }

        #endregion

        #region ParentInstance

        internal bool HasParentContext() => _parentContext is not null;

        /// <summary>
        /// Get parent instance <br />
        /// 获取上一级实例
        /// </summary>
        /// <returns></returns>
        public object GetParentInstance() => _parentContext?.Instance;

        /// <summary>
        /// Get parent instance <br />
        /// 获取上一级实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParentInstance<T>() => (T)_parentContext?.Instance;

        #endregion

        #region Annotation / Attribute

        /// <summary>
        /// Is include annotation <br />
        /// 标记是否包含注解
        /// </summary>
        public bool IncludeAnnotations
            => _contract.IncludeAnnotations;

        /// <summary>
        /// Get attributes <br />
        /// 获取特性集合
        /// </summary>
        public IReadOnlyCollection<Attribute> Attributes
            => _contract.Attributes;

        /// <summary>
        /// Get attributes <br />
        /// 获取特性集合
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
            => _contract.GetAttributes<TAttribute>();

        /// <summary>
        /// Get parameter annotations <br />
        /// 获取参数注解
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VerifiableParamsAttribute> GetParameterAnnotations()
            => _contract.GetParameterAnnotations();

        /// <summary>
        /// Get quiet verifiable annotations <br />
        /// 获取安静验证注解
        /// </summary>
        /// <param name="excludeFlagAnnotation"></param>
        /// <param name="excludeObjectContextVerifiableAnnotation"></param>
        /// <param name="excludeStrongVerifiableAnnotation"></param>
        /// <returns></returns>
        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false,
            bool excludeStrongVerifiableAnnotation = false)
            => _contract.GetQuietVerifiableAnnotations(
                excludeFlagAnnotation,
                excludeObjectContextVerifiableAnnotation,
                excludeStrongVerifiableAnnotation);

        /// <summary>
        /// Get strong verifiable annotations <br />
        /// 获取强验证注解
        /// </summary>
        /// <param name="excludeFlagAnnotation"></param>
        /// <param name="excludeObjectContextVerifiableAnnotation"></param>
        /// <returns></returns>
        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false)
            => _contract.GetStrongVerifiableAnnotations(
                excludeFlagAnnotation,
                excludeObjectContextVerifiableAnnotation);

        /// <summary>
        /// Get ObjectContext verifiable annotations <br />
        /// 获取带上下文的可验证注解
        /// </summary>
        /// <param name="excludeFlagAnnotation"></param>
        /// <returns></returns>
        public IEnumerable<IObjectContextVerifiableAnnotation> GetObjectContextVerifiableAnnotations
            (bool excludeFlagAnnotation = false)
            => _contract.GetObjectContextVerifiableAnnotations(excludeFlagAnnotation);

        /// <summary>
        /// Get flag annotations <br />
        /// 获取带 Flag 的注解
        /// </summary>
        /// <param name="excludeVerifiableAnnotation"></param>
        /// <returns></returns>
        public IEnumerable<IFlagAnnotation> GetFlagAnnotations(
            bool excludeVerifiableAnnotation = false)
            => _contract.GetFlagAnnotations(excludeVerifiableAnnotation);

        /// <summary>
        /// Get verifiable annotations <br />
        /// 获取可验证注解
        /// </summary>
        /// <param name="excludeFlagAnnotation"></param>
        /// <returns></returns>
        public IEnumerable<IVerifiable> GetVerifiableAnnotations(
            bool excludeFlagAnnotation = false)
            => _contract.GetVerifiableAnnotations(excludeFlagAnnotation);

        #endregion

        #region ConvertToObjectContext

        /// <summary>
        /// Convert to ObjectContext <br />
        /// 转换为可验证对象上下文 <see cref="VerifiableObjectContext"/>
        /// </summary>
        /// <returns></returns>
        public VerifiableObjectContext ConvertToObjectContext()
        {
            if (MemberType.IsBasicType())
                return _parentContext;

            var contract = VerifiableObjectContractManager.Resolve(MemberType);

            return new VerifiableObjectContext(Value, contract, _parentContext);
        }

        #endregion

        #region Factory

        /// <summary>
        /// Create <br />
        /// 创建
        /// </summary>
        /// <param name="value"></param>
        /// <param name="contract"></param>
        /// <param name="parentContext"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static VerifiableMemberContext Create<T>(T value, VerifiableMemberContract contract, VerifiableObjectContext parentContext = default)
        {
            return new(value, contract, parentContext);
        }

        #endregion

        #region Expose

        internal VerifiableMemberContract ExposeContract() => _contract;

        internal ICustomVerifiableMemberContractImpl ExposeInternalImpl() => _contract.ExposeInternalImpl();

        #endregion

        enum ValueMode
        {
            DirectType,
            DirectValue,
            Dictionary,
        }
    }
}