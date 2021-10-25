using System;
using System.Text;

namespace CosmosStack.Validation
{
    /// <summary>
    /// Area info for China Id Card
    /// </summary>
    public class ChinaIdAreaInfo
    {
        public ChinaIdAreaInfo(int number, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (number.ToString().Length % 2 != 0)
                throw new ArgumentException($"The number ('{number}') is not correct.");

            Number = number;
            Name = name.Trim();
        }

        /// <summary>
        /// Administrative Division Code <br />
        /// 行政区划代码
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Administrative Area Name <br />
        /// 行政区划名称）当前节点）
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The full name of the administrative area, include the name of the superior administrative area. <br />
        /// 行政区域的完整名称，包括上级行政区域的名称。
        /// </summary>
        public string FullName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                var area = this;
                while (area is not null)
                {
                    sb.Insert(0, area.Name);
                    area = area.Parent;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// The parent administrative aera
        /// </summary>
        public ChinaIdAreaInfo Parent { get; internal set; }

        /// <summary>
        /// <para>
        /// Get the depth of the current area. <br />
        /// 获取当前区域的深度
        /// </para>
        /// <para>从 1 开始，按照 GB2260 行政区域编码规则，2 位为一个深度。</para>
        /// </summary>
        /// <returns></returns>
        public int GetDepth() => Number.ToString().Length / 2;
    }

    /// <summary>
    /// The restrictions of regional validation level.<br />
    /// 区域验证级别限制
    /// </summary>
    public enum ChinaIdAreaValidLimit
    {
        /// <summary>
        /// <para>
        /// Province <br />
        /// 省
        /// </para>
        /// <para>身份证号码的前 2 位，新中国建立以来，行政区域列表还未对省级做过调整</para>
        /// </summary>
        Province = 1,

        /// <summary>
        /// <para>
        /// City <br />
        /// 市
        /// </para>
        /// <para>身份证号码的前 4 位，新中国建立以来对少数市级区域作过调整</para>
        /// </summary>
        City = 2,

        /// <summary>
        /// <para>
        /// Count <br />
        /// 县
        /// </para>
        /// <para>身份证号码的前 6 位，县级调整是最为频繁的。</para>
        /// </summary>
        Count = 3,
    }
}