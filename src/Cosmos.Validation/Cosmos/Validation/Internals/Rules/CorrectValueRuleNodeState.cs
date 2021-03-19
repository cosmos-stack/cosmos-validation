using System.Collections.Generic;
using Cosmos.Validation.Internals.Conditions;
using Cosmos.Validation.Internals.Tokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleNodeState
    {
        private readonly VerifiableMemberContract _contract;

        public CorrectValueRuleNodeState(VerifiableMemberContract contract, List<IValueToken> tokens, ConditionOps nextOps)
        {
            _contract = contract;

            var token = CopyAndGroup(_contract, tokens);

            _rootNode = token;
            _currentNode = token;
            _groupedNodeCounter = 1;
            _lastOps = ConditionOps.Break;
            _nextOps = nextOps == ConditionOps.Break ? ConditionOps.And : nextOps;
        }

        private IGroupedValueToken _rootNode;
        private IGroupedValueToken _currentNode;
        private int _groupedNodeCounter;
        private ConditionOps _lastOps;
        private ConditionOps _nextOps;

        public void ArchiveCurrentTokenAsGroup(List<IValueToken> tokens, ConditionOps nextOps, bool ignoreFixBreakOps = false)
        {
            if (!ignoreFixBreakOps && nextOps == ConditionOps.Break)
                nextOps = ConditionOps.And;

            if (ConditionOpsHelper.IsHigherPriority(_nextOps, nextOps))
                GroupCurrentNodes();

            var token = CopyAndGroup(_contract, tokens);
            var current = (GroupedValueToken) _currentNode;

            current.OpsForNext = _nextOps;
            current.NextNode = token;
            _currentNode = token;
            _lastOps = _nextOps;
            _nextOps = nextOps;
            ++_groupedNodeCounter;
        }

        private void GroupCurrentNodes()
        {
            var left = new GroupedValueToken(_contract, new List<IValueToken> {_rootNode});
            _rootNode = left;
            _currentNode = left;
            _groupedNodeCounter = 1;
        }

        public IGroupedValueToken ExposeRootNode() => _rootNode;

        private static IGroupedValueToken CopyAndGroup(VerifiableMemberContract contract, List<IValueToken> tokens)
        {
            var list = new List<IValueToken>();
            list.AddRange(tokens);
            var group = new GroupedValueToken(contract, list);

            return group;
        }
    }
}