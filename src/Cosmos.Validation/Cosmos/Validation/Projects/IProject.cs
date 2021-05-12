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

        VerifyResult Verify(VerifiableObjectContext context);

        VerifyResult VerifyOne(VerifiableMemberContext context);
        
        VerifyResult VerifyMany(IDictionary<string, VerifiableMemberContext> keyValueCollections);

        VerifyRulePackage ExposeRules();

        VerifyMemberRulePackage ExposeMemberRules(string memberName);
    }
}