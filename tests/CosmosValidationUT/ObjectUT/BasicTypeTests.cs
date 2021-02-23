using System;
using System.Linq;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ObjectUT
{
    [Trait("ObjectUT", "BasicTypeTests")]
    public class BasicTypeTests
    {
        [Fact(DisplayName = "To test create an ObjectContract by basic type.")]
        public void BasicTypeToObjectContractTest()
        {
            var int16Contract = ObjectContractManager.Resolve<Int16>();
            var int32Contract = ObjectContractManager.Resolve<Int32>();
            var int64Contract = ObjectContractManager.Resolve<Int64>();
            var single32Contract = ObjectContractManager.Resolve<float>();
            var single64Contract = ObjectContractManager.Resolve<double>();

            int16Contract.ShouldNotBeNull();
            int32Contract.ShouldNotBeNull();
            int64Contract.ShouldNotBeNull();
            single32Contract.ShouldNotBeNull();
            single64Contract.ShouldNotBeNull();

            int16Contract.Type.ShouldBe(typeof(Int16));
            int32Contract.Type.ShouldBe(typeof(Int32));
            int64Contract.Type.ShouldBe(typeof(Int64));
            single32Contract.Type.ShouldBe(typeof(float));
            single64Contract.Type.ShouldBe(typeof(double));

            int16Contract.IncludeAnnotations.ShouldBeFalse();
            int32Contract.IncludeAnnotations.ShouldBeFalse();
            int64Contract.IncludeAnnotations.ShouldBeFalse();
            single32Contract.IncludeAnnotations.ShouldBeFalse();
            single64Contract.IncludeAnnotations.ShouldBeFalse();

            int16Contract.ObjectKind.ShouldBe(ObjectKind.BasicType);
            int32Contract.ObjectKind.ShouldBe(ObjectKind.BasicType);
            int64Contract.ObjectKind.ShouldBe(ObjectKind.BasicType);
            single32Contract.ObjectKind.ShouldBe(ObjectKind.BasicType);
            single64Contract.ObjectKind.ShouldBe(ObjectKind.BasicType);

            int16Contract.IsBasicType().ShouldBeTrue();
            int32Contract.IsBasicType().ShouldBeTrue();
            int64Contract.IsBasicType().ShouldBeTrue();
            single32Contract.IsBasicType().ShouldBeTrue();
            single64Contract.IsBasicType().ShouldBeTrue();

            int16Contract.GetAllValueContracts().Count().ShouldBe(1);
            int32Contract.GetAllValueContracts().Count().ShouldBe(1);
            int64Contract.GetAllValueContracts().Count().ShouldBe(1);
            single32Contract.GetAllValueContracts().Count().ShouldBe(1);
            single64Contract.GetAllValueContracts().Count().ShouldBe(1);

            int16Contract.GetValueContract(ObjectValueContract.BASIC_TYPE).MemberType.ShouldBe(typeof(Int16));
            int32Contract.GetValueContract(ObjectValueContract.BASIC_TYPE).MemberType.ShouldBe(typeof(Int32));
            int64Contract.GetValueContract(ObjectValueContract.BASIC_TYPE).MemberType.ShouldBe(typeof(Int64));
            single32Contract.GetValueContract(ObjectValueContract.BASIC_TYPE).MemberType.ShouldBe(typeof(float));
            single64Contract.GetValueContract(ObjectValueContract.BASIC_TYPE).MemberType.ShouldBe(typeof(double));

            int16Contract.GetValueContract(0).MemberType.ShouldBe(typeof(Int16));
            int32Contract.GetValueContract(0).MemberType.ShouldBe(typeof(Int32));
            int64Contract.GetValueContract(0).MemberType.ShouldBe(typeof(Int64));
            single32Contract.GetValueContract(0).MemberType.ShouldBe(typeof(float));
            single64Contract.GetValueContract(0).MemberType.ShouldBe(typeof(double));
        }

        [Fact(DisplayName = "To test create an ObjectContract by enum type.")]
        public void EnumTypeToObjectContractTest()
        {
            var contract = ObjectContractManager.Resolve<NiceEnum>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceEnum));
            contract.IncludeAnnotations.ShouldBeFalse();
            contract.ObjectKind.ShouldBe(ObjectKind.BasicType);
            contract.IsBasicType().ShouldBeTrue();

            contract.GetAllValueContracts().Count().ShouldBe(1);
            contract.GetValueContract(ObjectValueContract.BASIC_TYPE).MemberType.ShouldBe(typeof(NiceEnum));
            contract.GetValueContract(0).MemberType.ShouldBe(typeof(NiceEnum));
        }

        [Fact(DisplayName = "To test create an ObjectContract by struct type.")]
        public void StructureTypeToObjectContractTest()
        {
            var contract = ObjectContractManager.Resolve<NiceStruct>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceStruct));
            contract.IncludeAnnotations.ShouldBeFalse();
            contract.ObjectKind.ShouldBe(ObjectKind.StructureType);
            contract.IsBasicType().ShouldBeFalse();

            contract.GetAllValueContracts().Count().ShouldBe(2);
        }

