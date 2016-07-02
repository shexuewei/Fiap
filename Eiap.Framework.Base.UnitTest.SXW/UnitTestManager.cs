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
        public IUnitTestManager Register<T>(string methodName, UnitTestCaseContainer unitTestCase)
        {
            _UnitTestContainerManager.RegisterUnitTestInterface(typeof(T), methodName, unitTestCase);
            return this;
        }

        /// <summary>
        /// 根据测试接口类型名称和方法获取用例集合
        /// </summary>
        /// <param name="interfaceTypeName"></param>
        /// <param name="testMethodInfo"></param>
        /// <returns></returns>
        public List<UnitTestCaseContainer> GetUnitTestCaseByInterfaceTypeNameAndMethodName(string interfaceTypeName, MethodInfo testMethodInfo)
        {
            UnitTestContainer unitTestContainer = _UnitTestContainerManager.GetUnitTestContainer(interfaceTypeName);
            return _UnitTestMethodContainerManager.GetUnitTestCaseContainerListByMethod(unitTestContainer.TestMethodList, testMethodInfo);
        }
    }
}
