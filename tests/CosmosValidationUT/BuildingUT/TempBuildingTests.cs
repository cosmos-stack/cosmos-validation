using CosmosStack.Validation;
using CosmosStack.Validation.Registrars;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.BuildingUT
{
    [Trait("BuildingUT", "TempBuilding")]
    public class TempBuildingTests
    {
        [Fact(DisplayName = "TempBuild test")]
        public void TempBuildTest()
        {
            var model1 = new NiceBoat {Name = "Haha"};
            var model2 = new NiceBoat {Name = ""};

            var options = new ValidationOptions()
            {
                AnnotationEnabled = false,
                CustomValidatorEnabled = false
            };

            var handler = ValidationRegistrar.DefaultRegistrar
                                             .ForType<NiceBoat>().ForMember(x => x.Name).NotEmpty()
                                             .TempBuild(options);

            handler.ShouldNotBeNull();
            handler.Verify(model1).IsValid.ShouldBeTrue();
            handler.Verify(model2).IsValid.ShouldBeFalse();
        }
    }
}