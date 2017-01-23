using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Cache.SXW;
using Eiap.Framework.Base.Dependency.SXW;
using Eiap.Framework.Base.Interceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Cache.SXW
{
    public class LocalCacheManagerModule : IComponentModule
    {
        public void AssemblyInitialize()
        {
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }

        public void RegisterInitialize()
        {
            
        }
    }
}
