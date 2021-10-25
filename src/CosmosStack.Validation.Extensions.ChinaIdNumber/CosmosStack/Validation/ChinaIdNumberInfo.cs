using System;

namespace CosmosStack.Validation
{
    /// <summary>
    /// Info for China Id Card's Number
    /// </summary>
    public class ChinaIdNumberInfo
    {
        /// <summary>
        /// The length of the Number of the Id Card. <br />
        /// 身份证号码长度
        /// </summary>
        public ChinaIdStyles Style { get; internal set; }

        /// <summary>
        /// Birthday on the Id Card's Number <br />
        /// 身份证上的出生日期
        /// </summary>
        public DateTime Birthday { get; internal set; }

        /// <summary>
        /// Gender <br />
        /// 性别
        /// </summary>
        public ChinaIdGender Gender { get; internal set; }

        /// <summary>
        /// Administrative area code <br />
        /// 行政区域编码
        /// </summary>
        public int AreaNumber { get; internal set; }
        
        /// <summary>
        /// <para>
        /// Id Card issuing administrative area. <br />
        /// 身份证办法行政区域
        /// </para>
        /// <para>
        /// Identity the deepest area of Depth. The complete area name can be obtained through FullName. <br />
        /// 识别出 Depth 最深的区域。可以通过 FullName 来获取完整的区域名。
        /// </para>
        /// </summary>
        public ChinaIdAreaInfo RecognizableArea { get; internal set; }
        
        /// <summary>
        /// Birth registration number <br />
        /// 出生顺序登记号
        /// </summary>
        public string Sequence { get; internal set; }
        
        /// <summary>
        /// Id verification code <br />
        /// 身份证校验码
        /// </summary>
        public char CheckBit { get; internal set; }
    }

}