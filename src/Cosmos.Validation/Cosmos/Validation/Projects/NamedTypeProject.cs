﻿using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Projects
{
    public class NamedTypeProject : IProject
    {
        private readonly List<CorrectValueRule> _rules;
        private readonly IEnumerable<CustomValidator> _validators;

        public NamedTypeProject(Type type, string name, IEnumerable<CustomValidator> validators)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = name;
            _rules = new();
            _validators = validators;
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
            return CorrectEngine.Valid(context, _rules, _validators);
        }
    }
}