using System;
using System.Linq;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;
using FluentAssertions;
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
            var int16Contract = VerifiableObjectContractManager.Resolve<Int16>();
            var int32Contract = VerifiableObjectContractManager.Resolve<Int32>();
            var int64Contract = VerifiableObjectContractManager.Resolve<Int64>();
            var single32Contract = VerifiableObjectContractManager.Resolve<float>();
            var single64Contract = VerifiableObjectContractManager.Resolve<double>();

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

            int16Contract.ObjectKind.ShouldBe(VerifiableObjectKind.BasicType);
            int32Contract.ObjectKind.ShouldBe(VerifiableObjectKind.BasicType);
            int64Contract.ObjectKind.ShouldBe(VerifiableObjectKind.BasicType);
            single32Contract.ObjectKind.ShouldBe(VerifiableObjectKind.BasicType);
            single64Contract.ObjectKind.ShouldBe(VerifiableObjectKind.BasicType);

            int16Contract.IsBasicType.ShouldBeTrue();
            int32Contract.IsBasicType.ShouldBeTrue();
            int64Contract.IsBasicType.ShouldBeTrue();
            single32Contract.IsBasicType.ShouldBeTrue();
            single64Contract.IsBasicType.ShouldBeTrue();

            int16Contract.GetMemberContracts().Should().HaveCount(1);
            int32Contract.GetMemberContracts().Should().HaveCount(1);
            int64Contract.GetMemberContracts().Should().HaveCount(1);
            single32Contract.GetMemberContracts().Should().HaveCount(1);
            single64Contract.GetMemberContracts().Should().HaveCount(1);

            int16Contract.GetMemberContract(VerifiableMemberContract.BASIC_TYPE).MemberType.ShouldBe(typeof(Int16));
            int32Contract.GetMemberContract(VerifiableMemberContract.BASIC_TYPE).MemberType.ShouldBe(typeof(Int32));
            int64Contract.GetMemberContract(VerifiableMemberContract.BASIC_TYPE).MemberType.ShouldBe(typeof(Int64));
            single32Contract.GetMemberContract(VerifiableMemberContract.BASIC_TYPE).MemberType.ShouldBe(typeof(float));
            single64Contract.GetMemberContract(VerifiableMemberContract.BASIC_TYPE).MemberType.ShouldBe(typeof(double));

            int16Contract.GetMemberContract(0).MemberType.ShouldBe(typeof(Int16));
            int32Contract.GetMemberContract(0).MemberType.ShouldBe(typeof(Int32));
            int64Contract.GetMemberContract(0).MemberType.ShouldBe(typeof(Int64));
            single32Contract.GetMemberContract(0).MemberType.ShouldBe(typeof(float));
            single64Contract.GetMemberContract(0).MemberType.ShouldBe(typeof(double));
        }

        [Fact(DisplayName = "To test create an ObjectContract by enum type.")]
        public void EnumTypeToObjectContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceEnum>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceEnum));
            contract.IncludeAnnotations.ShouldBeFalse();
            contract.ObjectKind.ShouldBe(VerifiableObjectKind.BasicType);
            contract.IsBasicType.ShouldBeTrue();

            contract.GetMemberContracts().Count().ShouldBe(1);
            contract.GetMemberContract(VerifiableMemberContract.BASIC_TYPE).MemberType.ShouldBe(typeof(NiceEnum));
            contract.GetMemberContract(0).MemberType.ShouldBe(typeof(NiceEnum));
        }

        [Fact(DisplayName = "To test create an ObjectContract by struct type.")]
        public void StructureTypeToObjectContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceStruct>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceStruct));
            contract.IncludeAnnotations.ShouldBeFalse();
            contract.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            contract.IsBasicType.ShouldBeFalse();

            contract.GetMemberContracts().Count().ShouldBe(2);
        }

#if !NETFRAMEWORK && !NETCOREAPP3_1

        [Fact(DisplayName = "To test create an ObjectContract by record type.")]
        public void RecordTypeToObjectContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceRecord>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceRecord));
            contract.IncludeAnnotations.ShouldBeFalse();
            contract.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            contract.IsBasicType.ShouldBeFalse();

            contract.GetMemberContracts().Count().ShouldBe(2);
        }

#endif

        [Fact(DisplayName = "To test create an ObjectContext by 'With' from basic type.")]
        public void BasicTypeWithValueToObjectContextTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<int>();
            var value = 1;
            var context1 = contract.WithInstance(value);
            var context2 = contract.WithInstance(value, "Int32");

            context1.ShouldNotBeNull();
            context2.ShouldNotBeNull();
            
            context1.InstanceName.ShouldBe(VerifiableMemberContract.BASIC_TYPE);
            context2.InstanceName.ShouldBe("Int32");
            
            context1.GetValues().Count().ShouldBe(1);
            context2.GetValues().Count().ShouldBe(1);
            
            context1.GetValue(0).MemberName.ShouldBe(VerifiableMemberContract.BASIC_TYPE);
            context2.GetValue(0).MemberName.ShouldBe(VerifiableMemberContract.BASIC_TYPE);
            
            context1.GetValue(VerifiableMemberContract.BASIC_TYPE).Value.ShouldBe(1);
            context2.GetValue(VerifiableMemberContract.BASIC_TYPE).Value.ShouldBe(1);
        }

        [Fact(DisplayName = "To test create an ObjectContext by 'With' from enum type.")]
        public void EnumTypeWithValueToObjectContextTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceEnum>();
            var @enum = NiceEnum.Black;
            var context1 = contract.WithInstance(@enum);
            var context2 = contract.WithInstance(@enum, "NiceEnum");
            
            context1.InstanceName.ShouldBe(VerifiableMemberContract.BASIC_TYPE);
            context2.InstanceName.ShouldBe("NiceEnum");
            
            context1.GetValues().Count().ShouldBe(1);
            context2.GetValues().Count().ShouldBe(1);
            
            context1.GetValue(0).MemberName.ShouldBe(VerifiableMemberContract.BASIC_TYPE);
            context2.GetValue(0).MemberName.ShouldBe(VerifiableMemberContract.BASIC_TYPE);
            
            context1.GetValue(VerifiableMemberContract.BASIC_TYPE).Value.ShouldBe(NiceEnum.Black);
            context2.GetValue(VerifiableMemberContract.BASIC_TYPE).Value.ShouldBe(NiceEnum.Black);
        }

        [Fact(DisplayName = "To test create an ObjectContext by 'With' from struct type.")]
        public void StructureTypeWithValueToObjectContextTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceStruct>();
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
            var contract = VerifiableObjectContractManager.Resolve<NiceRecord>();
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