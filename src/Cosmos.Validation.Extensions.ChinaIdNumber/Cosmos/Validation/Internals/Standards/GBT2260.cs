using System.Collections.Generic;

namespace Cosmos.Validation.Internals.Standards
{
    /// <summary>
    /// GBT2260标准，目前分别有以下10个版本：1980,1982,1984,1986,1988,1991,1995,1999,2002,2007 等等
    /// 第一代身份证办理时用的是1984版本……
    /// </summary>
    internal abstract class GBT2260
    {
        public abstract IDictionary<int, string> GetDictionary();
    }
}