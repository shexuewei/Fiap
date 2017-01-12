using Eiap.Framework.Base.Dependency.SXW;
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
        private static IUnitTestManager _Manager = null;

        /// <summary>
        /// 单例对象
        /// </summary>
        public static IUnitTestManager Instance
        {
            get
            {
                if (_Manager == null)
                {
                    _Manager = new UnitTestManager();
                }
                return _Manager;
            }
        }

        /// <summary>
        /// 根据程序集注册用例
        /// </summary>
        /// <param name="assemblyList"></param>
        public void Register(List<Assembly> assemblyList)
        {
            assemblyList.ForEach(m => {
                List<Type> typeList = m.GetTypes().Where(n => n.IsClass && typeof(IUnitTestModule).IsAssignableFrom(n)).ToList();
                if (typeList != null && typeList.Count > 0)
                {
                    typeList.ForEach(typeItem => {
                        IUnitTestModule unitTestModule = (IUnitTestModule)Activator.CreateInstance(typeItem);
                        unitTestModule.RegisterUnitTestCase();
                    });
                }
            });
        }

        public IUnitTestManager Run(string unitTestNamespace)
        {
            IUnitTestCaseManager unitTestCaseManager = DependencyManager.Instance.Resolver<IUnitTestCaseManager>();
            IUnitTestResultManager unitTestResultManager = DependencyManager.Instance.Resolver<IUnitTestResultManager>();
            List<UnitTestCaseContainer> unitTestCaseContainerList = unitTestCaseManager.GetUnitTestCaseByNamespace(unitTestNamespace);
            foreach (UnitTestCaseContainer unitTestCase in unitTestCaseContainerList)
            {
                object unitTestInstance = DependencyManager.Instance.Resolver(unitTestCase.UnitTestClassType);
                object unitTestInstanceMethodReturn = null;
                Exception tmpException = null;
                UnitTestResultContainer unitTestResult = new UnitTestResultContainer
                {
                    ClassName = unitTestCase.ClassName,
                    MethodName = unitTestCase.MethodName,
                    UnitTestNamespace = unitTestCase.UnitTestNamespace,
                    MethodParas = unitTestCase.MethodParas
                };
                try
                {
                    unitTestInstanceMethodReturn = unitTestInstance.GetType().GetMethod(unitTestCase.MethodName).Invoke(unitTestInstance, unitTestCase.MethodParas.ToArray());
                }
                catch (Exception ex)
                {
                    tmpException = ex;
                }

                switch (unitTestCase.CaseAssertType)
                {
                    case UnitTestCaseAssertType.AssertEquals:
                        if (unitTestInstanceMethodReturn.Equals(unitTestCase.MethodReturn))
                        {
                            unitTestResult.Result = true;
                        }
                        else
                        {
                            SetUnitTestResultFalse(unitTestResult, tmpException);
                        }
                        break;
                    case UnitTestCaseAssertType.AssertSame:
                        if (unitTestInstanceMethodReturn == unitTestCase.MethodReturn)
                        {
                            unitTestResult.Result = true;
                        }
                        else
                        {
                            SetUnitTestResultFalse(unitTestResult, tmpException);
                        }
                        break;
                    case UnitTestCaseAssertType.AssertFalse:
                        if (unitTestInstanceMethodReturn is bool && !(bool)unitTestInstanceMethodReturn)
                        {
                            unitTestResult.Result = true;
                        }
                        else
                        {
                            SetUnitTestResultFalse(unitTestResult, tmpException);
                        }
                        break;
                    case UnitTestCaseAssertType.AssertTrue:
                        if (unitTestInstanceMethodReturn is bool && (bool)unitTestInstanceMethodReturn)
                        {
                            unitTestResult.Result = true;
                        }
                        else
                        {
                            SetUnitTestResultFalse(unitTestResult, tmpException);
                        }
                        break;
                    case UnitTestCaseAssertType.AssertNull:
                        if (unitTestInstanceMethodReturn == null)
                        {
                            unitTestResult.Result = true;
                        }
                        else
                        {
                            SetUnitTestResultFalse(unitTestResult, tmpException);
                        }
                        break;
                    case UnitTestCaseAssertType.AssertNotNull:
                        if (unitTestInstanceMethodReturn != null)
                        {
                            unitTestResult.Result = true;
                        }
                        else
                        {
                            SetUnitTestResultFalse(unitTestResult, tmpException);
                        }
                        break;
                    case UnitTestCaseAssertType.AssertNotSame:
                        if (unitTestInstanceMethodReturn != unitTestCase.MethodReturn)
                        {
                            unitTestResult.Result = true;
                        }
                        else
                        {
                            SetUnitTestResultFalse(unitTestResult, tmpException);
                        }
                        break;
                }
                unitTestResultManager.AddUnitTestResult(unitTestResult);
            }
            return this;
        }

        /// <summary>
        /// 设置单元测试结果为False
        /// </summary>
        /// <param name="unitTestResult"></param>
        /// <param name="result"></param>
        /// <param name="tmpException"></param>
        private void SetUnitTestResultFalse(UnitTestResultContainer unitTestResult, Exception tmpException)
        {
            unitTestResult.Result = false;
            if (tmpException != null)
            {
                unitTestResult.MethodException = tmpException;
            }
        }

        /// <summary>
        /// 输出指定命名空间下的测试结果
        /// </summary>
        /// <param name="unitTestNamespace"></param>
        /// <returns></returns>
        public IUnitTestManager Print(string unitTestNamespace)
        {
            IUnitTestResultManager unitTestResultManager = DependencyManager.Instance.Resolver<IUnitTestResultManager>();
            unitTestResultManager.PrintUnitTestResultByNamespace(unitTestNamespace);
            return this;
        }
    }
}
