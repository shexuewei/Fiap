using Eiap.Framework.AppBase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.Repository
{
    public interface IRepositoryUnitOfWork
    {
        ICurrentUnitOfWork CurrentUnitOfWork { get; }
    }
}
