using Eiap.Framework.Base.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Cache.SXW
{
    public class LocalCacheManagerInterceptorMethodAttibute: InterceptorMethodAttibute, IInterceptorMethod
    {
        public bool Test(InterceptorMethodArgs args)
        {
            Console.WriteLine("test");
            return true;
        }
    }
}
