using System.Collections.Generic;
using Cosmos.Validation.Internals.Conditions;
using Cosmos.Validation.Internals.Tokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleState
    {
        private readonly VerifiableMemberContract _contract;

        public CorrectValueRuleState(VerifiableMemberContract contract)
        {
            _contract = contract;
            ValueTokens = new List<IValueToken>();
        }

        private List<IValueToken> ValueTokens { get; set; }

        private IValueToken _currentTokenPtr;

        public IValueToken CurrentToken
        {
            get => _currentTokenPtr;
            set
            {
                if (value is not null)
                {
                    _currentTokenPtr = value;
                    ValueTokens.Add(value);
                }
            }
        }

        public void ClearCurrentToken()
        {
            _currentTokenPtr = null;
        }

        private CorrectValueRuleNodeState NodeState { get; set; }

        public void GroupWithBreakOps() => Group(ConditionOps.Break, true);

        public void GroupWithAndOps() => Group(ConditionOps.And);

        public void GroupWithOrOps() => Group(ConditionOps.Or);

        private void Group(ConditionOps nextOps, bool ignoreFixBreakOps = false)
        {
            if (NodeState is null)
                NodeState = new CorrectValueRuleNodeState(_contract, ValueTokens, nextOps);
            else
                NodeState.ArchiveCurrentTokenAsGroup(ValueTokens, nextOps, ignoreFixBreakOps);

            ClearCurrentToken();
        }

        public List<IValueToken> ExposeValueTokens()
        {
            if (NodeState is null)
                return ValueTokens;
            if (_currentTokenPtr is not null)
                GroupWithBreakOps();
            
            return new List<IValueToken> {NodeState.ExposeRootNode()};
        }
    }
}