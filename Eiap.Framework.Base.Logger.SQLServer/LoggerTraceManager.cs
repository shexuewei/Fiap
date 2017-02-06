using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger.SQLServer
{
    public class LoggerTraceManager : ILoggerTraceManager
    {
        private LoggerTrace _LoggerTrace = null;

        public LoggerTrace GetLogTrace()
        {
            return _LoggerTrace;
        }

        public void SetLogTrace(LoggerTrace logTrace)
        {
            _LoggerTrace = logTrace;
        }
    }
}
