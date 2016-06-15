using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Common.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.DomainService
{
    public interface IDomainService : IDomainServiceUnitOfWork, IRealtimeDependency
    {
        ILogger Log { get; set; }
    }
}
