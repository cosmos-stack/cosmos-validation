using System;
using System.Collections.Generic;
using System.Reflection;
using Cosmos.Validation;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using CosmosValidationUT.Fakes;
using Shouldly;
using Xunit;

namespace CosmosValidationUT.SolutionUT
{
    [Trait("Solution", "ProjectManager")]
    public class ProjectManTests
    {
        [Fact(DisplayName = "Project register test")]
        public void ProjectRegisterTest()
        {
            var man = new FakeProjectManager();

            var project1 = new MyFakeTypeProject(typeof(string));
            var project2 = new MyFakeNameProject(typeof(string), "string");

            man.Register(project1);
            man.Register(project2);

            man._typedProjects.Keys.Count.ShouldBe(1);
            man._namedTypeProjects.Keys.Count.ShouldBe(1);
        }

        [Fact(DisplayName = "Project resolve test")]
        public void ProjectResolveTest()
        {
            var man = new FakeProjectManager();

            var project1 = new MyFakeTypeProject(typeof(string));
            var project2 = new MyFakeNameProject(typeof(string), "string");

            man.Register(project1);
            man.Register(project2);

            var project3 = man.Resolve(typeof(string));
            var project4 = man.Resolve(typeof(string), "string");

            project3.ShouldNotBeNull();
            project4.ShouldNotBeNull();

            project3.Class.ShouldBe(ProjectClass.Typed);
            project4.Class.ShouldBe(ProjectClass.Named);

            project3.Type.ShouldBe(typeof(string));
            project4.Type.ShouldBe(typeof(string));

            project3.Name.ShouldBe("System.String");
            project4.Name.ShouldBe("string");
        }

        [Fact(DisplayName = "Project resolve with unregister type test")]
        public void ProjectResolveWithUnregisterTypeTest()
        {
            var man = new FakeProjectManager();

            var project1 = new MyFakeTypeProject(typeof(string));
            var project2 = new MyFakeNameProject(typeof(string), "string");

            man.Register(project1);
            man.Register(project2);

            var project3 = man.Resolve(typeof(int));
            var project4 = man.Resolve(typeof(int), "int");

            project3.ShouldBeNull();
            project4.ShouldBeNull();
        }

        [Fact(DisplayName = "Project try resolve test")]
        public void ProjectTryResolveTest()
        {
            var man = new FakeProjectManager();

            var project1 = new MyFakeTypeProject(typeof(string));
            var project2 = new MyFakeNameProject(typeof(string), "string");

            man.Register(project1);
            man.Register(project2);

            var result3 = man.TryResolve(typeof(string), out var project3);
            var result4 = man.TryResolve(typeof(string), "string", out var project4);

            result3.ShouldBeTrue();
            result4.ShouldBeTrue();

            project3.ShouldNotBeNull();
            project4.ShouldNotBeNull();

            project3.Class.ShouldBe(ProjectClass.Typed);
            project4.Class.ShouldBe(ProjectClass.Named);

            project3.Type.ShouldBe(typeof(string));
            project4.Type.ShouldBe(typeof(string));

            project3.Name.ShouldBe("System.String");
            project4.Name.ShouldBe("string");
        }

        [Fact(DisplayName = "Project try resolve with unregister type test")]
        public void ProjectTryResolveWithUnregisterTypeTest()
        {
            var man = new FakeProjectManager();

            var project1 = new MyFakeTypeProject(typeof(string));
            var project2 = new MyFakeNameProject(typeof(string), "string");

            man.Register(project1);
            man.Register(project2);
            
            var result3 = man.TryResolve(typeof(int), out var project3);
            var result4 = man.TryResolve(typeof(int), "int", out var project4);

            result3.ShouldBeFalse();
            result4.ShouldBeFalse();
            
            project3.ShouldBeNull();
            project4.ShouldBeNull();            
        }

        private class MyFakeTypeProject : IProject
        {
            public MyFakeTypeProject(Type sourceType)
            {
                Type = sourceType;
                Name = sourceType.GetFullyQualifiedName();
                Class = ProjectClass.Typed;
            }

            public string Name { get; }
            public Type Type { get; }
            public ProjectClass Class { get; }

            public VerifyResult Verify(ObjectContext context)
            {
                return VerifyResult.Success;
            }

            public VerifyResult VerifyOne(ObjectValueContext context)
            {
                return VerifyResult.Success;
            }

            public VerifyResult VerifyMany(IDictionary<string, ObjectValueContext> keyValueCollections)
            {
                return VerifyResult.Success;
            }
        }

        private class MyFakeNameProject : IProject
        {
            public MyFakeNameProject(Type sourceType, string name)
            {
                Type = sourceType;
                Name = name;
                Class = ProjectClass.Named;
            }

            public string Name { get; }
            public Type Type { get; }
            public ProjectClass Class { get; }

            public VerifyResult Verify(ObjectContext context)
            {
                return VerifyResult.Success;
            }

            public VerifyResult VerifyOne(ObjectValueContext context)
            {
                return VerifyResult.Success;
            }

            public VerifyResult VerifyMany(IDictionary<string, ObjectValueContext> keyValueCollections)
            {
                return VerifyResult.Success;
            }
        }
    }
}