using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestManager : IRealtimeDependency
    {
        /// <summary>
        /// 根据程序集集合，注册用例
        /// </summary>
        /// <param name="assemblyList"></param>
        void Register(List<Assembly> assemblyList);
    }
}
