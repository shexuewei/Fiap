﻿using Eiap.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger
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

        /// <summary>
        /// 调用链Id
        /// </summary>
        public virtual Guid TraceId { get; set; }

        /// <summary>
        /// 调用层次关联ID
        /// </summary>
        public virtual int LocalId { get; set; }

        /// <summary>
        /// 调用层次关联父ID
        /// </summary>
        public virtual int ParentId { get; set; }

    }
}
