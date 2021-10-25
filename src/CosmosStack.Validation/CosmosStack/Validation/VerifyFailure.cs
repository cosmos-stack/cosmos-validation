using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CosmosStack.Text;

namespace CosmosStack.Validation
{
    /// <summary>
    /// It is used to keep the information of verification failure. <br />
    /// 验证失败信息
    /// </summary>
    [Serializable]
    public class VerifyFailure
    {
        private VerifyFailure() { }

        /// <summary>
        /// Create an instance of VerifyFailure.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessage"></param>
        /// <param name="verifiedValue"></param>
        public VerifyFailure(
            string propertyName,
            string errorMessage,
            object verifiedValue = null)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            VerifiedValue = verifiedValue;
        }

        /// <summary>
        /// The name of property <br />
        /// 被验证的属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The error message <br />
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The verified value <br />
        /// 验证的值
        /// </summary>
        public object VerifiedValue { get; set; }

        /// <summary>
        /// Error details <br />
        /// 错误详情
        /// </summary>
        public List<VerifyError> Details { get; set; } = new();

        /// <summary>
        /// Error code <br />
        /// 错误代码
        /// </summary>
        public long Code { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(ErrorMessage);

            if (Details is not null && Details.Any())
            {
                builder.AppendLine();
                builder.AppendLine("Detail(s):");
                foreach (var error in Details)
                {
                    builder.AppendLine($"  - {error.ErrorMessage}");
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// To Structured StringVal <br />
        /// 转换为结构化字符串值
        /// </summary>
        /// <returns></returns>
        public StructuredStringVal ToStringVal()
        {
            var result = new StructuredStringVal(ErrorMessage);

            if (Details is not null && Details.Any())
            {
                foreach (var error in Details)
                {
                    result.Append(error.ErrorMessage);
                }
            }
            
            return result;
        }

        /// <summary>
        /// Create a VerifyFailure object based on the given parameters.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static VerifyFailure Create(string propertyName, string errorMessage)
        {
            return new(propertyName, errorMessage);
        }

        /// <summary>
        /// Create a VerifyFailure object based on the given parameters.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="errorMessage"></param>
        /// <param name="verifiedValue"></param>
        /// <returns></returns>
        public static VerifyFailure Create(string propertyName, string errorMessage, object verifiedValue)
        {
            return new(propertyName, errorMessage, verifiedValue);
        }
    }
}