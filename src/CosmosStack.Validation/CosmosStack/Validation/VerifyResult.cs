using System;
using System.Collections.Generic;
using System.Linq;
using CosmosStack.Text;
using CosmosStack.Validation.Internals.Exceptions;

namespace CosmosStack.Validation
{
    /// <summary>
    /// The object containing the verification result. <br />
    /// 验证结果
    /// </summary>
    [Serializable]
    public class VerifyResult : IVerifyResult
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

        /// <summary>
        /// Create an instance of VerifyResult.
        /// </summary>
        public VerifyResult()
        {
            _failures = new List<VerifyFailure>();
            _success = !_failures.Any();
        }

        /// <summary>
        /// Create an instance of VerifyResult.
        /// </summary>
        /// <param name="failure"></param>
        public VerifyResult(VerifyFailure failure)
        {
            _failures = new List<VerifyFailure>();
            if (failure is not null)
                _failures.Add(failure);
            _success = !_failures.Any();
        }

        /// <summary>
        /// Create an instance of VerifyResult.
        /// </summary>
        /// <param name="failures"></param>
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
        /// Return the verification result.
        /// </summary>
        public bool IsValid => InternalSuccess;

        /// <summary>
        /// Return all error messages.
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

        /// <summary>
        /// Returns the names of all fields with errors.
        /// </summary>
        public IList<string> MemberNames => Errors.Select(e => e.PropertyName).ToList();

        #region ToString

