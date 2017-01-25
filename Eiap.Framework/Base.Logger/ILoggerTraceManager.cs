using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger
{
    public interface ILoggerTraceManager : IContextDependency, IDynamicProxyDisable
    {
        void SetLogTrace(LoggerTrace logTrace);

        LoggerTrace GetLogTrace();
    }
}
