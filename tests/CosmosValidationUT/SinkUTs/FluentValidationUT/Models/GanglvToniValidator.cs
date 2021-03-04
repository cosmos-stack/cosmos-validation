using FluentValidation;

namespace CosmosValidationUT.SinkUTs.FluentValidationUT.Models
{
    public class GanglvToniValidator : AbstractValidator<GanglvToni>
    {
        public GanglvToniValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(10);
            RuleFor(r => r.Age).GreaterThanOrEqualTo(10).LessThanOrEqualTo(20);
        }
    }
}