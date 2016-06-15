using Eiap.Framework.AppBase.BussinessService;
using Eiap.Framework.Common.Logger;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService.SXW
{
    public abstract class BusinessContextManagerBase : IBusinessContextManager
    {
        private readonly IBusinessHandlerContainerManager _businessHandlerContainer;
        private readonly ILogger _logger;

        public BusinessContextManagerBase(IBusinessHandlerContainerManager businessHandlerContainer, ILogger logger)
        {
            _businessHandlerContainer = businessHandlerContainer;
            _logger = logger;
        }

        public virtual void BusinessProcess<TContextManagerImpType>(BusinessContextBase businessContext)
            where TContextManagerImpType : IBusinessContextManager
        {
            try
            {
                _businessHandlerContainer.GetHandlerList(typeof(TContextManagerImpType)).ForEach(m =>
                {
                    IBusinessContextHandler<BusinessContextBase> context = (IBusinessContextHandler<BusinessContextBase>) DependencyManager.Instance.Resolver(m);
                    context.Process(businessContext);

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IBusinessContextManager Register<TContextManagerImpType, THandlerType>()
            where TContextManagerImpType : IBusinessContextManager
            where THandlerType : IBusinessContextHandler
        {
            _businessHandlerContainer.Register(typeof(TContextManagerImpType), typeof(THandlerType));
            return this;
        }

        public IBusinessContextManager Remove<TContextManagerImpType, THandlerType>()
            where TContextManagerImpType : IBusinessContextManager
            where THandlerType : IBusinessContextHandler
        {
            _businessHandlerContainer.Remove(typeof(TContextManagerImpType), typeof(THandlerType));
            return this;
        }
    }
}
