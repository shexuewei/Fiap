using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor.SXW
{
    public class FrameworkLogInterceptorMethod
    {
        public bool Test(InterceptorMethodArgs args)
        {
            Console.WriteLine("test");
            return true;
        }
    }
}
