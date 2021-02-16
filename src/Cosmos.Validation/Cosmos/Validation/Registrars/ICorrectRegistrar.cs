using System;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Registrars
{
    internal interface ICorrectRegistrar
    {
        void BuildForMember(Type type, string memberName, Func<IValueRuleBuilder, IValueRuleBuilder> func);
        void BuildForMember(Type type, string memberName, string name, Func<IValueRuleBuilder, IValueRuleBuilder> func);
        void BuildForMember(ObjectValueContract contract, Func<IValueRuleBuilder, IValueRuleBuilder> func);
        void BuildForMember(ObjectValueContract contract, string name, Func<IValueRuleBuilder, IValueRuleBuilder> func);
        void BuildForMember(CorrectValueRule rule);
        void BuildForMember(string name, CorrectValueRule rule);
        void BuildForMember<T>(string memberName, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);
        void BuildForMember<T>(string memberName, string name, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);
        void BuildForMember<T>(ObjectValueContract contract, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);
        void BuildForMember<T>(ObjectValueContract contract, string name, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);
        void BuildForMember<T>(CorrectValueRule rule);
        void BuildForMember<T>(string name, CorrectValueRule rule);
    }
}