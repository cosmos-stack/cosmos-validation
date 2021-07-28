using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cosmos.Validation.Internals.Extensions
{
    internal static class EnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return source.Any(other.Contains);
        }
    }
}