using System.Collections.Generic;
using Cosmos.Validation.Internals.Conditions;
using Cosmos.Validation.Internals.Tokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRule
    {
        public string MemberName { get; set; }

        public VerifiableMemberContract Contract { get; set; }

        public CorrectValueRuleMode Mode { get; set; }

        public List<IValueToken> Tokens { get; set; }

        /// <summary>
        /// The internal logic, <br />
        /// True = AND (default) <br />
        /// False = OR
        /// </summary>
        public bool InternalLogic { get; set; } = true;

        public void Merge(CorrectValueRule rule)
        {
            if (rule is null)
                return;

            if (MemberName != rule.MemberName)
                return;

            if (rule.Mode != CorrectValueRuleMode.Append)
                return;

            foreach (var token in rule.Tokens)
            {
                if (token.MutuallyExclusive)
                    continue;

                if (!TokenMutexCalculator.Available(Tokens, token))
                    continue;

                Tokens.Add(token);
            }
        }

        public void Verify(VerifiableOpsContext context)
        {
            for (var i = 0; i < Tokens.Count; i++)
            {
                var token = Tokens[i];

                // 验证
                // 如果验证结果为 true， InternalLogic 为 false (OR)， 则 break 并返回
                // 如果验证结果为 false， InternalLogic 为 true (AND)， 应继续执行， 以获得完整的错误信息
                if (token.Verify(context) && !InternalLogic)
                    break;

                // 如果遇到 OR， 提升作用域
                // 如果当前 token 是最后一个， 则不提升 Scope， 避免空 CorrectVerifyValBlock 干扰最终结果
                if (!InternalLogic && i < Tokens.Count - 1)
                    context.RaiseScope(MemberName, InternalLogic ? ConditionOps.And : ConditionOps.Or);
            }
        }
    }
}