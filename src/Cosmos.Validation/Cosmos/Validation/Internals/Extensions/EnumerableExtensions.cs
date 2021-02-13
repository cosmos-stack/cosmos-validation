using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Validation.Internals.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return source.Any(other.Contains);
        }
    }
}