using Eiap.Framework.Common.DataMapping.SQL;
using Eiap.Framework.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger
{
    public class LogBody : IEntity<Guid>
    {
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 日志消息内容
        /// </summary>
        public virtual string LogBodyContent { get; set; }

    }
}
