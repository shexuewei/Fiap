using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy
{
    public interface IDynamicProxyManager
    {
        T Create<T>(IDynamicProxyInterceptor interceptorInstance, object objInstance) where T : class;

        object Create(Type interfaceType, IDynamicProxyInterceptor interceptorInstance, object objInstance);
    }
}
