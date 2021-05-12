using System;
using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Internals.Rules;
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

        public IValidator AsValidator() => new VerifyRulePackageValidator(this);

        public IValidator AsValidator(ValidationOptions options) => new VerifyRulePackageValidator(this, options);
    }

    // public sealed class VerifyMemberRulePackage
    // {
    //     private readonly CorrectValueRule _rule;
    //     private readonly VerifiableMemberContract _contract;
    //
    //     private VerifyMemberRulePackage()
    //     {
    //         DeclaringType = typeof(object);
    //         _contract = default;
    //         _rule = default;
    //     }
    //
    //     internal VerifyMemberRulePackage(Type declaringType, VerifiableMemberContract contract, CorrectValueRule rule)
    //     {
    //         DeclaringType = declaringType ?? throw new ArgumentNullException(nameof(declaringType));
    //         _contract = contract ?? throw new ArgumentNullException(nameof(contract));
    //         _rule = rule ?? throw new ArgumentNullException(nameof(rule));
    //     }
    //
    //     public Type DeclaringType { get; }
    //
    //     public Type MemberType => _contract?.MemberType;
    //
    //     public string MemberName => _contract?.MemberName;
    //
    //     public int Count() => _rule?.Tokens.Count ?? 0;
    //
    //     public static VerifyMemberRulePackage Empty { get; } = new();
    // }
}