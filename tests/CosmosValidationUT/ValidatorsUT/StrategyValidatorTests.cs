using System.Collections.Generic;
using Cosmos.Validation;
using Cosmos.Validation.Validators;
using CosmosValidationUT.Models;
using CosmosValidationUT.StrategyUT;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "StrategyValidator")]
    public class StrategyValidatorTests
    {
        public StrategyValidatorTests()
        {
            RightModel = new NiceBoat() {Name = "Good", Length = 10, Width = 10};
            WrongModel = new NiceBoat() {Name = "", Length = -10, Width = -1};

            RightDictionary = new Dictionary<string, object> {{"Name", "Good"}, {"Length", 10}, {"Width", 10}};
            WrongDictionary = new Dictionary<string, object> {{"Name", ""}, {"Length", -10}, {"Width", -10}};

            Options = new ValidationOptions()
            {
                AnnotationEnabled = false,
                CustomValidatorEnabled = false
            };
        }

        private ValidationOptions Options { get; set; }

        private NiceBoat RightModel { get; set; }

        private NiceBoat WrongModel { get; set; }

        private IDictionary<string, object> RightDictionary { get; set; }

        private IDictionary<string, object> WrongDictionary { get; set; }

        [Fact(DisplayName = "Verify an entity for NormalNiceBoatStrategy validator test")]
        public void NormalNiceBoatStrategyValidatorAndVerifyEntityTest()
        {
            var validator = StrategyValidator.By<NormalNiceBoatStrategy>(Options);

            validator.ShouldNotBeNull();
            validator.Verify(typeof(NiceBoat), RightModel).IsValid.ShouldBeTrue();
            validator.Verify(typeof(NiceBoat), WrongModel).IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "Verify one member for NormalNiceBoatStrategy validator test")]
        public void NormalNiceBoatStrategyValidatorAndVerifyOneTest()
        {
            var validator = StrategyValidator.By<NormalNiceBoatStrategy>(Options);

            validator.ShouldNotBeNull();
            validator.VerifyOne(typeof(NiceBoat), "Good", "Name").IsValid.ShouldBeTrue();
            validator.VerifyOne(typeof(NiceBoat), 10, "Length").IsValid.ShouldBeTrue();
            validator.VerifyOne(typeof(NiceBoat), 10, "Width").IsValid.ShouldBeTrue();

            validator.VerifyOne(typeof(NiceBoat), "", "Name").IsValid.ShouldBeFalse();
            validator.VerifyOne(typeof(NiceBoat), -10, "Length").IsValid.ShouldBeFalse();
            validator.VerifyOne(typeof(NiceBoat), -10, "Width").IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "Verify many by a dictionary for NormalNiceBoatStrategy validator test")]
        public void NormalNiceBoatStrategyValidatorAndVerifyManyTest()
        {
            var validator = StrategyValidator.By<NormalNiceBoatStrategy>(Options);

            validator.ShouldNotBeNull();
            validator.VerifyMany(typeof(NiceBoat), RightDictionary).IsValid.ShouldBeTrue();
            validator.VerifyMany(typeof(NiceBoat), WrongDictionary).IsValid.ShouldBeFalse();
        }
    }
}