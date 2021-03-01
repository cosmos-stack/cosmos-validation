using System.Collections.Generic;
using Cosmos.Date;
using Cosmos.Validation;
using Cosmos.Validation.Validators;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ValidatorsUT
{
    [Trait("ValidatorsUT", "AnnotationValidator")]
    public class AnnotationValidatorTests
    {
        public AnnotationValidatorTests()
        {
            var _options = new ValidationOptions();
            _options.AnnotationEnabled = true;
            _options.FailureIfProjectNotMatch = false;
            AnnotationValidator = AnnotationValidator.GetInstance(_options);
        }

        private AnnotationValidator AnnotationValidator { get; }

        [Fact(DisplayName = "Verify an instance and return a success VerifyResult.")]
        public void VerifyAnByInstanceShouldBeSuccessTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var result1 = AnnotationValidator.Verify(instance);
            var result2 = AnnotationValidator.Verify(typeof(NiceBoat), instance);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
            result2.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify an instance and return a failure VerifyResult.")]
        public void VerifyAnByInstanceShouldBeFailureTest()
        {
            var instance = new NiceBoat
            {
                Name = "",
                Length = -1000,
                Width = -30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@@boat.com"
            };

            var result1 = AnnotationValidator.Verify(instance);
            var result2 = AnnotationValidator.Verify(typeof(NiceBoat), instance);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeFalse();
            result2.IsValid.ShouldBeFalse();

            result1.Errors.Count.ShouldBe(4);
            result2.Errors.Count.ShouldBe(4);
        }

        [Fact(DisplayName = "Verify a dictionary and return a success VerifyResult.")]
        public void VerifyAnByDictionaryShouldBeSuccessTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var result1 = AnnotationValidator.Verify(d);
            var result2 = AnnotationValidator.Verify(typeof(NiceBoat), d);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeFalse();
            result2.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify a dictionary and return a failure VerifyResult.")]
        public void VerifyAnByDictionaryShouldBeFailureTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "",
                ["Length"] = -1000,
                ["Width"] = -30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@@boat.com"
            };

            var result1 = AnnotationValidator.Verify(d);
            var result2 = AnnotationValidator.Verify(typeof(NiceBoat), d);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeFalse();
            result2.IsValid.ShouldBeFalse();

            result1.Errors.Count.ShouldBe(1);
            result2.Errors.Count.ShouldBe(4);
        }

        [Fact(DisplayName = "Verify a basic type.")]
        public void VerifyAnByBasicTypeTest()
        {
            var result = AnnotationValidator.Verify(1);

            result.ShouldNotBeNull();
            result.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify a enum type.")]
        public void VerifyAnByEnumTypeTest()
        {
            var result = AnnotationValidator.Verify(NiceEnum.Black);

            result.ShouldNotBeNull();
            result.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify a structure and return a success VerifyResult.")]
        public void VerifyAnByStructureTypeAndShouldBeSuccessTest()
        {
            var @struct = new NiceStructWithAnnotation("Nice", 10);

            var result1 = AnnotationValidator.Verify(@struct);
            var result2 = AnnotationValidator.Verify(typeof(NiceStructWithAnnotation), @struct);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
            result2.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify a structure and return a failure VerifyResult.")]
        public void VerifyAnByStructureTypeAndShouldBeFailureTest()
        {
            var @struct = new NiceStructWithAnnotation("", -10);

            var result1 = AnnotationValidator.Verify(@struct);
            var result2 = AnnotationValidator.Verify(typeof(NiceStructWithAnnotation), @struct);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeFalse();
            result2.IsValid.ShouldBeFalse();

            result1.Errors.Count.ShouldBe(2);
            result2.Errors.Count.ShouldBe(2);
        }

#if !NETFRAMEWORK && !NETCOREAPP3_1

        [Fact(DisplayName = "Verify a record and return a success VerifyResult.")]
        public void VerifyAnByRecordTypeAndShouldBeSuccessTest()
        {
            var recode = new NiceRecordWithAnnotation {Name = "Nice", Age = 10};

            var result1 = AnnotationValidator.Verify(recode);
            var result2 = AnnotationValidator.Verify(typeof(NiceRecordWithAnnotation), recode);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeTrue();
            result2.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Verify a record and return a failure VerifyResult.")]
        public void VerifyAnByRecordTypeAndShouldBeFailureTest()
        {
            var recode = new NiceRecordWithAnnotation {Name = "", Age = -10};

            var result1 = AnnotationValidator.Verify(recode);
            var result2 = AnnotationValidator.Verify(typeof(NiceRecordWithAnnotation), recode);

            result1.ShouldNotBeNull();
            result2.ShouldNotBeNull();

            result1.IsValid.ShouldBeFalse();
            result2.IsValid.ShouldBeFalse();

            result1.Errors.Count.ShouldBe(2);
            result2.Errors.Count.ShouldBe(2);
        }
#endif

        [Fact(DisplayName = "VerifyOne a type and return a success VerifyResult.")]
        public void VerifyOneTypeAndShouldBeSuccessTest()
        {
            AnnotationValidator.VerifyOne(typeof(string), "Nice", "Name").IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne(typeof(long), 10, "Length").IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne(typeof(long), 10, "Width").IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne(typeof(string), "2890000@qq.com", "Email").IsValid.ShouldBeTrue();

            AnnotationValidator.VerifyOne<NiceBoat, string>(x => x.Name, "Nice").IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne<NiceBoat, long>(x => x.Length, 1000).IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne<NiceBoat, long>(x => x.Width, 30).IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne<NiceBoat, string>(x => x.Email, "2000@qq.com").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyOne a type and return a failure VerifyResult.")]
        public void VerifyOneTypeAndShouldBeFailureTest()
        {
            AnnotationValidator.VerifyOne(typeof(NiceBoat), "", "Name").IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne(typeof(NiceBoat), -10, "Length").IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne(typeof(NiceBoat), -10, "Width").IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne(typeof(NiceBoat), "2890000@", "Email").IsValid.ShouldBeFalse();

            AnnotationValidator.VerifyOne<NiceBoat, string>(x => x.Name, "").IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne<NiceBoat, long>(x => x.Length, -1000).IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne<NiceBoat, long>(x => x.Width, -30).IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne<NiceBoat, string>(x => x.Email, "2000@").IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "VerifyOne a type with wrong member name and return a failure VerifyResult.")]
        public void VerifyOneTypeWithWrongMemberTest()
        {
            AnnotationValidator.VerifyOne(typeof(NiceBoat), "Nice", "NameNon").IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "VerifyOne a structure and return a success VerifyResult.")]
        public void VerifyOneByStructureTypeAndShouldBeSuccessTest()
        {
            AnnotationValidator.VerifyOne(typeof(NiceBoat), "Nice", "Name").IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne(typeof(NiceBoat), 10, "Length").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyOne a structure and return a failure VerifyResult.")]
        public void VerifyOneByStructureTypeAndShouldBeFailureTest()
        {
            AnnotationValidator.VerifyOne(typeof(NiceBoat), "", "Name").IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne(typeof(NiceBoat), -10, "Length").IsValid.ShouldBeFalse();
        }

#if !NETFRAMEWORK && !NETCOREAPP3_1

        [Fact(DisplayName = "VerifyOne a record and return a success VerifyResult.")]
        public void VerifyOneByRecordTypeAndShouldBeSuccessTest()
        {
            AnnotationValidator.VerifyOne(typeof(NiceBoat), "Nice", "Name").IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyOne(typeof(NiceBoat), 10, "Length").IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyOne a record and return a failure VerifyResult.")]
        public void VerifyOneByRecordTypeAndShouldBeFailureTest()
        {
            AnnotationValidator.VerifyOne(typeof(NiceBoat), "", "Name").IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyOne(typeof(NiceBoat), -10, "Length").IsValid.ShouldBeFalse();
        }
#endif

        [Fact(DisplayName = "VerifyMany a type and return a success VerifyResult.")]
        public void VerifyManyTypeAndShouldBeSuccessTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            AnnotationValidator.VerifyMany(typeof(NiceBoat), d).IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyMany<NiceBoat>(d).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyMany a type and return a failure VerifyResult.")]
        public void VerifyManyTypeAndShouldBeFailureTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "",
                ["Length"] = -1000,
                ["Width"] = -30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@@boat.com"
            };

            AnnotationValidator.VerifyMany(typeof(NiceBoat), d).IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyMany<NiceBoat>(d).IsValid.ShouldBeFalse();
        }

        [Fact(DisplayName = "VerifyMany a structure and return a success VerifyResult.")]
        public void VerifyManyByStructureTypeAndShouldBeSuccessTest()
        {
            var d = new Dictionary<string, object> {["Name"] = "Nice", ["Age"] = 10};
            AnnotationValidator.VerifyMany(typeof(NiceStructWithAnnotation), d).IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyMany<NiceStructWithAnnotation>(d).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyMany a structure and return a failure VerifyResult.")]
        public void VerifyManyByStructureTypeAndShouldBeFailureTest()
        {
            var d = new Dictionary<string, object> {["Name"] = "", ["Age"] = -10};
            AnnotationValidator.VerifyMany(typeof(NiceStructWithAnnotation), d).IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyMany<NiceStructWithAnnotation>(d).IsValid.ShouldBeFalse();
        }

#if !NETFRAMEWORK && !NETCOREAPP3_1

        [Fact(DisplayName = "VerifyMany a record and return a success VerifyResult.")]
        public void VerifyManyByRecordTypeAndShouldBeSuccessTest()
        {
            var d = new Dictionary<string, object> {["Name"] = "Nice", ["Age"] = 10};
            AnnotationValidator.VerifyMany(typeof(NiceRecordWithAnnotation), d).IsValid.ShouldBeTrue();
            AnnotationValidator.VerifyMany<NiceRecordWithAnnotation>(d).IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "VerifyMany a record and return a failure VerifyResult.")]
        public void VerifyManyByRecordTypeAndShouldBeFailureTest()
        {
            var d = new Dictionary<string, object> {["Name"] = "", ["Age"] = -10};
            AnnotationValidator.VerifyMany(typeof(NiceRecordWithAnnotation), d).IsValid.ShouldBeFalse();
            AnnotationValidator.VerifyMany<NiceRecordWithAnnotation>(d).IsValid.ShouldBeFalse();
        }
#endif
    }
}