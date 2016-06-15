using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Dependency;

namespace Eiap.Framework.AppBase.BussinessService
{
    public interface IBusinessContextManager : IRealtimeDependency
    {
        IBusinessContextManager Register<TContextManagerImpType, THandlerType>()
            where TContextManagerImpType : IBusinessContextManager
            where THandlerType : IBusinessContextHandler;

        void BusinessProcess<TContextManagerImpType>(BusinessContextBase businessContext)
            where TContextManagerImpType : IBusinessContextManager;

        IBusinessContextManager Remove<TContextManagerImpType, THandlerType>() 
            where TContextManagerImpType : IBusinessContextManager
            where THandlerType : IBusinessContextHandler;
    }
}
