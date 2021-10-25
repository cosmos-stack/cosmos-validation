﻿using CosmosStack.Validation.Internals.Conditions;

namespace CosmosStack.Validation.Internals.Tokens
{
    /// <summary>
    /// Interface of grouped value token
    /// </summary>
    internal interface IGroupedValueToken : IValueToken
    {
        string MemberName { get; }

        ConditionOps Relationship { get; set; }

        void Next(VerifiableOpsContext context, out bool valid);

        void AppendToken(IValueToken token);
    }
}