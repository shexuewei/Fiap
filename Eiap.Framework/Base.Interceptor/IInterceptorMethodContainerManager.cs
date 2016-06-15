using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    public interface IInterceptorMethodContainerManager : ISingletonDependency
    {
        void RegisterAttibuteAndInterceptorMethod(Type interceptorMethodAttibute, Func<InterceptorMethodArgs, bool> interceptorMethod);

        InterceptorMethodContainer GetInterceptorMethodContainer(Type interceptorMethodAttibute);
    }
}
