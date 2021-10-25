namespace CosmosStack.Validation
{
    /// <summary>
    /// Custom verify result <br />
    /// 自定义验证结果
    /// </summary>
    public class CustomVerifyResult
    {
        /// <summary>
        /// Verify result <br />
        /// 验证结果
        /// </summary>
        public bool VerifyResult { get; set; }

        /// <summary>
        /// Error message <br />
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// Operation name <br />
        /// 操作名称
        /// </summary>
        public string OperationName { get; set; }
    }
}