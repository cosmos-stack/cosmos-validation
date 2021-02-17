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
        private readonly IEnumerable<CustomValidator> _validators;

        public TypeProject(Type type, IEnumerable<CustomValidator> validators)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = type.GetFullyQualifiedName();
            _rules = new();
            _validators = validators;
        }

        public string Name { get; }

        public Type Type { get; }

        public ProjectClass Class => ProjectClass.Typed;

        internal void UpdateRules(IEnumerable<CorrectValueRule> rules)
        {
            foreach (var rule in rules) _rules.Add(rule);
        }

        public VerifyResult Verify(ObjectContext context)
        {
            return CorrectEngine.Valid(context, _rules, _validators);
        }

        public VerifyResult VerifyOne(ObjectValueContext context)
        {
            return CorrectEngine.ValidOne(context, _rules.Where(x => x.MemberName == context.MemberName).ToList(), _validators);
        }
    }
}