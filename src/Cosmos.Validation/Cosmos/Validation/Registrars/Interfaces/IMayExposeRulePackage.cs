using System;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayExposeRulePackage
    {
        VerifyRulePackage ExposeRulePackage<T>(string projectName = "");

        VerifyRulePackage ExposeRulePackage(Type declaringType, string projectName = "");
    }

    public interface IMayExposeUnregisteredRulePackage
    {
        VerifyRulePackage ExposeUnregisteredRulePackage<T>(string projectName = "");

        VerifyRulePackage ExposeUnregisteredRulePackage(Type declaringType, string projectName = "");
    }

    public interface IMayExposeRulePackageForType
    {
        VerifyRulePackage ExposeRulePackage();
    }

    public interface IMayExposeUnregisteredRulePackageForType
    {
        VerifyRulePackage ExposeUnregisteredRulePackage();
    }
}