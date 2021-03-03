using System.Linq;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ObjectUT
{
    [Trait("ObjectUT", "ObjectContract")]
    public class ObjectContractTests
    {
        [Fact(DisplayName = "Create ObjectContract by direct type")]
        public void DirectTypeCreateObjectContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceBoat));
            contract.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            contract.IsBasicType.ShouldBeFalse();

            //annotations/attributes - class level
            contract.Attributes.Count.ShouldBe(0);
            contract.IncludeAnnotations.ShouldBeTrue();

            //value-contract
            contract.GetMemberContracts().Count().ShouldBe(5);

            contract.GetMemberContract("Name").MemberName.ShouldBe("Name");
            contract.GetMemberContract("Length").MemberName.ShouldBe("Length");
            contract.GetMemberContract("Width").MemberName.ShouldBe("Width");
            contract.GetMemberContract("CreateTime").MemberName.ShouldBe("CreateTime");
            contract.GetMemberContract("Email").MemberName.ShouldBe("Email");

            contract.GetMemberContract(0).MemberName.ShouldBe("Name");
            contract.GetMemberContract(1).MemberName.ShouldBe("Length");
            contract.GetMemberContract(2).MemberName.ShouldBe("Width");
            contract.GetMemberContract(3).MemberName.ShouldBe("Email"); //Property first
            contract.GetMemberContract(4).MemberName.ShouldBe("CreateTime");
        }

        [Fact(DisplayName = "Create ObjectContract by generic type")]
        public void GenericTypeCreateObjectContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceBoat>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceBoat));
            contract.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            contract.IsBasicType.ShouldBeFalse();

            //annotations/attributes - class level
            contract.Attributes.Count.ShouldBe(0);
            contract.IncludeAnnotations.ShouldBeTrue();

            //value-contract
            contract.GetMemberContracts().Count().ShouldBe(5);

            contract.GetMemberContract("Name").MemberName.ShouldBe("Name");
            contract.GetMemberContract("Length").MemberName.ShouldBe("Length");
            contract.GetMemberContract("Width").MemberName.ShouldBe("Width");
            contract.GetMemberContract("CreateTime").MemberName.ShouldBe("CreateTime");
            contract.GetMemberContract("Email").MemberName.ShouldBe("Email");

            contract.GetMemberContract(0).MemberName.ShouldBe("Name");
            contract.GetMemberContract(1).MemberName.ShouldBe("Length");
            contract.GetMemberContract(2).MemberName.ShouldBe("Width");
            contract.GetMemberContract(3).MemberName.ShouldBe("Email"); //Property first
            contract.GetMemberContract(4).MemberName.ShouldBe("CreateTime");
        }
    }
}