﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cosmos.Validation
{
    /// <summary>
    /// A collection containing multiple VerifyResults.
    /// </summary>
    public class VerifyResultCollection : IEnumerable<VerifyResult>, IVerifyResult
    {
        private readonly List<VerifyResult> _results;

        /// <summary>
        /// Create an instance of VerifyResultCollection.
        /// </summary>
        public VerifyResultCollection()
        {
            _results = new();
        }

        /// <summary>
        /// Create an instance of VerifyResultCollection.
        /// </summary>
        /// <param name="results"></param>
        public VerifyResultCollection(IEnumerable<VerifyResult> results)
        {
            _results = results is null ? new() : results.ToList();
        }

        /// <summary>
        /// Return the verification result.
        /// </summary>
        public bool IsValid => _results.All(x => x.IsValid);

        /// <summary>
        /// A collection of errors
        /// </summary>
        public IEnumerable<VerifyFailure> Errors => _results.SelectMany(result => result.Errors);

        /// <summary>
        /// Returns the names of all fields with errors.
        /// </summary>
        public IEnumerable<string> MemberNames => Errors.Select(e => e.PropertyName).Distinct();

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<VerifyResult> GetEnumerator() => _results.GetEnumerator();

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _results.GetEnumerator();

        /// <summary>
        /// Create an empty VerifyResultCollection object.
        /// </summary>
        public static VerifyResultCollection Empty { get; } = new(Enumerable.Empty<VerifyResult>());

        /// <summary>
        /// Combine multiple internal VerifyResults into one and return it.
        /// </summary>
        /// <returns></returns>
        public VerifyResult ToOneVerifyResult()
        {
            return VerifyResult.MakeTogether(_results);
        }
    }
}