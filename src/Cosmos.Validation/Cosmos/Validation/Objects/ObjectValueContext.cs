using System;
using System.Collections.Generic;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public class ObjectValueContext
    {
        private readonly ObjectValueContract _contract;
        private readonly ObjectContext _parentContext;

        // 如果为 true，则表示为强类型模式；如果为 false，则表示为字典模式
        private readonly ValueMode _valueMode;

        public ObjectValueContext(ObjectContext parentContext, ObjectValueContract contract, bool directMode)
        {
            _parentContext = parentContext;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _valueMode = directMode.Ifttt(() => ValueMode.DirectType, () => ValueMode.Dictionary);
        }

        private ObjectValueContext(object value, ObjectValueContract contract, ObjectContext parentContext)
        {
            _parentContext = parentContext;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _valueMode = ValueMode.DirectValue;

            _hasGot = true;
            _valueCached = value;
        }

        public ObjectValueKind ObjectValueKind => _contract.ObjectValueKind;

        public bool IsBasicType => _contract.IsBasicType;

        public Type DeclaringType => _contract.DeclaringType;

        public Type MemberType => _contract.MemberType;

        public string MemberName => _contract.MemberName;

        #region GetValue

        private bool _hasGot;
        private object _valueCached;

        public object Value
        {
            get
            {
                if (!_hasGot)
                {
                    _valueCached = _valueMode switch
                    {
                        ValueMode.DirectType => _contract.GetValue(_parentContext.Instance),
                        ValueMode.Dictionary => _contract.GetValue(_parentContext.KeyValueCollection),
                        _ => default
                    };

                    _hasGot = true;
                }

                return _valueCached;
            }
        }

        public object GetValue() => Value;

        public TVal GetValue<TVal>() => Value.As<TVal>();

        #endregion

        #region Annotations

        public bool IncludeAnnotations => _contract.IncludeAnnotations;

        public IReadOnlyCollection<Attribute> Attributes => _contract.Attributes;

        public IEnumerable<ValidationParameterAttribute> GetParameterAnnotations()
            => _contract.GetParameterAnnotations();

        public IEnumerable<IQuietVerifiableAnnotation> GetQuietVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false,
            bool excludeStrongVerifiableAnnotation = false)
            => _contract.GetQuietVerifiableAnnotations(
                excludeFlagAnnotation,
                excludeObjectContextVerifiableAnnotation,
                excludeStrongVerifiableAnnotation);

        public IEnumerable<IStrongVerifiableAnnotation> GetStrongVerifiableAnnotations(
            bool excludeFlagAnnotation = false,
            bool excludeObjectContextVerifiableAnnotation = false)
            => _contract.GetStrongVerifiableAnnotations(
                excludeFlagAnnotation,
                excludeObjectContextVerifiableAnnotation);

        public IEnumerable<IObjectContextVerifiableAnnotation> GetObjectContextVerifiableAnnotations
            (bool excludeFlagAnnotation = false)
            => _contract.GetObjectContextVerifiableAnnotations(excludeFlagAnnotation);

        public IEnumerable<IFlagAnnotation> GetFlagAnnotations(
            bool excludeVerifiableAnnotation = false)
            => _contract.GetFlagAnnotations(excludeVerifiableAnnotation);

        public IEnumerable<IVerifiable> GetVerifiableAnnotations(
            bool excludeFlagAnnotation = false)
            => _contract.GetVerifiableAnnotations(excludeFlagAnnotation);

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute => _contract.GetAttributes<TAttribute>();

        #endregion

        #region ConvertToObjectContext

        public ObjectContext ConvertToObjectContext()
        {
            if (MemberType.IsBasicType())
                return _parentContext;

            var contract = ObjectContractManager.Resolve(MemberType);

            return new ObjectContext(Value, contract);
        }

        #endregion

        #region ExposeInternalImpl

        internal ICustomValueContractImpl ExposeInternalImpl() => _contract.ExposeInternalImpl();

        #endregion

        #region Factory

        public static ObjectValueContext Create<T>(T value, ObjectValueContract contract, ObjectContext parentContext = default)
        {
            return new(value, contract, parentContext);
        }        

        #endregion

        enum ValueMode
        {
            DirectType,
            DirectValue,
            Dictionary,
        }
    }
}