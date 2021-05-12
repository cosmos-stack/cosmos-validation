using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Projects
{
    public class NamedTypeProject : IProject
    {
        private readonly List<CorrectValueRule> _rules;

        public NamedTypeProject(Type type, string name)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = name;
            _rules = new();
        }

        public string Name { get; }

        public Type Type { get; }

        public ProjectClass Class => ProjectClass.Named;

        internal void UpdateRules(IEnumerable<CorrectValueRule> rules)
        {
            foreach (var rule in rules) _rules.Add(rule);
        }

        public VerifyRulePackage ExposeRules() => new(Type, _rules);

        // public VerifyMemberRulePackage ExposeMemberRules(string memberName)
        // {
        //     if (string.IsNullOrWhiteSpace(memberName))
        //         return VerifyMemberRulePackage.Empty;
        //
        //     var rule = _rules.FirstOrDefault(x => x.MemberName == memberName);
        //
        //     if (rule is null)
        //         return VerifyMemberRulePackage.Empty;
        //
        //     return new(Type, rule.Contract, rule);
        // }

        public VerifyResult Verify(VerifiableObjectContext context)
        {
            return CorrectEngine.Valid(
                VerifiableOpsContext.Create(context),
                _rules);
        }

        public VerifyResult VerifyOne(VerifiableMemberContext context)
        {
            return CorrectEngine.ValidOne(
                VerifiableOpsContext.Create(context),
                _rules.Where(x => x.MemberName == context.MemberName).ToList());
        }

        public VerifyResult VerifyMany(IDictionary<string, VerifiableMemberContext> keyValueCollections)
        {
            return CorrectEngine.ValidMany(keyValueCollections, _rules);
        }
    }
}