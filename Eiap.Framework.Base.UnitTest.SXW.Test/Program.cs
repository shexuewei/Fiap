using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.LoadAllAssembly().Register(DependencyManager.Instance.Register).Register(UnitTestManager.Instance.Register);
            //注册测试用例
            //IUnitTestManager _unitTestManager = DependencyManager.Instance.Resolver<IUnitTestManager>();
            //_unitTestManager.Register(typeof(IUnitTestAppInterface), "Add", new UnitTestCaseContainer { CaseAssertType = UnitTestCaseAssertType.AssertEquals, MethodParas = new List<object> { 1, 2 }, MethodReturn = 3 })
            //.Run(Assembly.GetExecutingAssembly().FullName);
            IUnitTestCaseManager unitTestCaseManager = DependencyManager.Instance.Resolver<IUnitTestCaseManager>();
            List<UnitTestCaseContainer> list = unitTestCaseManager.GetUnitTestCaseByMethodName("Eiap.Framework.Base.UnitTest.SXW.Test","IUnitTestAppInterface","Add");
            Console.WriteLine(list.Count);
            Console.ReadLine();
        }
    }

    public interface IUnitTestAppInterface : IRealtimeDependency
    {
        void Add(int a, int b);
    }
}
