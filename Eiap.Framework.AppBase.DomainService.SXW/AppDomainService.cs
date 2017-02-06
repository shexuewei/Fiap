using Eiap.Framework.AppBase.UnitOfWork;
using Eiap.Framework.Base.Logger;
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
        public ILogger Log { get; set; }

        public ICurrentUnitOfWork CurrentUnitOfWork
        {
            get { return DependencyManager.Instance.Resolver<ICurrentUnitOfWork>(); }
        }
    }
}
