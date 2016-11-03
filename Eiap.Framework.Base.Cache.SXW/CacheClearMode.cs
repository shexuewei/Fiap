using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Cache.SXW
{
    public enum CacheClearMode
    {
        FIFO = 0,//先进先出
        LFU = 1,//最近最少使用（次数）
        LRU = 2//最近最少使用（时间）
    }
}
