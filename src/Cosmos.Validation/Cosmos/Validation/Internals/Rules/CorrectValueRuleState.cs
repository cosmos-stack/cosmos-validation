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
                if (value is null)
                    return;

                if (NodeState is null)
                    ValueTokens.Add(value);
                else
                    NodeState.CurrentToken = value;

                _currentTokenPtr = value;
            }
        }

        public void ClearCurrentToken()
        {
            _currentTokenPtr = null;
        }

        private void ClearValueTokens()
        {
            ValueTokens.Clear();
        }

        private CorrectValueRuleNodeState NodeState { get; set; }

        public void MakeBreakOps() => MakeOps(ConditionOps.Break, true, true);

        public void MakeAndOps() => MakeOps(ConditionOps.And);

        public void MakeOrOps() => MakeOps(ConditionOps.Or);

        private void MakeOps(ConditionOps thisOps, bool ignoreFixBreakOps = false, bool tailOps = false)
        {
            if (NodeState is null)
                Shift(thisOps);
            NodeState.NextOps(thisOps, ignoreFixBreakOps, tailOps);
        }

        private void Shift(ConditionOps thisOps)
        {
            var group = new GroupedValueToken(_contract, ValueTokens);

            // ValueTokens.Count > 1  <---- 隐式 AND
            // ValueTokens.Count = 1  <---- 显式 Ops

            ConditionOps implicitNextOps;

            if (ValueTokens.Count == 1)
            {
                group.LogicFlag = thisOps != ConditionOps.Or;
                group.Relationship = thisOps;
                implicitNextOps = ConditionOps.Break;
            }
            else
            {
                implicitNextOps = ConditionOps.And;
            }

            NodeState = new(_contract, group, _currentTokenPtr, implicitNextOps);

            ClearValueTokens();
        }

        private void BreakOfRelationship(List<IValueToken> independentTokens)
        {
            var node = (GroupedValueToken) NodeState.ExposeRootNode();

            while (node is not null)
            {
                independentTokens.Add(node);
                var next = (GroupedValueToken) node.NextNode;
                node.NextNode = null;
                node = next;
            }
        }

        public List<IValueToken> ExposeValueTokens(out ConditionOps lastOps)
        {
            lastOps = ConditionOps.Break;

            if (NodeState is null)
                return ValueTokens;
            if (_currentTokenPtr is not null)
                MakeBreakOps();

            ClearCurrentToken();
            ClearValueTokens();

            lastOps = NodeState.ExposeTopLevelOps();

            var result = new List<IValueToken>();

            BreakOfRelationship(result);

            return result;
        }
    }
}