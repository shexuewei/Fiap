using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Dependency;

namespace Eiap.Framework.AppBase.BussinessService
{
    public interface IBusinessContextHandler<in TContextBase> : IBusinessContextHandler, IRealtimeDependency
        where TContextBase : BusinessContextBase
    {
        void Process(TContextBase contextBase);
    }

    public interface IBusinessContextHandler
    { }
}
