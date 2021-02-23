using System.Collections.Generic;
using Cosmos.Date;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ObjectUT
{
    [Trait("ObjectUT", "ValueContext")]
    public class ValueContextTests
    {
        public ValueContextTests()
        {
            _objectResolver = new BuildInObjectResolver();
        }

        private readonly IValidationObjectResolver _objectResolver;

        [Fact(DisplayName = "To test get ValueContext from Instance-ObjectContext")]
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
            value1.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            value1.Value.ShouldBe("NiceBoat1000");
            
            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            value2.Value.ShouldBe(1000);

            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            value3.Value.ShouldBe(30);

            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.ObjectValueKind.ShouldBe(ObjectValueKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            value4.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));

            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
            value5.Value.ShouldBe("nice@boat.com");
        }

        [Fact(DisplayName = "To test get ValueContext from Dictionary(full)-ObjectContext")]
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
            value1.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            value1.Value.ShouldBe("NiceBoat1000");

            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            value2.Value.ShouldBe(1000);

            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            value3.Value.ShouldBe(30);

            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.ObjectValueKind.ShouldBe(ObjectValueKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            value4.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));

            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
            value5.Value.ShouldBe("nice@boat.com");
        }

        [Fact(DisplayName = "To test get ValueContext from Dictionary(less)-ObjectContext")]
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
            value1.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            value1.Value.ShouldBe("NiceBoat1000");

            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            value2.Value.ShouldBe(default);

            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            value3.Value.ShouldBe(default);

            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.ObjectValueKind.ShouldBe(ObjectValueKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            value4.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));

            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
            value5.Value.ShouldBe("nice@boat.com");
        }

        [Fact(DisplayName = "To test get ValueContext for Property Name by direct value")]
        public void GetValue_Name_ByDirectValueTest()
        {
            var objectContract = ObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetValueContract("Name");
            var valueContext = ObjectValueContext.Create("NiceBoat1000", valueContract);

            valueContext.MemberName.ShouldBe("Name");
            valueContext.MemberType.ShouldBe(TypeClass.StringClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe("NiceBoat1000");
        }

        [Fact(DisplayName = "To test get ValueContext for Property Length by direct value")]
        public void GetValue_Length_ByDirectValueTest()
        {
            var objectContract = ObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetValueContract("Length");
            var valueContext = ObjectValueContext.Create(1000, valueContract);

            valueContext.MemberName.ShouldBe("Length");
            valueContext.MemberType.ShouldBe(TypeClass.LongClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe(1000);
        }

        [Fact(DisplayName = "To test get ValueContext for Property Width by direct value")]
        public void GetValue_Width_ByDirectValueTest()
        {
            var objectContract = ObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetValueContract("Width");
            var valueContext = ObjectValueContext.Create(30, valueContract);

            valueContext.MemberName.ShouldBe("Width");
            valueContext.MemberType.ShouldBe(TypeClass.LongClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe(30);
        }

        [Fact(DisplayName = "To test get ValueContext for Field CreateTime by direct value")]
        public void GetValue_CreateTime_ByDirectValueTest()
        {
            var objectContract = ObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetValueContract("CreateTime");
            var valueContext = ObjectValueContext.Create(DateTimeFactory.Create(2020, 12, 21), valueContract);

            valueContext.MemberName.ShouldBe("CreateTime");
            valueContext.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.ObjectValueKind.ShouldBe(ObjectValueKind.Field);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe(DateTimeFactory.Create(2020, 12, 21));
        }

        [Fact(DisplayName = "To test get ValueContext for Property Email by direct value")]
        public void GetValue_Email_ByDirectValueTest()
        {
            var objectContract = ObjectContractManager.Resolve(typeof(NiceBoat));
            var valueContract = objectContract.GetValueContract("Email");
            var valueContext = ObjectValueContext.Create("nice@boat.com", valueContract);

            valueContext.MemberName.ShouldBe("Email");
            valueContext.MemberType.ShouldBe(TypeClass.StringClazz);
            valueContext.DeclaringType.ShouldBe(typeof(NiceBoat));
            valueContext.ObjectValueKind.ShouldBe(ObjectValueKind.Property);
            valueContext.IsBasicType.ShouldBeTrue();
            valueContext.IncludeAnnotations.ShouldBeTrue();
            valueContext.Value.ShouldBe("nice@boat.com");
        }
    }
}