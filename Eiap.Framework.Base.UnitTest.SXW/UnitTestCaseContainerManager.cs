using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestCaseContainerManager : IUnitTestCaseContainerManager
    {
        private List<UnitTestCaseContainer> _UnitTestCaseContainerList = null;

        public UnitTestCaseContainerManager()
        {
            _UnitTestCaseContainerList = new List<UnitTestCaseContainer>();
        }

        /// <summary>
        /// 注册单元测试用例
        /// </summary>
        /// <param name="container"></param>
        public void RegisterUnitTestCase(UnitTestCaseContainer container)
        {
            if (!IsExistSameUnitTestCase(container.UnitTestNamespace, container.ClassName, container.MethodName))
            {
                _UnitTestCaseContainerList.Add(container);
            }
        }

        /// <summary>
        /// 根据命名空间获取用例集合
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <returns></returns>
        public List<UnitTestCaseContainer> GetUnitTestCaseByNamespace(string unitTestNamespace)
        {
            return _UnitTestCaseContainerList.Where(m => m.UnitTestNamespace == unitTestNamespace).ToList();
        }

        /// <summary>
        /// 判断是否重复注册
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private bool IsExistSameUnitTestCase(string unitTestNamespace, string className, string methodName)
        {
            return _UnitTestCaseContainerList.Any(m => m.UnitTestNamespace == unitTestNamespace && m.ClassName == className && m.MethodName == methodName);
        }
    }
}
