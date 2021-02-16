using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Validation
{
    public class VerifyResultCollection : IEnumerable<VerifyResult>
    {
        private readonly List<VerifyResult> _results;

        public VerifyResultCollection()
        {
            _results = new();
        }

        public VerifyResultCollection(IEnumerable<VerifyResult> results)
        {
            _results = results is null ? new() : results.ToList();
        }

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid => _results.All(x => x.IsValid);

        public IEnumerator<VerifyResult> GetEnumerator() => _results.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _results.GetEnumerator();

        public static VerifyResultCollection Empty { get; } = new(Enumerable.Empty<VerifyResult>());

        public VerifyResult ToOneVerifyResult()
        {
            return VerifyResult.MakeTogether(_results);
        }
    }
}