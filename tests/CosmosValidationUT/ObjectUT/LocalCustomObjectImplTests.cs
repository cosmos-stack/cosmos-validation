using CosmosStack.Validation.Objects;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ObjectUT
{
    [Trait("ObjectUT", "LocalCustomObjectImpl")]
    public class LocalCustomObjectImplTests
    {
        [Fact(DisplayName = "To test GetValue by 3th-part Impl of VerifiableObjectContract")]
        public void ImplForVerifiableObjectContractTest()
        {
            var objImpl = new LocalObjectContractImpl();
            var objContract = new VerifiableObjectContract(objImpl);

            objContract.GetValue((object) null, "Name").ShouldBe("Nice");
            objContract.GetValue((object) null, "Length").ShouldBe(10);
            objContract.GetValue((object) null, "Width").ShouldBe(100);
        }
    }
}