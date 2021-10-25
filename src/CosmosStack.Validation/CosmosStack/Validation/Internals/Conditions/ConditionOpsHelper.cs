namespace CosmosStack.Validation.Internals.Conditions
{
    internal static class ConditionOpsHelper
    {
        /// <summary>
        /// 如果 Right 的优先级高于 Left，
        /// 则需要将 Left 打包为 GroupedToken，然后再进行联立
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool IsHigherPriority(ConditionOps left, ConditionOps right)
        {
            if (left == ConditionOps.Break) return false;
            if (left == ConditionOps.And && left < right) return true;
            if (left == ConditionOps.Or) return false;
            return false;
        }
    }
}