using System.Collections.Generic;
using Cosmos.Validation.Internals.Tokens;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleState
    {
        public CorrectValueRuleState()
        {
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

        public List<IValueToken> ExposeValueTokens() => ValueTokens;
    }
}