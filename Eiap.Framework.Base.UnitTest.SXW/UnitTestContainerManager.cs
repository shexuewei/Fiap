using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestContainerManager : IUnitTestContainerManager
    {
        List<UnitTestContainer> _UnitTestContainerList = null;

        private readonly IUnitTestMethodContainerManager _UnitTestMethodContainerManager;

        public UnitTestContainerManager(IUnitTestMethodContainerManager unitTestMethodContainerManager)
        {
            _UnitTestContainerList = new List<UnitTestContainer>();
            _UnitTestMethodContainerManager = unitTestMethodContainerManager;
        }

        /// <summary>
        /// 注册测试接口
        /// </summary>
        /// <param name="unitTestInterface"></param>
        /// <param name="methodName"></param>
        /// <param name="unitTestCase"></param>
        /// <returns></returns>
        public UnitTestContainer RegisterUnitTestInterface(Type unitTestInterface, string methodName,UnitTestCaseContainer unitTestCase)
        {
            UnitTestContainer _unitTestContainer = null;
            if(unitTestInterface.IsInterface)
            {

                UnitTestMethodContainer _unitTestMethodContainer = _UnitTestMethodContainerManager.RegisterUnitTestMethod(unitTestInterface.GetMethod(methodName), unitTestCase);
                _unitTestContainer = GetUnitTestContainer(unitTestInterface);
                _unitTestContainer.TestMethodList.Add(_unitTestMethodContainer);
            }
            return _unitTestContainer;
        }

        /// <summary>
        /// 根据测试接口类型获取测试接口容器
        /// </summary>
        /// <param name="unitTestInterface"></param>
        /// <returns></returns>
        private UnitTestContainer GetUnitTestContainer(Type unitTestInterface)
        {
            foreach (UnitTestContainer container in _UnitTestContainerList)
            {
                if (IsExistSameUnitTestInterface(container.UnitTestInterfaceType, unitTestInterface))
                {
                    return container;
                }
            }
            return CreateUnitTestContainer(unitTestInterface);
        }

        /// <summary>
        /// 判断两个接口是否相同
        /// </summary>
        /// <param name="unitTestInterfaceOne"></param>
        /// <param name="unitTestInterfaceTwo"></param>
        /// <returns></returns>
        private bool IsExistSameUnitTestInterface(Type unitTestInterfaceOne, Type unitTestInterfaceTwo) 
        {
            if (unitTestInterfaceOne.FullName == unitTestInterfaceTwo.FullName)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///根据测试接口类型创建测试接口容器
        /// </summary>
        /// <param name="unitTestInterface"></param>
        /// <returns></returns>
        private UnitTestContainer CreateUnitTestContainer(Type unitTestInterface)
        {
            UnitTestContainer _container = new UnitTestContainer { UnitTestInterfaceType = unitTestInterface };
            _UnitTestContainerList.Add(_container);
            return _container;
        }

        /// <summary>
        /// 根据测试接口类型名称获取测试容器
        /// </summary>
        /// <param name="interfaceTypeName"></param>
        /// <returns></returns>
        public UnitTestContainer GetUnitTestContainer(string interfaceTypeName)
        {
            return _UnitTestContainerList.Where(m => m.UnitTestInterfaceType.FullName == interfaceTypeName).FirstOrDefault();
        }
    }
}
