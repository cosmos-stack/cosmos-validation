using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Extensions;

namespace Cosmos.Validation.Internals.Tokens
{
    internal static class TokenMutexCalculator
    {
        public static bool Available(List<IValueToken> tokens, IValueToken token)
        {
            if (!tokens.Any())
                return true;

            if (token.MutuallyExclusive)
                return false;

            return !tokens.Any(x => x.MutuallyExclusive && x.MutuallyExclusiveFlags.ContainsAny(token.MutuallyExclusiveFlags));
        }
    }
}