using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.UnitOfWork.SXW
{
    public class CurrentUnitOfWorkManager : ICurrentUnitOfWork
    {
        public IUnitOfWork CurrentUnitOfWork
        {
            get { return DependencyManager.Instance.Resolver<IUnitOfWork>(); }
        }
    }
}
