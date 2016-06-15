using Eiap.Framework.AppBase.BussinessService;
using Eiap.Framework.Common.Logger;
using Eiap.Framework.Base.Dependency.SXW;
using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService.SXW
{
    public class PipingManager :IPipingManager, ISingletonDependency
    {
        private readonly IPipingContainerManager _pipingContainerManager;

        public PipingManager(IPipingContainerManager pipingContainerManager)
        {
            _pipingContainerManager = pipingContainerManager;
        }

        public IPipingManager RegisterPiping<TPipingImpType, TBusinessContextManagerImpType>()
            where TPipingImpType : IPiping
            where TBusinessContextManagerImpType : IBusinessContextManager
        {
            _pipingContainerManager.RegisterPiping(typeof(TPipingImpType), typeof(TBusinessContextManagerImpType));
            return this;
        }

        public IBusinessContextManager GetPipingBusinessContextManager<TPipingImpType>() where TPipingImpType : IPiping
        {
            var pipingcontainer = _pipingContainerManager.GetPipingContainer(typeof(TPipingImpType));
            if (pipingcontainer != null)
            {
                return (IBusinessContextManager)DependencyManager.Instance.Resolver(pipingcontainer.BusinessContextManagerImpType);
            }
            else
            {
                throw new Exception("No Piping!");
            }
        }
    }
}
