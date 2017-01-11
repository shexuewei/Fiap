using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Dependency;

namespace Eiap.Framework.Base.UnitTest
{
    public interface IUnitTestResultManager : IRealtimeDependency
    {
        /// <summary>
        /// 添加单元测试结果
        /// </summary>
        /// <param name="container"></param>
        void AddUnitTestResult(UnitTestResultContainer container);

        /// <summary>
        /// 根据命名空间输出单元测试结果
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        void PrintUnitTestResultByNamespace(string unitTestNamespace);
    }
}
