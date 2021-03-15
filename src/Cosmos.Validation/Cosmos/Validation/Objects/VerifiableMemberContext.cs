using System;
using System.Collections.Generic;
using Cosmos.Conversions;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
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

        public string MemberName => _contract.MemberName;

        public Type DeclaringType => _contract.DeclaringType;

        public Type MemberType => _contract.MemberType;

        public bool IsBasicType => _contract.IsBasicType;

        public VerifiableMemberKind MemberKind => _contract.MemberKind;

        #region Value

        private bool _hasGot;
        private object _valueCached;

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

        public object GetParentInstance() => _parentContext?.Instance;

        public T GetParentInstance<T>() => (T) _parentContext?.Instance;

        #endregion

        #region Annotation / Attribute

        public bool IncludeAnnotations => _contract.IncludeAnnotations;

        public IReadOnlyCollection<Attribute> Attributes => _contract.Attributes;

        public IEnumerable<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute => _contract.GetAttributes<TAttribute>();

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

        #endregion

        #region ConvertToObjectContext

        public VerifiableObjectContext ConvertToObjectContext()
        {
            if (MemberType.IsBasicType())
                return _parentContext;

            var contract = VerifiableObjectContractManager.Resolve(MemberType);

            return new VerifiableObjectContext(Value, contract, _parentContext);
        }

        #endregion

        #region Factory

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