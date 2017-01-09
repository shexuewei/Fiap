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

        public IUnitTestManager UnitTest(string unitTestNamespace)
        {
            IUnitTestCaseManager unitTestCaseManager = DependencyManager.Instance.Resolver<IUnitTestCaseManager>();
            List<UnitTestCaseContainer> unitTestCaseContainerList = unitTestCaseManager.GetUnitTestCaseByNamespace(unitTestNamespace);
            foreach (UnitTestCaseContainer unitTestCase in unitTestCaseContainerList)
            {
                object obj = DependencyManager.Instance.Resolver(unitTestCase.UnitTestClassType);
                var xx = obj.GetType().GetMethod(unitTestCase.MethodName).Invoke(obj, unitTestCase.MethodParas.ToArray());
                switch (unitTestCase.CaseAssertType)
                {
                    case UnitTestCaseAssertType.AssertEquals:
                        if (xx.Equals(unitTestCase.MethodReturn))
                        {

                        }
                        break;
                }
            }
            
            return this;
        }
    }
}
