using CosmosStack.Validation.Internals.Conditions;
using CosmosStack.Validation.Internals.Tokens;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Rules
{
    internal class CorrectValueRuleNodeState
    {
        private readonly VerifiableMemberContract _contract;

        public CorrectValueRuleNodeState(
            VerifiableMemberContract contract,
            IGroupedValueToken groupedToken,
            IValueToken currentTokenPtr,
            ConditionOps implicitNextOps = ConditionOps.Break
        )
        {
            _contract = contract;

            _rootNode = groupedToken;
            _workingNode = groupedToken;
            _currentTokenPtr = currentTokenPtr;

            _nextOps = implicitNextOps;
        }

        private IGroupedValueToken _rootNode;
        private IGroupedValueToken _workingNode;
        private IValueToken _currentTokenPtr;
        private ConditionOps _nextOps;

        public void NextOps(ConditionOps ops, bool ignoreFixBreakOps, bool tailOps)
        {
            if (!ignoreFixBreakOps && ops == ConditionOps.Break)
            {
                ops = ConditionOps.And;
            }

            if (ConditionOpsHelper.IsHigherPriority(_nextOps, ops))
            {
                DivideNewWorkingNode(ops);
                _nextOps = ops;
                return;
            }

            if (!tailOps)
            {
                _workingNode.Relationship = ops;
            }

            _nextOps = ops;
        }

        public IValueToken CurrentToken
        {
            get => _currentTokenPtr;
            set
            {
                _workingNode.AppendToken(value);
                _currentTokenPtr = value;
            }
        }

        private void DivideNewWorkingNode(ConditionOps ops)
        {
            //生成新的Working Node
            var nextWorkingGroup = new GroupedValueToken(_contract);
            var current = (GroupedValueToken) _workingNode;

            current.Relationship = ops;
            current.NextNode = nextWorkingGroup;

            _workingNode = nextWorkingGroup;
        }

        public IGroupedValueToken ExposeRootNode() => _rootNode;

        public ConditionOps ExposeTopLevelOps() => _rootNode.Relationship;
    }
}