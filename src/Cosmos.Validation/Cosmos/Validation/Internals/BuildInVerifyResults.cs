using System.Collections.Generic;

namespace Cosmos.Validation.Internals
{
    internal static class BuildInVerifyResults
    {
        static BuildInVerifyResults()
        {
            NullObjectReference = new VerifyResult(new List<VerifyFailure>
            {
                new("$Instance", "Instance cannot be null reference.")
            });
            UnregisterProjectForSuchType = new VerifyResult(new List<VerifyFailure>
            {
                new("$TypedProject", "The corresponding type of Project is not registered.")
            });
            UnregisterProjectForSuchNamedType = new VerifyResult(new List<VerifyFailure>
            {
                new("$NamedProject", "The Project of the corresponding type and name is not registered.")
            });
        }

        public static VerifyResult NullObjectReference { get; }
        
        public static VerifyResult UnregisterProjectForSuchType { get; }
        
        public static VerifyResult UnregisterProjectForSuchNamedType { get; }
    }
}