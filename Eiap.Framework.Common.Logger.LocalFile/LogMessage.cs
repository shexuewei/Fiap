using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Logger.LocalFile
{
    public class LogMessage
    {
        public virtual LogHead LogHead { get; set; }

        public virtual LogBody LogBody { get; set; }

        public override string ToString()
        {
            return "LogHead:" + LogHead.Id + "\r\n"
                + LogHead.ApplicationName + "\r\n"
                + LogHead.ServerIp + "\r\n"
                + LogHead.LogDateTime.ToString() + "\r\n"
                + LogHead.LogKey + "\r\n"
                + LogHead.LogName + "\r\n"
                + LogBody.LogBodyContent + "\r\n\r\n";
        }
    }
}
