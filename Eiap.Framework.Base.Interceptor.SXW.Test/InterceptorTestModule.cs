using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Cache.SXW;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Interceptor.SXW.Test
{
    public class InterceptorTestModule : IComponentModule
    {
        public void AssemblyInitialize()
        {
            //注册当前程序集
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }

        public void RegisterInitialize()
        {
            IInterceptorMethodManager interceptorMethodManager = DependencyManager.Instance.Resolver<IInterceptorMethodManager>();
            interceptorMethodManager.RegisterAttibuteAndInterceptorMethod(typeof(LocalCacheManagerInterceptorMethodAttibute));
        }
    }
}
