namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May build interface <br />
    /// 标记注册器可被构建
    /// </summary>
    public interface IMayBuild
    {
        /// <summary>
        /// Build <br />
        /// 构建
        /// </summary>
        void Build();
    }
}