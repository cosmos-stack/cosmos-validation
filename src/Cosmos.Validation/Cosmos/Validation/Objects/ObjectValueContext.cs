using System;
using System.Collections.Generic;
using Cosmos.Validation.Annotations;

namespace Cosmos.Validation.Objects
{
    public class ObjectValueContext
    {
        private readonly object _parent;
        private readonly ObjectValueContract _contract;
        private readonly ObjectContext _parentContext;

        public ObjectValueContext(ObjectContext parentContext, ObjectValueContract contract)
        {
            _parentContext = parentContext;
            _parent = parentContext.Instance;
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
        }

        private bool _hasGot;
        private object _valueCached;

        public object Value
        {
            get
            {
                if (!_hasGot)
                {
                    _valueCached = _contract.GetValue(_parent);
                    _hasGot = true;
                }

                return _valueCached;
            }
        }

        public object GetValue() => Value;

        public TVal GetValue<TVal>() => Value.As<TVal>();

        public ObjectValueKind ObjectValueKind => _contract.ObjectValueKind;

        public Type DeclaringType => _contract.DeclaringType;

        public Type MemberType => _contract.MemberType;

        public string MemberName => _contract.MemberName;

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

        #region ToObjectContext

        public ObjectContext ToObjectContext()
        {
            if (MemberType.IsBasicType())
                return _parentContext;

            var contract = ObjectContractManager.Resolve(MemberType);

            return new ObjectContext(Value, contract);
        }

        #endregion
    }
}