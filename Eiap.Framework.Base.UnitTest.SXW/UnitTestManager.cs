using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestManager : IUnitTestManager
    {
        private readonly IUnitTestContainerManager _UnitTestContainerManager;
        private readonly IUnitTestMethodContainerManager _UnitTestMethodContainerManager;

        public UnitTestManager(IUnitTestContainerManager unitTestContainerManager, IUnitTestMethodContainerManager unitTestMethodContainerManager)
        {
            _UnitTestContainerManager = unitTestContainerManager;
            _UnitTestMethodContainerManager = unitTestMethodContainerManager;
        }

        /// <summary>
        /// 注册单元测试接口、方法和测试用例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="unitTestCase"></param>
        /// <returns></returns>
        public IUnitTestManager Register(Type interfaceType, string methodName, UnitTestCaseContainer unitTestCase)
        {
            _UnitTestContainerManager.RegisterUnitTestInterface(interfaceType, methodName, unitTestCase);
            return this;
        }

        /// <summary>
        /// 运行注册的
        /// </summary>
        public void Run(string assemblyName)
        {
            _UnitTestContainerManager.GetUnitTestContainerList(assemblyName);
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
