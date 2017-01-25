using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Logger.LocalFile
{
    public interface ILoggerConfigeration : IRealtimeDependency, IPropertyDependency, IDynamicProxyDisable
    {
        string LogPathFormat { get; }

        long LogSize { get; }
    }
}
