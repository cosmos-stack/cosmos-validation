using CosmosStack.Validation.Internals.Conditions;

namespace CosmosStack.Validation.Internals.Rules
{
    internal class LogicCorrectValueRule : CorrectValueRule
    {
        /// <summary>
        /// The internal logic, <br />
        /// True = AND (default) <br />
        /// False = OR
        /// </summary>
        public bool InternalLogic { get; set; } = true;
        
        public override void Merge(CorrectValueRule rule)
        {
            if (rule is LogicCorrectValueRule logicRule && InternalLogic != logicRule.InternalLogic)
                return;
            base.Merge(rule);
        }

        public override void Verify(VerifiableOpsContext context)
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