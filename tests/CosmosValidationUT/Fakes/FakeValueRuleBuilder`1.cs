using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cosmos.Reflection;
using Cosmos.Validation;
using Cosmos.Validation.Objects;

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder<T> : IValueRuleBuilder<T>
    {
        internal readonly VerifiableMemberContract Contract;

        public FakeValueRuleBuilder(VerifiableMemberContract contract)
        {
            Contract = contract;
        }

        public IValueRuleBuilder<T> AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> And()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Or()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Empty()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Required()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Null()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotNull()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Range(object @from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RangeWithOpenInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RangeWithCloseInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Length(int min, int max)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> MinLength(int min)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> MaxLength(int max)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> AtLeast(int count)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Equal(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Equal(Func<object> valueFunc, Type valueType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Equal(Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotEqual(Func<object> valueFunc, Type valueType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotEqual(Func<object> valueFunc, Type valueType, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> LessThan(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> LessThan(Func<object> valueFunc, Type valueType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> LessThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> LessThanOrEqual(Func<object> valueFunc, Type valueType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> GreaterThan(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> GreaterThan(Func<object> valueFunc, Type valueType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> GreaterThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> GreaterThanOrEqual(Func<object> valueFunc, Type valueType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Regex regex)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(string regexExpression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(string regexExpression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Func<T, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Expression<Func<T, Regex>> expression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Expression<Func<T, string>> expression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Expression<Func<T, string>> expression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Func<object, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Func<object, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T> Func(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T> Predicate(Predicate<object> predicate)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T> Satisfies(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> Satisfies(Func<object, bool> func, string message)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> In(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> In(Func<ICollection<object>> collectionFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> In(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotIn(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotIn(Func<ICollection<object>> collectionFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> NotIn(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredString()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredBoolean()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredGuid()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredDateTime(DateTimeStyles style = DateTimeStyles.None)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredDateInfo(DateTimeStyles style = DateTimeStyles.None)
        {
            throw new NotImplementedException();
        }
    }
}