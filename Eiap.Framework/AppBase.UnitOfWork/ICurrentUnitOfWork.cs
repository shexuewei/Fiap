using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.UnitOfWork
{
    public interface ICurrentUnitOfWork : IRealtimeDependency, IDynamicProxyDisable
    {
        IUnitOfWork CurrentUnitOfWork { get; }
    }
}
