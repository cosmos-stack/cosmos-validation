using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    public abstract class CustomValidator : IValidator, ICorrectValidator
    {
        protected readonly IValidationObjectResolver _objectResolver;

        protected CustomValidator(string name)
        {
            Name = name;
            _objectResolver = new BuildInObjectResolver();
        }

        protected CustomValidator(string name, IValidationObjectResolver objectResolver)
        {
            Name = name;
            _objectResolver = objectResolver ?? new BuildInObjectResolver();
        }

        public string Name { get; }

        public bool IsAnonymous => string.IsNullOrEmpty(Name);

        public virtual VerifyResult Verify(Type type, object instance)
        {
            return VerifyImpl(_objectResolver.Resolve(type, instance));
        }

        protected abstract VerifyResult VerifyImpl(ObjectContext context);
        
        public VerifyResult VerifyViaContext(ObjectContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            return VerifyImpl(context);
        }
    }

    public abstract class CustomValidator<T> : CustomValidator, IValidator<T>, ICorrectValidator<T>
    {
        protected CustomValidator(string name) : base(name) { }

        protected CustomValidator(string name, IValidationObjectResolver objectResolver) : base(name, objectResolver) { }

        public virtual VerifyResult Verify(T instance)
        {
            return VerifyImpl(_objectResolver.Resolve(instance));
        }

        public override VerifyResult Verify(Type type, object instance)
        {
            if (instance is T t)
                return Verify(t);
            return VerifyResult.UnexpectedType;
        }
    }

    internal sealed class SealedValidator : CustomValidator
    {
        public SealedValidator() : base("SealedValidator") { }

        protected override VerifyResult VerifyImpl(ObjectContext context)
        {
            return VerifyResult.Success;
        }

        public static SealedValidator Instance { get; } = new();
    }

    internal sealed class SealedValidator<T> : CustomValidator<T>
    {
        public SealedValidator() : base("SealedValidator`1") { }

        protected override VerifyResult VerifyImpl(ObjectContext context)
        {
            return VerifyResult.Success;
        }

        public static SealedValidator<T> Instance { get; } = new();
    }
}