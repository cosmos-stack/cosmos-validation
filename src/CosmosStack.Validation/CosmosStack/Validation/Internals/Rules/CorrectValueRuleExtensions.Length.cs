using CosmosStack.Validation.Internals.Tokens.ValueTokens;

// ReSharper disable once CheckNamespace
namespace CosmosStack.Validation
{
    public static partial class CorrectValueRuleExtensions
    {
        //`0

        public static IPredicateValueRuleBuilder Length(this IValueRuleBuilder builder, int min, int max)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLengthLimitedToken(current._contract, min, max);
            return current;
        }

        public static IPredicateValueRuleBuilder Range(this IValueRuleBuilder builder, object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken(current._contract, from, to, options);
            return current;
        }

        public static IPredicateValueRuleBuilder RangeWithOpenInterval(this IValueRuleBuilder builder, object from, object to)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken(current._contract, from, to, RangeOptions.OpenInterval);
            return current;
        }

        public static IPredicateValueRuleBuilder RangeWithCloseInterval(this IValueRuleBuilder builder, object from, object to)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken(current._contract, from, to, RangeOptions.CloseInterval);
            return current;
        }

        public static IPredicateValueRuleBuilder MinLength(this IValueRuleBuilder builder, int min)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMinLengthLimitedToken(current._contract, min);
            return current;
        }

        public static IPredicateValueRuleBuilder MaxLength(this IValueRuleBuilder builder, int max)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMaxLengthLimitedToken(current._contract, max);
            return current;
        }

        public static IPredicateValueRuleBuilder AtLeast(this IValueRuleBuilder builder, int count)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMinLengthLimitedToken(current._contract, count);
            return current;
        }

        //`1

        public static IPredicateValueRuleBuilder<T> Range<T>(this IValueRuleBuilder<T> builder, object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken(current._contract, from, to, options);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> RangeWithOpenInterval<T>(this IValueRuleBuilder<T> builder, object from, object to)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken(current._contract, from, to, RangeOptions.OpenInterval);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> RangeWithCloseInterval<T>(this IValueRuleBuilder<T> builder, object from, object to)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken(current._contract, from, to, RangeOptions.CloseInterval);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> Length<T>(this IValueRuleBuilder<T> builder, int min, int max)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLengthLimitedToken(current._contract, min, max);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> MinLength<T>(this IValueRuleBuilder<T> builder, int min)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMinLengthLimitedToken(current._contract, min);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> MaxLength<T>(this IValueRuleBuilder<T> builder, int max)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMaxLengthLimitedToken(current._contract, max);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> AtLeast<T>(this IValueRuleBuilder<T> builder, int count)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMinLengthLimitedToken(current._contract, count);
            return current;
        }

        //`2

        public static IPredicateValueRuleBuilder<T, TVal> Range<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken<TVal>(current._contract, from, to, options);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> RangeWithOpenInterval<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal from, TVal to)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken<TVal>(current._contract, from, to, RangeOptions.OpenInterval);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> RangeWithCloseInterval<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TVal from, TVal to)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRangeToken<TVal>(current._contract, from, to, RangeOptions.CloseInterval);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> Length<T, TVal>(this IValueRuleBuilder<T, TVal> builder, int min, int max)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueLengthLimitedToken(current._contract, min, max);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> MinLength<T, TVal>(this IValueRuleBuilder<T, TVal> builder, int min)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMinLengthLimitedToken(current._contract, min);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> MaxLength<T, TVal>(this IValueRuleBuilder<T, TVal> builder, int max)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMaxLengthLimitedToken(current._contract, max);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> AtLeast<T, TVal>(this IValueRuleBuilder<T, TVal> builder, int count)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueMinLengthLimitedToken(current._contract, count);
            return current;
        }
    }
}