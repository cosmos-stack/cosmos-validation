using System;
using System.Collections.Generic;
using System.Linq;
using CosmosStack.Reflection;
using CosmosStack.Validation.Internals;
using CosmosStack.Validation.Internals.Rules;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Projects
{
    /// <summary>
    /// 类型项目
    /// </summary>
    public class TypedProject : IProject
    {
        private readonly List<CorrectValueRule> _rules;

        public TypedProject(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = type.GetFullyQualifiedName();
            _rules = new();
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public Type Type { get; }

        /// <inheritdoc />
        public ProjectClass Class => ProjectClass.Typed;

        internal void UpdateRules(IEnumerable<CorrectValueRule> rules)
        {
            foreach (var rule in rules) _rules.Add(rule);
        }

        /// <inheritdoc />
        public VerifyRulePackage ExposeRules() => new(Type, _rules);

        /// <inheritdoc />
        public VerifyMemberRulePackage ExposeMemberRules(string memberName)
        {
            if (string.IsNullOrWhiteSpace(memberName))
                return VerifyMemberRulePackage.Empty;

            var rule = _rules.FirstOrDefault(x => x.MemberName == memberName);

            if (rule is null)
                return VerifyMemberRulePackage.Empty;

            return new(Type, rule.Contract, rule);
        }

        /// <inheritdoc />
        public VerifyResult Verify(VerifiableObjectContext context)
        {
            return CorrectEngine.Valid(
                VerifiableOpsContext.Create(context),
                _rules);
        }

        /// <inheritdoc />
        public VerifyResult VerifyOne(VerifiableMemberContext context)
        {
            return CorrectEngine.ValidOne(
                VerifiableOpsContext.Create(context),
                _rules.Where(x => x.MemberName == context.MemberName).ToList());
        }

        /// <inheritdoc />
        public VerifyResult VerifyMany(IDictionary<string, VerifiableMemberContext> keyValueCollections)
        {
            return CorrectEngine.ValidMany(keyValueCollections, _rules);
        }
    }
}