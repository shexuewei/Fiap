using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    public interface IInterceptorMethod
    {
        bool Test(InterceptorMethodArgs args);
    }
}
