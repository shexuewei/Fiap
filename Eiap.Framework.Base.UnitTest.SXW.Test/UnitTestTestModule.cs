using Eiap.Framework.Base.AssemblyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.Dependency.SXW;

namespace Eiap.Framework.Base.UnitTest.SXW.Test
{
    public class UnitTestTestModule : IComponentModule, IUnitTestModule
    {
        public void AssemblyInitialize()
        {
            //注册当前程序集
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());

        }

        public void RegisterInitialize()
        {
            
        }

        /// <summary>
        /// 注册单元测试用例
        /// </summary>
        public void RegisterUnitTestCase()
        {
            IUnitTestCaseManager unitTestCaseManager = DependencyManager.Instance.Resolver<IUnitTestCaseManager>();
            unitTestCaseManager.RegisterUnitTestCase(new UnitTestCaseContainer
            {
                UnitTestNamespace = "Eiap.Framework.Base.UnitTest.SXW.Test",
                ClassName = "IUnitTestAppInterface",
                MethodName = "Add",
                MethodParas = new List<object> { 3, 5 },
                CaseAssertType = UnitTestCaseAssertType.AssertEquals,
                UnitTestClassTypeHandle = typeof(IUnitTestAppInterface).TypeHandle,
                MethodReturn = 7
            });
        }
    }
}
