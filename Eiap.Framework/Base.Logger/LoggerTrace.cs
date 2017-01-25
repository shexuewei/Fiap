using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger
{
    /// <summary>
    /// 日志追踪
    /// </summary>
    public class LoggerTrace
    {
        public Guid TraceId { get; set; }

        public int LocalId { get; set; }

        public int ParentId { get; set; }
    }
}
