namespace Cosmos.Validation
{
    /// <summary>
    /// Verify the effective mode of the strategy.
    /// </summary>
    public enum StrategyMode
    {
        /// <summary>
        /// Append
        /// </summary>
        Append,

        /// <summary>
        /// Item overwrite
        /// </summary>
        ItemOverwrite,

        /// <summary>
        /// Overall overwrite
        /// </summary>
        OverallOverwrite
    }
}