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
    public interface IUnitTestResultContainerManager : ISingletonDependency
    {
        /// <summary>
        /// 添加单元测试结果
        /// </summary>
        /// <param name="container"></param>
        void AddUnitTestResult(UnitTestResultContainer container);

        /// <summary>
        /// 根据命名空间获取测试结果
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <returns></returns>
        List<UnitTestResultContainer> GetUnitTestResultByNamespace(string unitTestNamespace);
    }
}
