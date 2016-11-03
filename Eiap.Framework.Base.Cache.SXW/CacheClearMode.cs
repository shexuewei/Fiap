using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Cache.SXW
{
    public enum CacheClearMode
    {
        /// <summary>
        /// 最少使用（次数）
        /// </summary>
        LFU = 0,
        /// <summary>
        /// 最远使用（时间）
        /// </summary>
        LRU = 1
    }
}
