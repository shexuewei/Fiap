using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW
{
    public class UnitTestMethodContainerManager : IUnitTestMethodContainerManager
    {
        List<UnitTestMethodContainer> _UnitTestMethodContainerList = null;

        public UnitTestMethodContainerManager()
        {
            _UnitTestMethodContainerList = new List<UnitTestMethodContainer>();
        }

        public UnitTestMethodContainer RegisterUnitTestMethod(MethodInfo methodInfo, UnitTestCaseContainer unitTestCase)
        {
            UnitTestMethodContainer _unitTestMethodContainer = GetUnitTestMethodContainer(methodInfo);
            _unitTestMethodContainer.CaseList.Add(unitTestCase);
            return _unitTestMethodContainer;
        }

        /// <summary>
        /// 获取测试方法
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private UnitTestMethodContainer GetUnitTestMethodContainer(MethodInfo methodInfo)
        {
            foreach (UnitTestMethodContainer container in _UnitTestMethodContainerList)
            {
                if (IsExistSameUnitTestMethod(container.TestMethodInfo, methodInfo))
                {
                    return container;
                }
            }
            return CreateNewUnitTestMethodContainer(methodInfo);
        }

        /// <summary>
        /// 判断两个方法是否相同
        /// </summary>
        /// <param name="methodInfoOne"></param>
        /// <param name="methodInfoTwo"></param>
        /// <returns></returns>
        private bool IsExistSameUnitTestMethod(MethodInfo methodInfoOne, MethodInfo methodInfoTwo)
        {
            if (methodInfoOne.Name == methodInfoTwo.Name
                && methodInfoOne.GetParameters().Count() == methodInfoTwo.GetParameters().Count()
                && methodInfoOne.ReturnType.FullName == methodInfoTwo.ReturnType.FullName)
            {
                ParameterInfo[] methodInfoOneParas = methodInfoOne.GetParameters();
                ParameterInfo[] methodInfoTwoParas = methodInfoTwo.GetParameters();
                int parasCount = methodInfoOneParas.Count();
                for (int i = 0; i < parasCount; i++)
                {
                    if (methodInfoOneParas[i].Name == methodInfoTwoParas[i].Name
                        && methodInfoOneParas[i].ParameterType.FullName == methodInfoTwoParas[i].ParameterType.FullName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 根据测试方法获取测试方法容器
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private UnitTestMethodContainer CreateNewUnitTestMethodContainer(MethodInfo methodInfo)
        {
            UnitTestMethodContainer _container = new UnitTestMethodContainer { TestMethodInfo = methodInfo };
            _UnitTestMethodContainerList.Add(_container);
            return _container;
        }

        /// <summary>
        /// 根据方法容器和方法获取用例集合
        /// </summary>
        /// <param name="methodContainer"></param>
        /// <param name="testMethodInfo"></param>
        /// <returns></returns>
        public List<UnitTestCaseContainer> GetUnitTestCaseContainerListByMethod(List<UnitTestMethodContainer> methodContainer, MethodInfo testMethodInfo)
        {
            UnitTestMethodContainer container = methodContainer.Where(m => IsExistSameUnitTestMethod(m.TestMethodInfo, testMethodInfo)).FirstOrDefault();
            if (container != null)
            {
                return container.CaseList;
            }
            return null;
        }
    }
}
