using System.Collections.Generic;

namespace CosmosStack.Validation
{
    /// <summary>
    /// The interface used to constrain the verification results. <br />
    /// 验证结果接口
    /// </summary>
    public interface IVerifyResult
    {
        /// <summary>
        /// Return the verification result. <br />
        /// 返回一个验证解雇
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Return all error messages. <br />
        /// 异常集
        /// </summary>
        public IList<VerifyFailure> Errors { get; }

        /// <summary>
        /// Returns the names of all fields with errors. <br />
        /// 所有经过验证的成员名称
        /// </summary>
        public IList<string> MemberNames { get; }
    }
}