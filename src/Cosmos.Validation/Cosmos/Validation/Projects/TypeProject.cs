using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Projects
{
    public class TypeProject : IProject
    {
        private readonly List<CorrectValueRule> _rules;

        public TypeProject(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = type.GetFullyQualifiedName();
            _rules = new();
        }

        public string Name { get; }

        public Type Type { get; }

        public ProjectClass Class => ProjectClass.Typed;

        internal void UpdateRules(IEnumerable<CorrectValueRule> rules)
        {
            foreach (var rule in rules) _rules.Add(rule);
        }

        public VerifyResult Verify(VerifiableObjectContext context)
        {
            return CorrectEngine.Valid(context, _rules);
        }

        public VerifyResult VerifyOne(VerifiableMemberContext context)
        {
            return CorrectEngine.ValidOne(context, _rules.Where(x => x.MemberName == context.MemberName).ToList());
        }

        public VerifyResult VerifyMany(IDictionary<string, VerifiableMemberContext> keyValueCollections)
        {
            return CorrectEngine.ValidMany(keyValueCollections, _rules);
        }
    }
}