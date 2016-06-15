using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    public class InterceptorMethodArgs
    {
        public object Instance { get; set; }

        public string MethodName { get; set; }

        public object[] MethodParameters { get; set; }

        public DateTime MethodDateTime { get; set; }

        public Exception MethodException { get; set; }

        public TimeSpan? MethodExecute { get; set; }
    }
}
