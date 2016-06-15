using Eiap.Framework.AppBase.UnitOfWork;
using Eiap.Framework.Common.Logger;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Dependency;

namespace Eiap.Framework.AppBase.DomainService.SXW
{
    public class AppDomainService : IDomainService
    {
        private IUnitOfWork _CurrentUnitOfWork;

        public AppDomainService()
        {
            _CurrentUnitOfWork = DependencyManager.Instance.Resolver<IUnitOfWork>(ObjectLifeCycle.Context);
        }

        public ILogger Log { get; set; }

        public IUnitOfWork CurrentUnitOfWork 
        {
            get 
            {
                return _CurrentUnitOfWork;
            }
        }
    }
}
