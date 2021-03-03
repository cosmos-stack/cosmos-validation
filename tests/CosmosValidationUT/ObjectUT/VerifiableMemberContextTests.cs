using System.Collections.Generic;
using Cosmos.Date;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ObjectUT
{
    [Trait("ObjectUT", "VerifiableValueContext")]
    public class VerifiableMemberContextTests
    {
        public VerifiableMemberContextTests()
        {
            _objectResolver = new DefaultVerifiableObjectResolver();
        }

        private readonly IVerifiableObjectResolver _objectResolver;

        [Fact(DisplayName = "To test get VerifiableMemberContext from Instance-VerifiableObjectContext")]
        public void GetValueContextFromInstanceObjectContextTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var context = _objectResolver.Resolve<NiceBoat>(instance);

            var value1 = context.GetValue("Name");
            var value2 = context.GetValue("Length");
            var value3 = context.GetValue("Width");
            var value4 = context.GetValue("CreateTime");
            var value5 = context.GetValue("Email");

            value1.MemberName.ShouldBe("Name");
            value1.MemberType.ShouldBe(TypeClass.StringClazz);
            value1.DeclaringType.ShouldBe(typeof(NiceBoat));
            value1.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            value1.Value.ShouldBe("NiceBoat1000");
            
            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            value2.Value.ShouldBe(1000);

            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            value3.Value.ShouldBe(30);

            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.MemberKind.ShouldBe(VerifiableMemberKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            value4.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));

            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
            value5.Value.ShouldBe("nice@boat.com");
        }

        [Fact(DisplayName = "To test get VerifiableMemberContext from Dictionary(full)-VerifiableObjectContext")]
        public void GetValueContextFromDictionaryObjectContextTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var context = _objectResolver.Resolve<NiceBoat>(d);

            var value1 = context.GetValue("Name");
            var value2 = context.GetValue("Length");
            var value3 = context.GetValue("Width");
            var value4 = context.GetValue("CreateTime");
            var value5 = context.GetValue("Email");

            value1.MemberName.ShouldBe("Name");
            value1.MemberType.ShouldBe(TypeClass.StringClazz);
            value1.DeclaringType.ShouldBe(typeof(NiceBoat));
            value1.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            value1.Value.ShouldBe("NiceBoat1000");

            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            value2.Value.ShouldBe(1000);

            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            value3.Value.ShouldBe(30);

            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.MemberKind.ShouldBe(VerifiableMemberKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            value4.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));

            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
            value5.Value.ShouldBe("nice@boat.com");
        }

        [Fact(DisplayName = "To test get VerifiableMemberContext from Dictionary(less)-VerifiableObjectContext")]
        public void GetValueContextFromDictionaryLessObjectContextTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var context = _objectResolver.Resolve<NiceBoat>(d);

            var value1 = context.GetValue("Name");
            var value2 = context.GetValue("Length");
            var value3 = context.GetValue("Width");
            var value4 = context.GetValue("CreateTime");
            var value5 = context.GetValue("Email");

            value1.MemberName.ShouldBe("Name");
            value1.MemberType.ShouldBe(TypeClass.StringClazz);
            value1.DeclaringType.ShouldBe(typeof(NiceBoat));
            value1.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            value1.Value.ShouldBe("NiceBoat1000");

            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            value2.Value.ShouldBe(default);

            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            value3.Value.ShouldBe(default);

            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.MemberKind.ShouldBe(VerifiableMemberKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            value4.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));

            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
            value5.Value.ShouldBe("nice@boat.com");
        }

        [Fact(DisplayName = "To test get VerifiableMemberContext for Property Name by direct value")]
        public void GetValue_Name_ByDirectValueTest()
        {
            var objectContract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetMemberContract("Name");
            var valueContext = VerifiableMemberContext.Create("NiceBoat1000", valueContract);

            valueContext.MemberName.ShouldBe("Name");
            valueContext.MemberType.ShouldBe(TypeClass.StringClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe("NiceBoat1000");
        }

        [Fact(DisplayName = "To test get VerifiableMemberContext for Property Length by direct value")]
        public void GetValue_Length_ByDirectValueTest()
        {
            var objectContract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetMemberContract("Length");
            var valueContext = VerifiableMemberContext.Create(1000, valueContract);

            valueContext.MemberName.ShouldBe("Length");
            valueContext.MemberType.ShouldBe(TypeClass.LongClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe(1000);
        }

        [Fact(DisplayName = "To test get VerifiableMemberContext for Property Width by direct value")]
        public void GetValue_Width_ByDirectValueTest()
        {
            var objectContract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetMemberContract("Width");
            var valueContext = VerifiableMemberContext.Create(30, valueContract);

            valueContext.MemberName.ShouldBe("Width");
            valueContext.MemberType.ShouldBe(TypeClass.LongClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe(30);
        }

        [Fact(DisplayName = "To test get VerifiableMemberContext for Field CreateTime by direct value")]
        public void GetValue_CreateTime_ByDirectValueTest()
        {
            var objectContract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetMemberContract("CreateTime");
            var valueContext = VerifiableMemberContext.Create(DateTimeFactory.Create(2020, 12, 21), valueContract);

            valueContext.MemberName.ShouldBe("CreateTime");
            valueContext.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.MemberKind.ShouldBe(VerifiableMemberKind.Field);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));
        }

        [Fact(DisplayName = "To test get VerifiableMemberContext for Property Email by direct value")]
        public void GetValue_Email_ByDirectValueTest()
        {
            var objectContract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetMemberContract("Email");
            var valueContext = VerifiableMemberContext.Create("nice@boat.com", valueContract);

            valueContext.MemberName.ShouldBe("Email");
            valueContext.MemberType.ShouldBe(TypeClass.StringClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe("nice@boat.com");
        }
    }
}