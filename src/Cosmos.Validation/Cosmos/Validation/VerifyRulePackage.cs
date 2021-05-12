using System;
using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    public sealed class VerifyRulePackage
    {
        private readonly List<CorrectValueRule> _rules;

        private VerifyRulePackage()
        {
            DeclaringType = typeof(object);
            _rules = Colls.Empty<CorrectValueRule>();
        }

        internal VerifyRulePackage(Type declaringType, List<CorrectValueRule> rules)
        {
            DeclaringType = declaringType;
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public Type DeclaringType { get; }

        public int Count() => _rules.Count;

        public static VerifyRulePackage Empty { get; } = new();

        internal List<CorrectValueRule> ExposeRules() => _rules;

        internal IEnumerable<VerifyMemberRulePackage> ExposeMemberRulePackages()
        {
            foreach (var rule in _rules)
                yield return new VerifyMemberRulePackage(DeclaringType, rule.Contract, rule);
        }

        public IValidator AsValidator() => new VerifyRulePackageValidator(this);

        public IValidator AsValidator(ValidationOptions options) => new VerifyRulePackageValidator(this, options);

        public static explicit operator List<VerifyMemberRulePackage>(VerifyRulePackage package)
        {
            if (package is null) throw new ArgumentNullException(nameof(package));
            List<VerifyMemberRulePackage> ret = new();
            foreach (var rule in package.ExposeMemberRulePackages())
                ret.Add(new VerifyMemberRulePackage(package.DeclaringType, rule.ExposeRule().Contract, rule.ExposeRule()));
            return ret;
        }
    }

    public sealed class VerifyMemberRulePackage
    {
        private readonly CorrectValueRule _rule;
        private readonly VerifiableMemberContract _contract;

        private VerifyMemberRulePackage()
        {
            DeclaringType = typeof(object);
            _contract = default;
            _rule = default;
        }

        internal VerifyMemberRulePackage(Type declaringType, VerifiableMemberContract contract, CorrectValueRule rule)
        {
            DeclaringType = declaringType ?? throw new ArgumentNullException(nameof(declaringType));
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
            _rule = rule ?? throw new ArgumentNullException(nameof(rule));
        }

        public Type DeclaringType { get; }

        public Type MemberType => _contract?.MemberType;

        public string MemberName => _contract?.MemberName;

        public int Count() => _rule?.Tokens.Count ?? 0;

        public static VerifyMemberRulePackage Empty { get; } = new();

        internal CorrectValueRule ExposeRule() => _rule;
    }
}