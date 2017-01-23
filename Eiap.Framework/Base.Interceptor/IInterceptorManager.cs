using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor
{
    public interface IInterceptorManager: IRealtimeDependency, IDynamicProxyDisable
    {
        /// <summary>
        /// 根据程序集集合，注册拦截器
        /// </summary>
        /// <param name="assemblyList"></param>
        void Register(List<Assembly> assemblyList);
    }
}
