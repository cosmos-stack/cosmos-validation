using System;
using System.Collections.Generic;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Validators;

// ReSharper disable PossibleMultipleEnumeration

namespace Cosmos.Validation.Projects
{
    internal static class ProjectFactory
    {
        public static List<IProject> CreateTypeProject(
            Dictionary<Type, List<CorrectValueRule>> rulesDictionary,
            CustomValidatorManager customValidatorManager)
        {
            var result = new List<IProject>();

            foreach (var item in rulesDictionary)
            {
                var project = new TypeProject(item.Key, customValidatorManager);

                project.UpdateRules(item.Value);

                result.Add(project);
            }

            return result;
        }

        public static List<IProject> CreateNamedTypeProject(
            Dictionary<(Type, string), List<CorrectValueRule>> rulesDictionary,
            CustomValidatorManager customValidatorManager)
        {
            var result = new List<IProject>();

            foreach (var item in rulesDictionary)
            {
                var project = new NamedTypeProject(item.Key.Item1, item.Key.Item2, customValidatorManager);

                project.UpdateRules(item.Value);

                result.Add(project);
            }

            return result;
        }
    }
}