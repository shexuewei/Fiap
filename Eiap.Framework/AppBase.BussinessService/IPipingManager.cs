using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService
{
    public interface IPipingManager : IRealtimeDependency
    {
        IPipingManager RegisterPiping<TPipingImpType, TBusinessContextManagerImpType>()
            where TPipingImpType : IPiping
            where TBusinessContextManagerImpType : IBusinessContextManager;

        IBusinessContextManager GetPipingBusinessContextManager<TPipingImpType>() where TPipingImpType : IPiping;
    }
}
