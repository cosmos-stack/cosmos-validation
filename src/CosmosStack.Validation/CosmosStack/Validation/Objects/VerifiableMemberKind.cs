namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable member kind <br />
    /// 可验证成员种类
    /// </summary>
    public enum VerifiableMemberKind
    {
        /// <summary>
        /// Property <br />
        /// 属性
        /// </summary>
        Property,

        /// <summary>
        /// Field <br />
        /// 字段
        /// </summary>
        Field,

        /// <summary>
        /// Unknown <br />
        /// 为止
        /// </summary>
        Unknown,

        /// <summary>
        /// Custom contract <br />
        /// 自定义约定
        /// </summary>
        CustomContract
    }
}