        /// <summary>
        /// Output the result of VerifyResult as a string, and separate each VerifyFailure message with a newline character.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(Environment.NewLine);
        }

        /// <summary>
        /// Output the result of VerifyResult as a string, and use the given separator to separate each VerifyFailure message.
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string ToString(string separator)
        {
            return string.Join(separator, _failures.Select(f => f.ToString()));
        }

        public StructuredStringVal ToStringVal(bool includeDetails = false, string message = null)
        {
            var result = InternalSuccess
                ? new StructuredStringVal("Success")
                : new StructuredStringVal(message ?? $"Verification failure: A total of {_failures.Count} failures occurred.");

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

        /// <summary>
        /// Returns a VerifyResult object marked as successful.
        /// </summary>
        public static VerifyResult Success { get; } = new() { InternalSuccess = true };

        /// <summary>
        /// Determine whether the given VerifyResult object is marked as a successful verification.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsSuccess(VerifyResult result)
        {
            return result?.InternalSuccess ?? false;
        }

        #endregion

        #region Failure

        /// <summary>
        /// For design reasons, return verification failure.
        /// </summary>
        public static VerifyResult ByDesign { get; } = new(VerifyFailure.Create("Instance", "By Design."));

        /// <summary>
        /// Because it is a null reference, the return verification failed.
        /// </summary>
        public static VerifyResult NullReference { get; } = new(VerifyFailure.Create("Instance", "Null Reference."));

        /// <summary>
        /// Because it is a null reference, the verification failed with the given error message returned.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static VerifyResult NullReferenceWith(string paramName) => new(VerifyFailure.Create(paramName, "Null Reference."));

        /// <summary>
        /// Since it is not the expected type, the return verification failed.
        /// </summary>
        public static VerifyResult UnexpectedType { get; } = new(VerifyFailure.Create("$Type", "Unexpected Type."));

        /// <summary>
        /// Since it is not the expected type, the verification failed with the given error message.
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static VerifyResult UnexpectedTypeWith(string paramName) => new(VerifyFailure.Create(paramName, "Unexpected Type."));

        /// <summary>
        /// No rules are registered for this type, and verification failure is returned.
        /// </summary>
        internal static VerifyResult UnregisterProjectForSuchType { get; } = new(VerifyFailure.Create("$TypedProject", "The corresponding type of Project is not registered."));

        /// <summary>
        /// No rules are registered for this type and name, and verification failure is returned.
        /// </summary>
        internal static VerifyResult UnregisterProjectForSuchNamedType { get; } = new(VerifyFailure.Create("$NamedProject", "The Project of the corresponding type and name is not registered."));

        /// <summary>
        /// The member does not exist, and the verification fails.
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        internal static VerifyResult MemberIsNotExists(string memberName) => new(VerifyFailure.Create(memberName, "Member name is not exists."));

        #endregion

        #region Operator

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

        /// <summary>
        /// Combine multiple sub-results into one main result.
        /// </summary>
        /// <param name="masterResult"></param>
        /// <param name="slaveResults"></param>
        /// <returns></returns>
        public static VerifyResult Merge(VerifyResult masterResult, params VerifyResult[] slaveResults)
        {
            if (slaveResults is null || !slaveResults.Any())
                return masterResult;

            foreach (var result in slaveResults)
            {
                if (result is null)
                    continue;

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

        /// <summary>
        /// Combine multiple results into one result.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static VerifyResult MakeTogether(List<VerifyResult> results)
        {
            if (results is null)
                return Success;

            if (results.Where(v => v is not null).All(v => v.IsValid))
                return Success;

            var mainResult = new VerifyResult();

            return Merge(mainResult, results.ToArray());
        }

        /// <summary>
        /// Combine multiple results into one result.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static VerifyResult MakeTogether(params VerifyResult[] results)
        {
            if (results is null)
                return Success;

            if (results.Where(v => v is not null).All(v => v.IsValid))
                return Success;

            var mainResult = new VerifyResult();

            return Merge(mainResult, results.ToArray());
        }

        /// <summary>
        /// Create a VerifyResultCollection object, which contains a set of given VerifyResult.
        /// </summary>
        /// <param name="masterResult"></param>
        /// <param name="slaveResults"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create a VerifyResultCollection object, which contains a set of given VerifyResult.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static VerifyResultCollection ToList(List<VerifyResult> results)
        {
            if (results is null)
                return VerifyResultCollection.Empty;
            return new VerifyResultCollection(results);
        }

        #endregion

        #region Raise

        /// <summary>
        /// If the verification result fails, an exception is thrown.
        /// </summary>
        /// <exception cref="ValidationException"></exception>
        public void Raise()
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this);
            }
        }

        /// <summary>
        /// If the verification result fails, an exception is thrown.
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ValidationException"></exception>
        public void Raise(string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, message);
            }
        }

        /// <summary>
        /// If the verification result fails, an exception is thrown.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <exception cref="ValidationException"></exception>
        public void Raise(long errorCode, string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, errorCode, message);
            }
        }

        /// <summary>
        /// If the verification result fails, an exception is thrown.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="message"></param>
        /// <exception cref="ValidationException"></exception>
        public void Raise(string flag, string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, flag, message);
            }
        }

        /// <summary>
        /// If the verification result fails, an exception is thrown.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="flag"></param>
        /// <param name="message"></param>
        /// <exception cref="ValidationException"></exception>
        public void Raise(long errorCode, string flag, string message)
        {
            if (!IsValid)
            {
                throw ExceptionFactory.Create(this, errorCode, flag, message);
            }
        }

        #endregion

        #region ToException

        /// <summary>
        /// If the verification result fails, an exception will be returned.
        /// </summary>
        /// <returns></returns>
        public ValidationException ToException()
        {
            return IsValid ? default : ExceptionFactory.Create(this);
        }

        /// <summary>
        /// If the verification result fails, an exception will be returned.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ValidationException ToException(string message)
        {
            return IsValid ? default : ExceptionFactory.Create(this, message);
        }

        /// <summary>
        /// If the verification result fails, an exception will be returned.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ValidationException ToException(long errorCode, string message)
        {
            return IsValid ? default : ExceptionFactory.Create(this, errorCode, message);
        }

        /// <summary>
        /// If the verification result fails, an exception will be returned.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ValidationException ToException(string flag, string message)
        {
            return IsValid ? default : ExceptionFactory.Create(this, flag, message);
        }

        /// <summary>
        /// If the verification result fails, an exception will be returned.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="flag"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ValidationException ToException(long errorCode, string flag, string message)
        {
            return IsValid ? default : ExceptionFactory.Create(this, errorCode, flag, message);
        }

        #endregion
    }
}