using System;
using System.Collections.Generic;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Projects
{
    public interface IProject
    {
        string Name { get; }

        Type Type { get; }

        ProjectClass Class { get; }

        VerifyResult Verify(ObjectContext context, ValidationOptions options);

        VerifyResult VerifyOne(ObjectValueContext context, ValidationOptions options);
        
        VerifyResult VerifyMany(IDictionary<string, ObjectValueContext> keyValueCollections, ValidationOptions options);
    }
}