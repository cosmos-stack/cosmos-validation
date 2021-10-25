using CosmosStack.Validation.Internals.Rules;

// ReSharper disable once CheckNamespace
namespace CosmosStack.Validation
{
    public static partial class CorrectValueRuleExtensions
    {
        private static CorrectValueRuleBuilder<T, TVal> _impl<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            return (CorrectValueRuleBuilder<T, TVal>) builder;
        }

        private static CorrectValueRuleBuilder<T> _impl<T>(this IValueRuleBuilder<T> builder)
        {
            return (CorrectValueRuleBuilder<T>) builder;
        }

        private static CorrectValueRuleBuilder _impl(this IValueRuleBuilder builder)
        {
            return (CorrectValueRuleBuilder) builder;
        }
    }
}