using Eiap.Framework.Base.AssemblyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Dependency.SXW;

namespace Eiap.Framework.Base.Dependency.SXW.Test
{
    public class DependencyTestModule : IComponentModule
    {
        public void Initialize()
        {
            //注册当前程序集
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
