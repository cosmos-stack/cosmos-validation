using System.Collections.Generic;
using System.Linq;
using CosmosStack.Date;
using CosmosStack.Reflection;
using CosmosStack.Validation.Annotations;
using CosmosStack.Validation.Objects;
using CosmosValidationUT.Models;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.ObjectUT
{
    [Trait("ObjectUT", "VerifiableMemberContract")]
    public class VerifiableMemberContractTests
    {
        [Fact(DisplayName = "Create VerifiableObjectContract and touch VerifiableMemberContract by direct type")]
        public void DirectTypeCreateObjectValueContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceBoat));
            contract.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            contract.IsBasicType.ShouldBeFalse();

            //value-contract
            contract.GetMemberContracts().Count().ShouldBe(5);

            var value1 = contract.GetMemberContract("Name");
            var value2 = contract.GetMemberContract("Length");
            var value3 = contract.GetMemberContract("Width");
            var value4 = contract.GetMemberContract("CreateTime");
            var value5 = contract.GetMemberContract("Email");
            
            value1.MemberName.ShouldBe("Name");
            value1.MemberType.ShouldBe(TypeClass.StringClazz);
            value1.DeclaringType.ShouldBe(typeof(NiceBoat));
            value1.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            
            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            
            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            
            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.MemberKind.ShouldBe(VerifiableMemberKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            
            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
        }

        [Fact(DisplayName = "Create VerifiableObjectContract and touch VerifiableMemberContract by generic type")]
        public void GenericTypeCreateObjectValueContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceBoat>();

            contract.ShouldNotBeNull();
            contract.Type.ShouldBe(typeof(NiceBoat));
            contract.ObjectKind.ShouldBe(VerifiableObjectKind.StructureType);
            contract.IsBasicType.ShouldBeFalse();

            //value-contract
            contract.GetMemberContracts().Count().ShouldBe(5);

            var value1 = contract.GetMemberContract("Name");
            var value2 = contract.GetMemberContract("Length");
            var value3 = contract.GetMemberContract("Width");
            var value4 = contract.GetMemberContract("CreateTime");
            var value5 = contract.GetMemberContract("Email");
            
            value1.MemberName.ShouldBe("Name");
            value1.MemberType.ShouldBe(TypeClass.StringClazz);
            value1.DeclaringType.ShouldBe(typeof(NiceBoat));
            value1.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value1.IsBasicType.ShouldBeTrue();
            value1.IncludeAnnotations.ShouldBeTrue();
            
            value2.MemberName.ShouldBe("Length");
            value2.MemberType.ShouldBe(TypeClass.LongClazz);
            value2.DeclaringType.ShouldBe(typeof(NiceBoat));
            value2.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value2.IsBasicType.ShouldBeTrue();
            value2.IncludeAnnotations.ShouldBeTrue();
            
            value3.MemberName.ShouldBe("Width");
            value3.MemberType.ShouldBe(TypeClass.LongClazz);
            value3.DeclaringType.ShouldBe(typeof(NiceBoat));
            value3.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value3.IsBasicType.ShouldBeTrue();
            value3.IncludeAnnotations.ShouldBeTrue();
            
            value4.MemberName.ShouldBe("CreateTime");
            value4.MemberType.ShouldBe(TypeClass.DateTimeClazz);
            value4.DeclaringType.ShouldBe(typeof(NiceBoat));
            value4.MemberKind.ShouldBe(VerifiableMemberKind.Field);
            value4.IsBasicType.ShouldBeTrue();
            value4.IncludeAnnotations.ShouldBeTrue();
            
            value5.MemberName.ShouldBe("Email");
            value5.MemberType.ShouldBe(TypeClass.StringClazz);
            value5.DeclaringType.ShouldBe(typeof(NiceBoat));
            value5.MemberKind.ShouldBe(VerifiableMemberKind.Property);
            value5.IsBasicType.ShouldBeTrue();
            value5.IncludeAnnotations.ShouldBeTrue();
        }

        [Fact(DisplayName="To test annotations of Property Name in VerifiableMemberContract")]
        public void Annotation_Name_InObjectValueContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceBoat>();

            contract.IncludeAnnotations.ShouldBeTrue();
            
            var value1 = contract.GetMemberContract("Name");

            value1.IncludeAnnotations.ShouldBeTrue();
            value1.Attributes.Count.ShouldBe(1);

            var a101 = value1.GetFlagAnnotations().ToList();
            a101.Count.ShouldBe(1);
            a101[0].GetType().ShouldBe(typeof(NotWhiteSpaceAttribute));

            var a102 = value1.GetParameterAnnotations().ToList();
            a102.Count.ShouldBe(1);
            a102[0].GetType().ShouldBe(typeof(NotWhiteSpaceAttribute));
            a102[0].Name.ShouldBe("Not-WhiteSpace Annotation");

            var a103 = value1.GetVerifiableAnnotations().ToList();
            a103.Count.ShouldBe(0);

            var a104 = value1.GetQuietVerifiableAnnotations().ToList();
            a104.Count.ShouldBe(0);

            var a105 = value1.GetStrongVerifiableAnnotations().ToList();
            a105.Count.ShouldBe(0);
            
            var a106 = value1.GetObjectContextVerifiableAnnotations().ToList();
            a106.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "To test annotations of Property Length in VerifiableMemberContract")]
        public void Annotation_Length_InObjectValueContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceBoat>();

            contract.IncludeAnnotations.ShouldBeTrue();
            
            var value2 = contract.GetMemberContract("Length");

            value2.IncludeAnnotations.ShouldBeTrue();
            value2.Attributes.Count.ShouldBe(1);

            var a201 = value2.GetFlagAnnotations().ToList();
            a201.Count.ShouldBe(1);
            a201[0].GetType().ShouldBe(typeof(NotNegativeAttribute));

            var a202 = value2.GetParameterAnnotations().ToList();
            a202.Count.ShouldBe(1);
            a202[0].GetType().ShouldBe(typeof(NotNegativeAttribute));
            a202[0].Name.ShouldBe("Not-Negative Annotation");

            var a203 = value2.GetVerifiableAnnotations().ToList();
            a203.Count.ShouldBe(0);

            var a204 = value2.GetQuietVerifiableAnnotations().ToList();
            a204.Count.ShouldBe(0);

            var a205 = value2.GetStrongVerifiableAnnotations().ToList();
            a205.Count.ShouldBe(0);
            
            var a206 = value2.GetObjectContextVerifiableAnnotations().ToList();
            a206.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "To test annotations of Property Weight in VerifiableMemberContract")]
        public void Annotation_Weight_InObjectValueContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceBoat>();

            contract.IncludeAnnotations.ShouldBeTrue();
            
            var value3 = contract.GetMemberContract("Width");
            
            value3.IncludeAnnotations.ShouldBeTrue();
            value3.Attributes.Count.ShouldBe(1);

            var a301 = value3.GetFlagAnnotations().ToList();
            a301.Count.ShouldBe(1);
            a301[0].GetType().ShouldBe(typeof(NotNegativeAttribute));

            var a302 = value3.GetParameterAnnotations().ToList();
            a302.Count.ShouldBe(1);
            a302[0].GetType().ShouldBe(typeof(NotNegativeAttribute));
            a302[0].Name.ShouldBe("Not-Negative Annotation");

            var a303 = value3.GetVerifiableAnnotations().ToList();
            a303.Count.ShouldBe(0);

            var a304 = value3.GetQuietVerifiableAnnotations().ToList();
            a304.Count.ShouldBe(0);

            var a305 = value3.GetStrongVerifiableAnnotations().ToList();
            a305.Count.ShouldBe(0);
            
            var a306 = value3.GetObjectContextVerifiableAnnotations().ToList();
            a306.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "To test annotations of Field CreatedTime in VerifiableMemberContract")]
        public void Annotation_CreatedTime_InObjectValueContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceBoat>();

            contract.IncludeAnnotations.ShouldBeTrue();
            
            var value4 = contract.GetMemberContract("CreateTime");
            
            value4.IncludeAnnotations.ShouldBeTrue();
            value4.Attributes.Count.ShouldBe(1);

            var a401 = value4.GetFlagAnnotations().ToList();
            a401.Count.ShouldBe(1);
            a401[0].GetType().ShouldBe(typeof(ValidDateValueAttribute));

            var a402 = value4.GetParameterAnnotations().ToList();
            a402.Count.ShouldBe(1);
            a402[0].GetType().ShouldBe(typeof(ValidDateValueAttribute));
            a402[0].Name.ShouldBe("Valid-Date-Value Annotation");

            var a403 = value4.GetVerifiableAnnotations().ToList();
            a403.Count.ShouldBe(1);
            a403[0].GetType().ShouldBe(typeof(ValidDateValueAttribute));
            a403[0].Name.ShouldBe("Valid-Date-Value Annotation");

            var a404 = value4.GetQuietVerifiableAnnotations().ToList();
            a404.Count.ShouldBe(1);
            a404[0].GetType().ShouldBe(typeof(ValidDateValueAttribute));
            a404[0].Name.ShouldBe("Valid-Date-Value Annotation");

            var a405 = value4.GetStrongVerifiableAnnotations().ToList();
            a405.Count.ShouldBe(0);
            
            var a406 = value4.GetObjectContextVerifiableAnnotations().ToList();
            a406.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "To test annotations of Property Email in VerifiableMemberContract")]
        public void Annotation_Email_InObjectValueContractTest()
        {
            var contract = VerifiableObjectContractManager.Resolve<NiceBoat>();

            contract.IncludeAnnotations.ShouldBeTrue();
            
            var value5 = contract.GetMemberContract("Email");
            
            value5.IncludeAnnotations.ShouldBeTrue();
            value5.Attributes.Count.ShouldBe(1);

            var a501 = value5.GetFlagAnnotations().ToList();
            a501.Count.ShouldBe(1);
            a501[0].GetType().ShouldBe(typeof(ValidEmailValueAttribute));

            var a502 = value5.GetParameterAnnotations().ToList();
            a502.Count.ShouldBe(1);
            a502[0].GetType().ShouldBe(typeof(ValidEmailValueAttribute));
            a502[0].Name.ShouldBe("ValidEmailValueAnnotation");

            var a503 = value5.GetVerifiableAnnotations().ToList();
            a503.Count.ShouldBe(1);
            a503[0].GetType().ShouldBe(typeof(ValidEmailValueAttribute));
            a503[0].Name.ShouldBe("ValidEmailValueAnnotation");

            var a504 = value5.GetQuietVerifiableAnnotations().ToList();
            a504.Count.ShouldBe(1);
            a504[0].GetType().ShouldBe(typeof(ValidEmailValueAttribute));
            a504[0].Name.ShouldBe("ValidEmailValueAnnotation");

            var a505 = value5.GetStrongVerifiableAnnotations().ToList();
            a505.Count.ShouldBe(1);
            a505[0].GetType().ShouldBe(typeof(ValidEmailValueAttribute));
            a505[0].Name.ShouldBe("ValidEmailValueAnnotation");
            
            var a506 = value5.GetObjectContextVerifiableAnnotations().ToList();
            a506.Count.ShouldBe(1);
            a506[0].GetType().ShouldBe(typeof(ValidEmailValueAttribute));
            a506[0].Name.ShouldBe("ValidEmailValueAnnotation");
        }

        [Fact(DisplayName = "To test getting value from Instance by VerifiableMemberContract")]
        public void GetValueFromInstanceTest()
        {
            var instance = new NiceBoat
            {
                Name = "NiceBoat1000",
                Length = 1000,
                Width = 30,
                CreateTime = DateTimeFactory.Create(2020, 12, 21),
                Email = "nice@boat.com"
            };
            
            var contract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));

            var value1 = contract.GetMemberContract("Name");
            var value2 = contract.GetMemberContract("Length");
            var value3 = contract.GetMemberContract("Width");
            var value4 = contract.GetMemberContract("CreateTime");
            var value5 = contract.GetMemberContract("Email");
            
            value1.GetValue(instance).ShouldBe("NiceBoat1000");
            value2.GetValue(instance).ShouldBe(1000);
            value3.GetValue(instance).ShouldBe(30);
            value4.GetValue(instance).ShouldBe(DateTimeFactory.Create(2020, 12, 21));
            value5.GetValue(instance).ShouldBe("nice@boat.com");
        }

        [Fact(DisplayName = "To test getting value from Dictionary by VerifiableMemberContract")]
        public void GetValueFromDictionaryTest()
        {
            var d = new Dictionary<string, object>
            {
                ["Name"] = "NiceBoat1000",
                ["Length"] = 1000,
                ["Width"] = 30,
                ["CreateTime"] = DateTimeFactory.Create(2020, 12, 21),
                ["Email"] = "nice@boat.com"
            };
            
            var contract = VerifiableObjectContractManager.Resolve(typeof(NiceBoat));

            var value1 = contract.GetMemberContract("Name");
            var value2 = contract.GetMemberContract("Length");
            var value3 = contract.GetMemberContract("Width");
            var value4 = contract.GetMemberContract("CreateTime");
            var value5 = contract.GetMemberContract("Email");
            
            value1.GetValue(d).ShouldBe("NiceBoat1000");
            value2.GetValue(d).ShouldBe(1000);
            value3.GetValue(d).ShouldBe(30);
            value4.GetValue(d).ShouldBe(DateTimeFactory.Create(2020, 12, 21));
            value5.GetValue(d).ShouldBe("nice@boat.com");
        }
    }
}