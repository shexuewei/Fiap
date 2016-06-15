using Eiap.Framework.AppBase.BussinessService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.AppBase.BussinessService.SXW
{
    public abstract class BusinessContextHandlerBase<TContextBase> : IBusinessContextHandler<TContextBase>
        where TContextBase : BusinessContextBase
    {
        public virtual void Process(TContextBase contextBase)
        { }
    }
}
