using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Registrars;
using CosmosValidationUT.Fakes;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.BuildingUT
{
    [Trait("BuildingUT", "RegistrarGetting")]
    public class RegistrarGettingTests
    {
        [Fact(DisplayName = "Default registrar getting test")]
        public void DefaultRegistrarGettingTest()
        {
            var registrar = ValidationRegistrar.DefaultRegistrar;
            var instance = registrar as InternalValidationRegistrar;

            registrar.ShouldNotBeNull();
            instance.ShouldNotBeNull();

            instance.Name.ShouldBe(ValidationProvider.DefaultName);
        }

        [Fact(DisplayName = "Main registrar accessing test")]
        public void MainRegistrarAccessingTest()
        {
            var provider = new ValidationProvider(new FakeProjectManager(), new DefaultVerifiableObjectResolver(), new ValidationOptions());

            var registrar1 = ValidationRegistrar.ForProvider(provider);
            var registrar2 = ValidationRegistrar.Continue();

            registrar1.ShouldNotBeNull();
            registrar2.ShouldNotBeNull();

            var instance1 = registrar1 as InternalValidationRegistrar;
            var instance2 = registrar2 as InternalValidationRegistrar;

            instance1.ShouldNotBeNull();
            instance2.ShouldNotBeNull();

            instance1.Name.ShouldBe(instance2.Name);
        }

        [Fact(DisplayName = "Named registrar accessing test")]
        public void NamedRegistrarAccessingTest()
        {
            var provider = new ValidationProvider(new FakeProjectManager(), new DefaultVerifiableObjectResolver(), new ValidationOptions());

            var registrar1 = ValidationRegistrar.ForProvider(provider, "UT_NamedRegistrarAccessingTest");
            var registrar2 = ValidationRegistrar.Continue("UT_NamedRegistrarAccessingTest");

            registrar1.ShouldNotBeNull();
            registrar2.ShouldNotBeNull();

            var instance1 = registrar1 as InternalValidationRegistrar;
            var instance2 = registrar2 as InternalValidationRegistrar;

            instance1.ShouldNotBeNull();
            instance2.ShouldNotBeNull();

            instance1.Name.ShouldBe("UT_NamedRegistrarAccessingTest");
            instance2.Name.ShouldBe("UT_NamedRegistrarAccessingTest");
        }

        [Fact(DisplayName = "To get registrar (by Continue), if not exists then return default().")]
        public void RegistrarContinueOrDefaultTest()
        {
            var registrar1 = ValidationRegistrar.ContinueOrDefault("UT_RegistrarContinueOrDefaultTest1");
            var registrarRef = ValidationRegistrar.Continue(); //Default/Main registrar

            registrar1.ShouldNotBeNull();
            registrarRef.ShouldNotBeNull();

            var instance1 = registrar1 as InternalValidationRegistrar;
            var instanceRef = registrarRef as InternalValidationRegistrar;

            instance1.ShouldNotBeNull();
            instanceRef.ShouldNotBeNull();
            
            instance1.Name.ShouldBe(instanceRef.Name);

            var provider = new ValidationProvider(new FakeProjectManager(), new DefaultVerifiableObjectResolver(), new ValidationOptions());

            var registrar2 = ValidationRegistrar.ForProvider(provider, "UT_RegistrarContinueOrDefaultTest2");
            var registrar3 = ValidationRegistrar.ContinueOrDefault("UT_RegistrarContinueOrDefaultTest2");

            registrar2.ShouldNotBeNull();
            registrar3.ShouldNotBeNull();

            var instance2 = registrar2 as InternalValidationRegistrar;
            var instance3 = registrar3 as InternalValidationRegistrar;

            instance2.ShouldNotBeNull();
            instance3.ShouldNotBeNull();

            instance2.Name.ShouldBe("UT_RegistrarContinueOrDefaultTest2");
            instance3.Name.ShouldBe("UT_RegistrarContinueOrDefaultTest2");
        }
    }
}