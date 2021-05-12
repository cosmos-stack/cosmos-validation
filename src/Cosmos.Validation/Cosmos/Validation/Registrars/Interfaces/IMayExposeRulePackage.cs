using System;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayExposeRulePackage
    {
        VerifyRulePackage ExposeVerifyRulePackage<T>(string projectName = "");

        VerifyRulePackage ExposeVerifyRulePackage(Type declaringType, string projectName = "");
    }

    public interface IMayExposeUnregisteredRulePackage
    {
        VerifyRulePackage ExposeUnregisteredVerifyRulePackage<T>(string projectName = "");

        VerifyRulePackage ExposeUnregisteredVerifyRulePackage(Type declaringType, string projectName = "");
    }

    public interface IMayExposeRulePackageForType
    {
        VerifyRulePackage ExposeVerifyRulePackage();
    }

    public interface IMayExposeUnregisteredRulePackageForType
    {
        VerifyRulePackage ExposeUnregisteredVerifyRulePackage();
    }

    // public interface IMayExposeRulePackageForMember
    // {
    //     VerifyMemberRulePackage ExposeVerifyMemberRulePackage();
    // }
    //
    // public interface IMayExposeUnregisteredRulePackageForMember
    // {
    //     VerifyMemberRulePackage ExposeUnregisteredVerifyMemberRulePackage();
    // }
}