#if !NETFRAMEWORK && !NETCOREAPP3_1

        [Fact(DisplayName = "To test create an ObjectContract by record type.")]
        public void RecordTypeToObjectContractTest()
        {
            var contract = ObjectContractManager.Resolve<NiceRecord>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceRecord));
            contract.IncludeAnnotations.ShouldBeFalse();
            contract.ObjectKind.ShouldBe(ObjectKind.StructureType);
            contract.IsBasicType().ShouldBeFalse();

            contract.GetAllValueContracts().Count().ShouldBe(2);
        }

#endif

        [Fact(DisplayName = "To test create an ObjectContext by 'With' from basic type.")]
        public void BasicTypeWithValueToObjectContextTest()
        {
            var contract = ObjectContractManager.Resolve<int>();
            var value = 1;
            var context1 = contract.WithInstance(value);
            var context2 = contract.WithInstance(value, "Int32");

            context1.ShouldNotBeNull();
            context2.ShouldNotBeNull();
            
            context1.InstanceName.ShouldBe(ObjectValueContract.BASIC_TYPE);
            context2.InstanceName.ShouldBe("Int32");
            
            context1.GetValues().Count().ShouldBe(1);
            context2.GetValues().Count().ShouldBe(1);
            
            context1.GetValue(0).MemberName.ShouldBe(ObjectValueContract.BASIC_TYPE);
            context2.GetValue(0).MemberName.ShouldBe(ObjectValueContract.BASIC_TYPE);
            
            context1.GetValue(ObjectValueContract.BASIC_TYPE).Value.ShouldBe(1);
            context2.GetValue(ObjectValueContract.BASIC_TYPE).Value.ShouldBe(1);
        }

        [Fact(DisplayName = "To test create an ObjectContext by 'With' from enum type.")]
        public void EnumTypeWithValueToObjectContextTest()
        {
            var contract = ObjectContractManager.Resolve<NiceEnum>();
            var @enum = NiceEnum.Black;
            var context1 = contract.WithInstance(@enum);
            var context2 = contract.WithInstance(@enum, "NiceEnum");
            
            context1.InstanceName.ShouldBe(ObjectValueContract.BASIC_TYPE);
            context2.InstanceName.ShouldBe("NiceEnum");
            
            context1.GetValues().Count().ShouldBe(1);
            context2.GetValues().Count().ShouldBe(1);
            
            context1.GetValue(0).MemberName.ShouldBe(ObjectValueContract.BASIC_TYPE);
            context2.GetValue(0).MemberName.ShouldBe(ObjectValueContract.BASIC_TYPE);
            
            context1.GetValue(ObjectValueContract.BASIC_TYPE).Value.ShouldBe(NiceEnum.Black);
            context2.GetValue(ObjectValueContract.BASIC_TYPE).Value.ShouldBe(NiceEnum.Black);
        }

        [Fact(DisplayName = "To test create an ObjectContext by 'With' from struct type.")]
        public void StructureTypeWithValueToObjectContextTest()
        {
            var contract = ObjectContractManager.Resolve<NiceStruct>();
            var @struct = new NiceStruct("NiceBoat1000", 100);
            var context1 = contract.WithInstance(@struct);
            var context2 = contract.WithInstance(@struct, "NiceStruct");
            
            context1.InstanceName.ShouldBe("Instance");
            context2.InstanceName.ShouldBe("NiceStruct");
            
            context1.GetValues().Count().ShouldBe(2);
            context2.GetValues().Count().ShouldBe(2);
            
            context1.GetValue(0).MemberName.ShouldBe("Name");
            context2.GetValue(0).MemberName.ShouldBe("Name");
            
            context1.GetValue(1).MemberName.ShouldBe("Age");
            context2.GetValue(1).MemberName.ShouldBe("Age");
            
            context1.GetValue("Name").Value.ShouldBe("NiceBoat1000");
            context2.GetValue("Name").Value.ShouldBe("NiceBoat1000");
            
            context1.GetValue("Age").Value.ShouldBe(100);
            context2.GetValue("Age").Value.ShouldBe(100);
        }

#if !NETFRAMEWORK && !NETCOREAPP3_1

        [Fact(DisplayName = "To test create an ObjectContext by 'With' from record type.")]
        public void RecordTypeWithValueToObjectContextTest()
        {
            var contract = ObjectContractManager.Resolve<NiceRecord>();
            var record = new NiceRecord {Name = "NiceBoat1000", Age = 100};
            var context1 = contract.WithInstance(record);
            var context2 = contract.WithInstance(record, "NiceRecord");
            
            context1.InstanceName.ShouldBe("Instance");
            context2.InstanceName.ShouldBe("NiceRecord");
            
            context1.GetValues().Count().ShouldBe(2);
            context2.GetValues().Count().ShouldBe(2);
            
            context1.GetValue(0).MemberName.ShouldBe("Name");
            context2.GetValue(0).MemberName.ShouldBe("Name");
            
            context1.GetValue(1).MemberName.ShouldBe("Age");
            context2.GetValue(1).MemberName.ShouldBe("Age");
            
            context1.GetValue("Name").Value.ShouldBe("NiceBoat1000");
            context2.GetValue("Name").Value.ShouldBe("NiceBoat1000");
            
            context1.GetValue("Age").Value.ShouldBe(100);
            context2.GetValue("Age").Value.ShouldBe(100);
        }

#endif
    }
}