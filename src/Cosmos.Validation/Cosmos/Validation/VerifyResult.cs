using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Text;
using Cosmos.Validation.Internals.Exceptions;

namespace Cosmos.Validation
{
    [Serializable]
    public class VerifyResult
    {
        private readonly List<VerifyFailure> _failures;

        private bool _success;

        private bool InternalSuccess
        {
            get => _success && _failures.Count == 0;
            set
            {
                _success = value;
                if (value) _failures.Clear();
            }
        }

        public VerifyResult()
        {
            _failures = new List<VerifyFailure>();
            _success = !_failures.Any();
        }

        public VerifyResult(VerifyFailure failure)
        {
            _failures = new List<VerifyFailure>();
            if(failure is not null)
                _failures.Add(failure);
            _success = !_failures.Any();
        }

        public VerifyResult(IEnumerable<VerifyFailure> failures)
        {
            _failures = failures.Where(f => f != null).ToList();
            _success = !_failures.Any();
        }

        /// <summary>
        /// Name collection of executed validation rules. 
        /// </summary>
        public string[] NameOfExecutedRules { get; internal set; }

        /// <summary>
        /// Is valid
        /// </summary>
        public bool IsValid => InternalSuccess;

        /// <summary>
        /// A collection of errors
        /// </summary>
        public IList<VerifyFailure> Errors
        {
            get
            {
                if (_success)
                    return _failures.AsReadOnly();
                return _failures;
            }
        }

        #region ToString

        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string ToString(string separator)
        {
            return string.Join(separator, _failures.Select(f => f.ToString()));
        }

        public StringVal ToStringVal(bool includeDetails = false, string message = null)
        {
            var result = InternalSuccess
                ? new StringVal("Success")
                : new StringVal(message ?? $"Verification failure: A total of {_failures.Count} failures occurred.");

            if (includeDetails)
            {
                foreach (var failure in _failures)
                {
                    result.Append(failure.ToStringVal());
                }
            }

            return result;
        }

        #endregion

        #region Success

        public static VerifyResult Success { get; } = new() {InternalSuccess = true};

        public static bool IsSuccess(VerifyResult result)
        {
            return result?.InternalSuccess ?? false;
        }

        #endregion

        #region Failure

        public static VerifyResult ByDesign { get; } = new(VerifyFailure.Create("Instance", "By Design."));

        public static VerifyResult NullReference { get; } = new(VerifyFailure.Create("Instance", "Null Reference."));

        public static VerifyResult NullReferenceWith(string paramName) => new(VerifyFailure.Create(paramName, "Null Reference."));

        public static VerifyResult UnexpectedType { get; } = new(VerifyFailure.Create("$Type", "Unexpected Type."));

        public static VerifyResult UnexpectedTypeWith(string paramName) => new(VerifyFailure.Create(paramName, "Unexpected Type."));

        internal static VerifyResult UnregisterProjectForSuchType { get; } = new(VerifyFailure.Create("$TypedProject", "The corresponding type of Project is not registered."));

        internal static VerifyResult UnregisterProjectForSuchNamedType { get; } = new(VerifyFailure.Create("$NamedProject", "The Project of the corresponding type and name is not registered."));

        internal static VerifyResult MemberIsNotExists(string memberName)=>new(VerifyFailure.Create(memberName, "Member name is not exists."));

        
        #endregion

        #region Base

        public static bool operator ==(VerifyResult left, VerifyResult right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            if (left.InternalSuccess && right.InternalSuccess) return true;
            return left.Equals(right);
        }

        public static bool operator !=(VerifyResult left, VerifyResult right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is VerifyResult result)
                return Equals(result);

            return false;
        }

        protected bool Equals(VerifyResult other)
        {
            if (other is null) return false;
            if (other.InternalSuccess && InternalSuccess) return true;

            return Equals(_failures, other._failures) &&
                   InternalSuccess == other.InternalSuccess &&
                   Equals(NameOfExecutedRules, other.NameOfExecutedRules);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_failures);
        }

        #endregion

        #region Merge

        public static VerifyResult Merge(VerifyResult masterResult, params VerifyResult[] slaveResults)
        {
            if (slaveResults is null || !slaveResults.Any())
                return masterResult;

            foreach (var result in slaveResults)
            {
                if (result.IsValid)
                    continue;

                foreach (var failure in result._failures)
                {
                    var mainFailure = masterResult._failures.FirstOrDefault(x => x.PropertyName == failure.PropertyName);

                    if (mainFailure is null)
                        masterResult._failures.Add(failure);
                    else
                        mainFailure.Details.AddRange(failure.Details);
                }
            }

            return masterResult;
        }

        public static VerifyResult MakeTogether(List<VerifyResult> results)
        {
            if (results is null)
                return Success;

            var mainResult = new VerifyResult();

            return Merge(mainResult, results.ToArray());
        }

        public static VerifyResultCollection ToList(VerifyResult masterResult, params VerifyResult[] slaveResults)
        {
            var results = new List<VerifyResult>();

            if (masterResult is not null)
                results.Add(masterResult);

            foreach (var result in slaveResults)
                if (result is not null)
                    results.Add(result);

            return new VerifyResultCollection(results);
        }

        public static VerifyResultCollection ToList(List<VerifyResult> results)
        {
            if (results is null)
                return VerifyResultCollection.Empty;
            return new VerifyResultCollection(results);
        }

        #endregion

        #region Raise

        public void Raise(string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, message);
            }
        }

        public void Raise(long errorCode, string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, errorCode, message);
            }
        }

        public void Raise(string flag, string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, flag, message);
            }
        }

        public void Raise(long errorCode, string flag, string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, errorCode, flag, message);
            }
        }

        #endregion
    }
}