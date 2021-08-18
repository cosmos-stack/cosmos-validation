namespace Cosmos.Validation.Validators
{
    public class ChinaIdNumberValidationOptions
    {
        public ChinaIdNumberValidationOptions() { }

        public ChinaIdNumberValidationOptions(ChinaIdStyles styles)
        {
            Styles = styles;
        }

        public ChinaIdNumberValidationOptions(ChinaIdStyles styles, string paramName)
        {
            Styles = styles;
            ParamName = paramName;
        }

        public ChinaIdStyles Styles { get; set; }

        public int MinYear { get; set; } = 0;

        public ChinaIdAreaValidLimit Limit { get; set; } = ChinaIdAreaValidLimit.Province;

        public bool IgnoreCheckBit { get; set; } = false;

        internal string ParamName { get; set; } = "Instance";

        internal static ChinaIdNumberValidationOptions Default { get; } = new();
    }
}