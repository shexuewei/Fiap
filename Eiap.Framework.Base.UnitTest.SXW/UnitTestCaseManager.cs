using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestCaseManager : IUnitTestCaseManager
    {
        private readonly IUnitTestCaseContainerManager _ContainerManager;

        public UnitTestCaseManager(IUnitTestCaseContainerManager unitTestCaseContainerManager)
        {
            _ContainerManager = unitTestCaseContainerManager;
        }

        /// <summary>
        /// 注册单元测试用例
        /// </summary>
        /// <param name="container"></param>
        public void RegisterUnitTestCase(UnitTestCaseContainer container)
        {
            _ContainerManager.RegisterUnitTestCase(container);
        }

        /// <summary>
        /// 根据命名空间获取用例集合
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <returns></returns>
        public List<UnitTestCaseContainer> GetUnitTestCaseByNamespace(string unitTestNamespace)
        {
            return _ContainerManager.GetUnitTestCaseByNamespace(unitTestNamespace);
        }
    }
}
