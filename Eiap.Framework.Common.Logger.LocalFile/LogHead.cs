using Eiap.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Logger.LocalFile
{
    public class LogHead : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 日志创建时间
        /// </summary>
        public virtual DateTime LogDateTime { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public virtual LogLevel LogLevel { get; set; }

        /// <summary>
        /// 日志来源（应用程序）
        /// </summary>
        public virtual int LogSource { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        public virtual int LogYear { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public virtual int LogMonth { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public virtual int LogDay { get; set; }

        /// <summary>
        /// 时
        /// </summary>
        public virtual int LogHour { get; set; }

        /// <summary>
        /// 分
        /// </summary>
        public virtual int LogMinute { get; set; }

        /// <summary>
        /// 秒
        /// </summary>
        public virtual int LogSecond { get; set; }

        /// <summary>
        /// 毫秒
        /// </summary>
        public virtual int LogMillisecond { get; set; }

        /// <summary>
        /// 服务器Ip
        /// </summary>
        public virtual string ServerIp { get; set; }

        /// <summary>
        /// 日志名称
        /// </summary>
        public virtual string LogName { get; set; }

        /// <summary>
        /// 日志标识（搜索关键字）
        /// </summary>
        public virtual string LogKey { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public virtual string ApplicationName { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModulesName { get; set; }

        /// <summary>
        /// 日志消息索引
        /// </summary>
        public virtual Guid LogBodyKey { get; set; }

    }
}
