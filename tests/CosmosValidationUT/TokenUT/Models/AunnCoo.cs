using System;
using System.Collections.Generic;

namespace CosmosValidationUT.TokenUT.Models
{
    public class AunnCoo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char C { get; set; }
        public DateTime D { get; set; }
        public List<string> Tags { get; set; }
        public string[] Career { get; set; }
        public object OtherInfo { get; set; }
        public AunnEnum AunnClass { get; set; }
        public Type AunnType { get; set; }
        public string AunnRegexExpression { get; set; }
        public decimal Discount { get; set; }
        public decimal? NullableDiscount { get; set; }

        public bool IsThisOk { get; set; }

        public bool? IsThatOk { get; set; }

        public Guid ThisGuid { get; set; }

        public Guid? ThatGuid { get; set; }
    }
}