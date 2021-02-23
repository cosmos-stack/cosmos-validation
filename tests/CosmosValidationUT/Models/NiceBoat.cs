using System;
using Cosmos.Validation.Annotations;

namespace CosmosValidationUT.Models
{
    public class NiceBoat
    {
        [NotWhiteSpace] public string Name { get; set; }

        [NotNegative] public long Length { get; set; }

        [NotNegative] public long Width { get; set; }

        [ValidDateValue]
        public DateTime CreateTime;

        [ValidEmailValue] public string Email { get; set; }
    }
}