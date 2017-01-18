using Eiap.Framework.Base.AssemblyService;
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
        public void Initialize()
        {
            //注册当前程序集
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
