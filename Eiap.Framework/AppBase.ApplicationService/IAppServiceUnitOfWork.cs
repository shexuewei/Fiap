using Eiap.Framework.AppBase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.ApplicationService
{
    public interface IAppServiceUnitOfWork
    {
        ICurrentUnitOfWork CurrentUnitOfWork { get; }
    }
}
