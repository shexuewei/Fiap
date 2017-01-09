using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Dependency;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestCaseManager : IRealtimeDependency
    {
        /// <summary>
        /// 注册用例
        /// </summary>
        /// <param name="container"></param>
        void RegisterUnitTestCase(UnitTestCaseContainer container);

        /// <summary>
        /// 根据命名空间获取用例集合
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <returns></returns>
        List<UnitTestCaseContainer> GetUnitTestCaseByNamespace(string unitTestNamespace);
    }
}
