using Eiap.Framework.Base.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.ApplicationService
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public class UnitOfWorkCommitAttribute : InterceptorMethodEndAttibute
    {
        public UnitOfWorkCommitAttribute()
        {
            this.Priority = -1;
        }


        public override void Execute(InterceptorMethodArgs args)
        {
            IAppService appinstance = args.InstanceObject as IAppService;
            appinstance.CurrentUnitOfWork.CurrentUnitOfWork.Commit();
        }
    }
}
