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
        private readonly ICustomValidatorManager _customValidatorManager;

        public TypeProject(Type type, ICustomValidatorManager customValidatorManager)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            _customValidatorManager = customValidatorManager ?? throw new ArgumentNullException(nameof(customValidatorManager));
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

        public VerifyResult Verify(ObjectContext context, ValidationOptions options)
        {
            return CorrectEngine.Valid(
                context, 
                _rules,
                options.CustomValidatorEnabled
                    ? _customValidatorManager.ResolveAll()
                    : _customValidatorManager.ResolveEmpty());
        }

        public VerifyResult VerifyOne(ObjectValueContext context, ValidationOptions options)
        {
            return CorrectEngine.ValidOne(
                context,
                _rules.Where(x => x.MemberName == context.MemberName).ToList(),
                options.CustomValidatorEnabled
                    ? _customValidatorManager.ResolveAll()
                    : _customValidatorManager.ResolveEmpty());
        }

        public VerifyResult VerifyMany(IDictionary<string, ObjectValueContext> keyValueCollections, ValidationOptions options)
        {
            return CorrectEngine.ValidMany(
                keyValueCollections,
                _rules,
                options.CustomValidatorEnabled
                    ? _customValidatorManager.ResolveAll()
                    : _customValidatorManager.ResolveEmpty());
        }
    }
}