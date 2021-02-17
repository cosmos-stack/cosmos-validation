using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Projects
{
    public interface IProject
    {
        string Name { get; }

        Type Type { get; }

        ProjectClass Class { get; }

        VerifyResult Verify(ObjectContext context);
        
        VerifyResult VerifyOne(ObjectValueContext context);
    }
}