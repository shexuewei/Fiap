using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class InterceptorMethodAttibute : Attribute, IInterceptorMethod
    {
        public virtual bool Execute(InterceptorMethodArgs args)
        {
            return true;
        }
    }
}
