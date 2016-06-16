using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy
{
    public interface IDynamicProxyInterceptor : IRealtimeDependency, IDynamicProxyDisable
    {
        object Invoke(object instance, string name, object[] parameters);
    }
}
