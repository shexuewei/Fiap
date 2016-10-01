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
        /// 根据方法名（命名空间、类型）获取用例集合
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public List<UnitTestCaseContainer> GetUnitTestCaseByMethodName(string unitTestNamespace, string className, string methodName)
        {
            return _ContainerManager.GetUnitTestCaseByMethodName(unitTestNamespace, className, methodName);
        }
    }
}
