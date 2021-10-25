namespace CosmosStack.Validation.Internals.Rules
{
    internal enum CorrectValueRuleMode
    {
        Append,
        Overwrite,
    }

    internal static class CorrectValueRuleModeExtensions
    {
        public static CorrectValueRuleMode X(this VerifyRuleMode mode)
        {
            return mode switch
            {
                VerifyRuleMode.Append => CorrectValueRuleMode.Append,
                VerifyRuleMode.Overwrite => CorrectValueRuleMode.Overwrite,
                _ => CorrectValueRuleMode.Append
            };
        }

        public static VerifyRuleMode X(this CorrectValueRuleMode mode)
        {
            return mode switch
            {
                CorrectValueRuleMode.Append => VerifyRuleMode.Append,
                CorrectValueRuleMode.Overwrite => VerifyRuleMode.Overwrite,
                _ => VerifyRuleMode.Append
            };
        }
    }
}