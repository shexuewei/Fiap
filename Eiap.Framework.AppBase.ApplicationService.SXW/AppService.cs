using Eiap.Framework.AppBase.DTOMapper;
using Eiap.Framework.AppBase.UnitOfWork;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Dependency.SXW;
using Eiap.Framework.Common.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.ApplicationService.SXW
{
    public class AppService : IAppService
    {
        public ILogger Log { get; set; }

        public IDTOMapper Mapper { get; set; }

        public ICurrentUnitOfWork CurrentUnitOfWork
        {
            get { return DependencyManager.Instance.Resolver<ICurrentUnitOfWork>(); }
        }
    }
}
