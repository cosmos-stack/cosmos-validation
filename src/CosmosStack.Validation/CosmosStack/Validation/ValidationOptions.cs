namespace CosmosStack.Validation
{
    /// <summary>
    /// Validation options <br />
    /// 验证选项
    /// </summary>
    public class ValidationOptions
    {
        /// <summary>
        /// Mark whether the annotation validator is valid. <br />
        /// 开启注解验证功能。默认为 True。
        /// </summary>
        public bool AnnotationEnabled { get; set; } = true;

        /// <summary>
        /// Mark whether the custom validator is valid. <br />
        /// 开启自定义验证器功能。默认为 True。
        /// </summary>
        public bool CustomValidatorEnabled { get; set; } = true;

        /// <summary>
        /// When the verified instance is empty, whether to return a failure result. <br />
        /// 实例为空时，将视作验证失败。默认为 True。
        /// </summary>
        public bool FailureIfInstanceIsNull { get; set; } = true;

        /// <summary>
        /// When the type of rule does not exist, whether to return a failure result. <br />
        /// 类型（Type Project）未命中时，将视作验证失败。默认为 False。
        /// </summary>
        public bool FailureIfProjectNotMatch { get; set; } = false;
    }
}