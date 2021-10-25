using CosmosStack.Validation.Annotations;

#if !NETFRAMEWORK && !NETCOREAPP3_1

namespace CosmosValidationUT.Models
{
    public record NiceRecord
    {
        public string Name { get; init; }

        public int Age { get; init; }
    }

    public record NiceRecordWithAnnotation
    {
        [NotWhiteSpace] public string Name { get; init; }

        [NotNegative] public int Age { get; init; }
    }
}

#endif