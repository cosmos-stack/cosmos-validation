namespace CosmosStack.Validation
{
    /// <summary>
    /// Verify the effective mode of the strategy. <br />
    /// 策略模式
    /// </summary>
    public enum StrategyMode
    {
        /// <summary>
        /// Append <br />
        /// 附加
        /// </summary>
        Append,

        /// <summary>
        /// Item overwrite <br />
        /// 条目覆写
        /// </summary>
        ItemOverwrite,

        /// <summary>
        /// Overall overwrite <br />
        /// 全部覆写
        /// </summary>
        OverallOverwrite
    }
}