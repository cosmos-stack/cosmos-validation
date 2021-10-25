namespace CosmosStack.Validation
{
    /// <summary>
    /// The length of China Id Card's Number
    /// </summary>
    public enum ChinaIdStyles
    {
        /// <summary>
        /// 中国大陆身份证（15位）
        /// </summary>
        Id15 = 15,

        /// <summary>
        /// 中国大陆身份证（18位）
        /// </summary>
        Id18 = 18,

        /// <summary>
        /// 中华人民共和国澳门特别行政区身份证，1 + 6 + 1 位
        /// </summary>
        Macau = 108,

        /// <summary>
        /// 中华人民共和国香港特别行政区身份证， 1 + 6 + 1 位
        /// </summary>
        HkId03 = 110,

        /// <summary>
        /// 中华人民共和国台湾省身份证，1 + 9 位（当前为台湾伪政权身份证）
        /// </summary>
        Taiwan = 112,
    }
}