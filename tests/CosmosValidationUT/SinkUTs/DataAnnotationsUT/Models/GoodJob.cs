using System.ComponentModel.DataAnnotations;

namespace CosmosValidationUT.SinkUTs.DataAnnotationsUT.Models
{
    public class GoodJob
    {
        [Required] [MaxLength(10)] public string Name { get; set; }

        [Range(10, 20)] public int Cost { get; set; }
    }
}