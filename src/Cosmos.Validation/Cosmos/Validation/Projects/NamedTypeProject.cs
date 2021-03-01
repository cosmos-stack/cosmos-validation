using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

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

        public VerifyResult Verify(ObjectContext context)
        {
            return CorrectEngine.Valid(context, _rules);
        }

        public VerifyResult VerifyOne(ObjectValueContext context)
        {
            return CorrectEngine.ValidOne(context, _rules.Where(x => x.MemberName == context.MemberName).ToList());
        }

        public VerifyResult VerifyMany(IDictionary<string, ObjectValueContext> keyValueCollections)
        {
            return CorrectEngine.ValidMany(keyValueCollections, _rules);
        }
    }
}