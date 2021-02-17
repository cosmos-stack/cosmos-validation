namespace Cosmos.Validation.Objects
{
    public enum ObjectValueKind
    {
        Property,
        Field,
        Unknown,
        CustomContract
    }

    internal static class ObjectValueKindExtensions
    {
        public static bool BasicTypeState(this ObjectValueContract contract)
        {
            if (contract is null)
                return false;

            switch (contract.ObjectValueKind)
            {
                case ObjectValueKind.CustomContract:
                    return contract.ExposeInternalImpl().IsBasicType;
                
                case ObjectValueKind.Unknown:
                case ObjectValueKind.Field:
                case ObjectValueKind.Property:
                    return contract.IsBasicType;
                
                default:
                    return false;
            }
        }
        
        public static bool BasicTypeState(this ObjectValueContext context)
        {
            if (context is null)
                return false;

            switch (context.ObjectValueKind)
            {
                case ObjectValueKind.CustomContract:
                    return context.ExposeInternalImpl().IsBasicType;
                
                case ObjectValueKind.Unknown:
                case ObjectValueKind.Field:
                case ObjectValueKind.Property:
                    return context.IsBasicType;
                
                default:
                    return false;
            }
        }
    }
}