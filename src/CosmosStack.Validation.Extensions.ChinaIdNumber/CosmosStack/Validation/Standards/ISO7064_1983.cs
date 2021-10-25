using System;
using System.Linq;

namespace CosmosStack.Validation.Standards
{
    // ReSharper disable once InconsistentNaming
    internal static class ISO7064_1983
    {
        public static int MOD11_2(int[] source, int[] weightFactors, int mod)
        {
            if (source is null || weightFactors is null || source.Length != weightFactors.Length)
                throw new ArgumentException("The inout parameters do not comply with ISO7064 MOD 11, 2 Specifications.");
            var sum = source.Select((num, idx) => num * weightFactors[idx]).Sum();
            return sum % mod;
        }
    }
}