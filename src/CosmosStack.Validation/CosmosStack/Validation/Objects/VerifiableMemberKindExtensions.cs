namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable member kind extensions <br />
    /// 可验证成员种类扩展
    /// </summary>
    internal static class VerifiableMemberKindExtensions
    {
        public static bool BasicTypeState(this VerifiableMemberContract contract)
        {
            if (contract is null)
                return false;

            switch (contract.MemberKind)
            {
                case VerifiableMemberKind.CustomContract:
                    return contract.ExposeInternalImpl().IsBasicType;

                case VerifiableMemberKind.Unknown:
                case VerifiableMemberKind.Field:
                case VerifiableMemberKind.Property:
                    return contract.IsBasicType;

                default:
                    return false;
            }
        }

        public static bool BasicTypeState(this VerifiableMemberContext context)
        {
            if (context is null)
                return false;

            switch (context.MemberKind)
            {
                case VerifiableMemberKind.CustomContract:
                    return context.ExposeInternalImpl().IsBasicType;

                case VerifiableMemberKind.Unknown:
                case VerifiableMemberKind.Field:
                case VerifiableMemberKind.Property:
                    return context.IsBasicType;

                default:
                    return false;
            }
        }
    }
}