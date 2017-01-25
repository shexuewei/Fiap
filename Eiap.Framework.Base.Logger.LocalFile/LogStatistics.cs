using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger.LocalFile
{
    public class LogStatistics
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public virtual string ApplicationName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModulesName { get; set; }

        /// <summary>
        /// Debug数
        /// </summary>
        public virtual int DebugCount { get; set; }

        /// <summary>
        /// Error数
        /// </summary>
        public virtual int ErrorCount { get; set; }

        /// <summary>
        /// Fatal数
        /// </summary>
        public virtual int FatalCount { get; set; }

        /// <summary>
        /// Info数
        /// </summary>
        public virtual int InfoCount { get; set; }

        /// <summary>
        /// Warn数
        /// </summary>
        public virtual int WarnCount { get; set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public virtual DateTime LogStatisticsDateTime { get; set; }
    }
}
