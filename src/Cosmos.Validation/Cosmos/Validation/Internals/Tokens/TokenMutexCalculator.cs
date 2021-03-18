using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Extensions;

namespace Cosmos.Validation.Internals.Tokens
{
    /// <summary>
    /// Mutex Calculator <br />
    /// 互斥计算工具
    /// </summary>
    internal static class TokenMutexCalculator
    {
        /// <summary>
        /// Calculate whether the given token is mutually exclusive with the currently existing token group.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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