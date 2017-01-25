using Eiap.Framework.AppBase.DTOMapper;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.ApplicationService
{
    public interface IAppService : IAppServiceUnitOfWork, IRealtimeDependency
    {
        ILogger Log { get; set; }

        IDTOMapper Mapper { get; set; }
    }
}
