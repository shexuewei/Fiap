using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    public interface IInterceptorMethod
    {

    }

    public interface IInterceptorMethodBegin: IInterceptorMethod
    {
        void Execute(InterceptorMethodArgs args);
    }

    public interface IInterceptorMethodEnd : IInterceptorMethod
    {
        void Execute(InterceptorMethodArgs args);
    }

    public interface IInterceptorMethodException : IInterceptorMethod
    {
        void Execute(InterceptorMethodArgs args);
    }
}
