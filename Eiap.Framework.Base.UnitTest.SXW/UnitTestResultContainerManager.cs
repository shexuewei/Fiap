using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestResultContainerManager : IUnitTestResultContainerManager
    {
        private List<UnitTestResultContainer> _UnitTestResultContainerList = null;

        public UnitTestResultContainerManager()
        {
            _UnitTestResultContainerList = new List<UnitTestResultContainer>();
        }

        /// <summary>
        /// 添加单元测试结果
        /// </summary>
        /// <param name="container"></param>
        public void AddUnitTestResult(UnitTestResultContainer container)
        {
            _UnitTestResultContainerList.Add(container);
        }

        /// <summary>
        /// 根据命名空间获取测试结果
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <returns></returns>
        public List<UnitTestResultContainer> GetUnitTestResultByNamespace(string unitTestNamespace)
        {
            return _UnitTestResultContainerList.Where(m => m.UnitTestNamespace == unitTestNamespace).ToList();
        }
    }
}
