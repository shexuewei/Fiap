using Eiap.Framework.Base.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Cache.SXW
{
    public class LocalCacheManagerInterceptorMethodAttibute: InterceptorMethodAttibute
    {
        public override bool Execute(InterceptorMethodArgs args)
        {
            Console.WriteLine("test");
            return true;
        }
    }
}
