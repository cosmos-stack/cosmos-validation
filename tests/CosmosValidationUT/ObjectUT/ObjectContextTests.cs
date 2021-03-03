using System.Collections.Generic;
using System.Linq;
using Cosmos.Date;
using Cosmos.Validation.Objects;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ObjectUT
{
    [Trait("ObjectUT", "ObjectContext")]
    public class ObjectContextTests
    {
        public ObjectContextTests()
        {
            _objectResolver = new DefaultVerifiableObjectResolver();
        }

        private readonly IVerifiableObjectResolver _objectResolver;

        [Fact(DisplayName = "To test create an object context with a direct type instance")]
        public void InstanceWithDirectTypeCreateObjectContextTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var context = _objectResolver.Resolve(typeof(NiceBoat), instance);

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBe(instance);
            context.KeyValueCollection.ShouldBeNull();
            context.InstanceName.ShouldBe("Instance");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with a generic type instance")]
        public void InstanceWithGenericTypeCreateObjectContextTest()
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

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBe(instance);
            context.KeyValueCollection.ShouldBeNull();
            context.InstanceName.ShouldBe("Instance");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with a direct type instance and name")]
        public void InstanceWithDirectTypeAndNameCreateObjectContextTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var context = _objectResolver.Resolve(typeof(NiceBoat), instance, "nice_boat_2000");

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBe(instance);
            context.KeyValueCollection.ShouldBeNull();
            context.InstanceName.ShouldBe("nice_boat_2000");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with a generic type instance and name")]
        public void InstanceWithGenericTypeAndNameCreateObjectContextTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var context = _objectResolver.Resolve<NiceBoat>(instance, "nice_boat_2000");

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBe(instance);
            context.KeyValueCollection.ShouldBeNull();
            context.InstanceName.ShouldBe("nice_boat_2000");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with a direct type dictionary")]
        public void DictionaryWithDirectTypeCreateObjectContextTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var context = _objectResolver.Resolve(typeof(NiceBoat), d);

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBeNull();
            context.KeyValueCollection.ShouldBe(d);
            context.InstanceName.ShouldBe("KeyValueCollection");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with a generic type dictionary")]
        public void DictionaryWithGenericTypeCreateObjectContextTest()
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

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBeNull();
            context.KeyValueCollection.ShouldBe(d);
            context.InstanceName.ShouldBe("KeyValueCollection");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with a direct type dictionary and name")]
        public void DictionaryWithDirectTypeAndNameCreateObjectContextTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var context = _objectResolver.Resolve(typeof(NiceBoat), d, "nice_boat_2000");

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBeNull();
            context.KeyValueCollection.ShouldBe(d);
            context.InstanceName.ShouldBe("nice_boat_2000");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with a generic type dictionary and name")]
        public void DictionaryWithGenericTypeAndNameCreateObjectContextTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var context = _objectResolver.Resolve<NiceBoat>(d, "nice_boat_2000");

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBeNull();
            context.KeyValueCollection.ShouldBe(d);
            context.InstanceName.ShouldBe("nice_boat_2000");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with contract and instance")]
        public void InstanceAfterCreatedObjectContextTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var context = VerifiableObjectContractManager.Resolve<NiceBoat>().WithInstance(instance);

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBe(instance);
            context.KeyValueCollection.ShouldBeNull();
            context.InstanceName.ShouldBe("Instance");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with contract and dictionary")]
        public void DictionaryAfterCreatedObjectContextTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var context = VerifiableObjectContractManager.Resolve<NiceBoat>().WithDictionary(d);

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBeNull();
            context.KeyValueCollection.ShouldBe(d);
            context.InstanceName.ShouldBe("KeyValueCollection");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with contract, instance and name")]
        public void InstanceWithNameAfterCreatedObjectContextTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };

            var context = VerifiableObjectContractManager.Resolve<NiceBoat>().WithInstance(instance, "nice_boat_2000");

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBe(instance);
            context.KeyValueCollection.ShouldBeNull();
            context.InstanceName.ShouldBe("nice_boat_2000");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }

        [Fact(DisplayName = "To test create an object context with contract, dictionary and name")]
        public void DictionaryWithNameAfterCreatedObjectContextTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };

            var context = VerifiableObjectContractManager.Resolve<NiceBoat>().WithDictionary(d, "nice_boat_2000");

            context.ShouldNotBeNull();
            context.Type.ShouldBe(typeof(NiceBoat));
            context.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            context.IsBasicType().ShouldBeFalse();
            context.Instance.ShouldBeNull();
            context.KeyValueCollection.ShouldBe(d);
            context.InstanceName.ShouldBe("nice_boat_2000");
            
            //annotations/attributes - class level
            context.IncludeAnnotations.ShouldBeTrue();
            context.Attributes.Count.ShouldBe(0);
            
            //member/value-contract
            context.GetMembers().Count().ShouldBe(5);
            
            context.GetMember("Name").MemberName.ShouldBe("Name");
            context.GetMember("Length").MemberName.ShouldBe("Length");
            context.GetMember("Width").MemberName.ShouldBe("Width");
            context.GetMember("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetMember("Email").MemberName.ShouldBe("Email");

            context.GetMember(0).MemberName.ShouldBe("Name");
            context.GetMember(1).MemberName.ShouldBe("Length");
            context.GetMember(2).MemberName.ShouldBe("Width");
            context.GetMember(3).MemberName.ShouldBe("Email"); //Property first
            context.GetMember(4).MemberName.ShouldBe("CreateTime");
            
            //value/value-context
            context.GetValues().Count().ShouldBe(5);
            
            context.GetValue("Name").MemberName.ShouldBe("Name");
            context.GetValue("Length").MemberName.ShouldBe("Length");
            context.GetValue("Width").MemberName.ShouldBe("Width");
            context.GetValue("CreateTime").MemberName.ShouldBe("CreateTime");
            context.GetValue("Email").MemberName.ShouldBe("Email");

            context.GetValue(0).MemberName.ShouldBe("Name");
            context.GetValue(1).MemberName.ShouldBe("Length");
            context.GetValue(2).MemberName.ShouldBe("Width");
            context.GetValue(3).MemberName.ShouldBe("Email"); //Property first
            context.GetValue(4).MemberName.ShouldBe("CreateTime");
            
            context.GetMemberMap().Count.ShouldBe(5);
            context.GetValueMap().Count.ShouldBe(5);
        }
    }
}