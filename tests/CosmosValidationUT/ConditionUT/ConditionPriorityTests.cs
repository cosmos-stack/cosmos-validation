using Cosmos.Date;
using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ConditionUT
{
    [Trait("ConditionUT", "ConditionPriorityTests")]
    public class ConditionPriorityTests
    {
        public ConditionPriorityTests()
        {
            ValidationProjectManager = new BuildInProjectManager();
            VerifiableObjectResolver = new DefaultVerifiableObjectResolver();
        }

        private IValidationProjectManager ValidationProjectManager { get; set; }
        private IVerifiableObjectResolver VerifiableObjectResolver { get; set; }

        [Fact]
        public void ConditionPriorityTest()
        {
            var options = new ValidationOptions();
            options.AnnotationEnabled = false;
            options.FailureIfInstanceIsNull = true;
            options.FailureIfProjectNotMatch = false;
            options.CustomValidatorEnabled = false;

            var provider = new ValidationProvider(ValidationProjectManager, VerifiableObjectResolver, options);

            ValidationRegistrar.ForProvider(provider, "UT_ConditionPriorityTest")
                               .ForType<NiceBoat>()
                               .ForMember(x => x.Name).MaxLength(10).And().MinLength(8).Or().MaxLength(20).And().MinLength(18)
                               .Build();

            var validator = ValidationMe.Use("UT_ConditionPriorityTest").Resolve<NiceBoat>();

            var instance = new NiceBoat
            {
                Name = "NiceBoat1234567890Z", //19
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = validator.Verify(instance);

            result1.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
        }
    }
}