using Eiap.Framework.Base.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest
{
    /// <summary>
    /// 单元测试用例容器管理
    /// </summary>
    public interface IUnitTestCaseContainerManager : ISingletonDependency
    {
        /// <summary>
        /// 注册用例
        /// </summary>
        /// <param name="container"></param>
        void RegisterUnitTestCase(UnitTestCaseContainer container);

        /// <summary>
        /// 根据方法名（命名空间、类型）获取用例集合
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        List<UnitTestCaseContainer> GetUnitTestCaseByMethodName(string unitTestNamespace, string className, string methodName);
    }
}
