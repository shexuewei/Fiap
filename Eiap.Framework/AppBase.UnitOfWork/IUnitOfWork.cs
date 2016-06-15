using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.UnitOfWork
{
    public interface IUnitOfWork : IContextDependency, IDynamicProxyDisable
    {
        void SetRepository(IUnitOfWorkCommandConnection res);

        void Commit(bool IsTransaction = false);
    }
}
