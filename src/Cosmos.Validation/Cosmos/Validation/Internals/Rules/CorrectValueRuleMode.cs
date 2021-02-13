namespace Cosmos.Validation.Internals.Rules
{
    internal enum CorrectValueRuleMode
    {
        Append,
        Overwrite,
    }

    internal static class CorrectValueRuleModeExtensions
    {
        public static CorrectValueRuleMode X(this ValueRuleMode mode)
        {
            return mode switch
            {
                ValueRuleMode.Append => CorrectValueRuleMode.Append,
                ValueRuleMode.Overwrite => CorrectValueRuleMode.Overwrite,
                _ => CorrectValueRuleMode.Append
            };
        }

        public static ValueRuleMode X(this CorrectValueRuleMode mode)
        {
            return mode switch
            {
                CorrectValueRuleMode.Append => ValueRuleMode.Append,
                CorrectValueRuleMode.Overwrite => ValueRuleMode.Overwrite,
                _ => ValueRuleMode.Append
            };
        }
    }
}