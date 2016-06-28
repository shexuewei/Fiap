using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestManager : IUnitTestManager
    {
        private readonly IUnitTestContainerManager _UnitTestContainerManager;

        public UnitTestManager(IUnitTestContainerManager unitTestContainerManager)
        {
            _UnitTestContainerManager = unitTestContainerManager;
        }

        /// <summary>
        /// 注册单元测试接口、方法和测试用例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="unitTestCase"></param>
        /// <returns></returns>
        public IUnitTestManager Register<T>(string methodName, UnitTestCaseContainer unitTestCase)
        {
            _UnitTestContainerManager.RegisterUnitTestInterface(typeof(T), methodName, unitTestCase);
            return this;
        }
    }
}
