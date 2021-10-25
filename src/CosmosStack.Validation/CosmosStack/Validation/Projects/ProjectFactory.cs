using System;
using System.Collections.Generic;
using CosmosStack.Validation.Internals.Rules;

// ReSharper disable PossibleMultipleEnumeration

namespace CosmosStack.Validation.Projects
{
    internal static class ProjectFactory
    {
        public static List<IProject> CreateTypeProject(
            Dictionary<Type, List<CorrectValueRule>> rulesDictionary)
        {
            var result = new List<IProject>();

            foreach (var item in rulesDictionary)
            {
                var project = new TypedProject(item.Key);

                project.UpdateRules(item.Value);

                result.Add(project);
            }

            return result;
        }

        public static List<IProject> CreateNamedTypeProject(
            Dictionary<(Type, string), List<CorrectValueRule>> rulesDictionary)
        {
            var result = new List<IProject>();

            foreach (var item in rulesDictionary)
            {
                var project = new NamedTypeProject(item.Key.Item1, item.Key.Item2);

                project.UpdateRules(item.Value);

                result.Add(project);
            }

            return result;
        }
    }
}