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
    public class UnitTestModule : IComponentModule
    {
        public void Initialize()
        {
            //注册当前程序集
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());

            //注册测试用例
            IUnitTestManager _unitTestManager = DependencyManager.Instance.Resolver<IUnitTestManager>();
            _unitTestManager.Register<IUnitTestAppInterface>("Add", new UnitTestCaseContainer { CaseAssertType = UnitTestCaseAssertType.AssertEquals, MethodParas = new List<object> { 1, 2 }, MethodReturn = 3 })
                ;
        }
    }
}
