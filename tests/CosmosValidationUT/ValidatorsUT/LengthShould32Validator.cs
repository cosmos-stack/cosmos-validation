using Cosmos.Validation;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace CosmosValidationUT.ValidatorsUT
{
    public class Length32Model
    {
        public string Name { get; set; }
    }

    public class LengthShould32Validator : CustomValidator<Length32Model>
    {
        private ValidationOptions _options { get; set; }

        public LengthShould32Validator() : base("LengthShould32Validator")
        {
            _options = new ValidationOptions();
        }

        public LengthShould32Validator(ValidationOptions options) : base("LengthShould32Validator")
        {
            _options = options ?? new ValidationOptions();
        }

        public override VerifyResult Verify(Length32Model instance)
        {
            if (instance is null) return _options.ReturnNullReferenceOrSuccess();
            if (instance.Name.Length == 32)
                return VerifyResult.Success;
            return new VerifyResult(new VerifyFailure("Length32Instance", "Should length == 32", instance.Name));
        }

        protected override VerifyResult VerifyImpl(ObjectContext context)
        {
            var valueContext = context?.GetValue("Name");
            if (valueContext is null) return _options.ReturnNullReferenceOrSuccess();
            if (valueContext.Value is string {Length: 32})
                return VerifyResult.Success;
            return new VerifyResult(new VerifyFailure(valueContext.MemberName, "Should length == 32", valueContext.Value));
        }

        protected override VerifyResult VerifyOneImpl(ObjectValueContext context)
        {
            if (context is null) return _options.ReturnNullReferenceOrSuccess();
            if (context.MemberName != "Name") return VerifyResult.MemberIsNotExists("Name");
            if (context.Value is string {Length: 32})
                return VerifyResult.Success;
            return new VerifyResult(new VerifyFailure(context.MemberName, "Should length == 32", context.Value));
        }
    }
}