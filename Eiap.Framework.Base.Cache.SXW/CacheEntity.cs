using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Cache.SXW
{
    public class CacheEntity
    {
        /// <summary>
        /// 缓存值
        /// </summary>
        public object CacheValue { get; set; }

        public TimeSpan CacheVersion { get; set; }

        public DateTime? AbsoluteExpiration { get; set; }

        public int? SlidingExpiration { get; set; }

    }
}
