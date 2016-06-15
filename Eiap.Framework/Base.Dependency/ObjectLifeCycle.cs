using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Dependency
{
    public class ObjectLifeCycle
    {
        /// <summary>
        /// 单例
        /// </summary>
        public const int Singleton = 0;

        /// <summary>
        /// 实时创建
        /// </summary>
        public const int Realtime = 1;

        /// <summary>
        /// 上下文
        /// </summary>
        public const int Context = 2;
    }
}
