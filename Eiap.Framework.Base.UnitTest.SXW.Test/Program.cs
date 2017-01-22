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
            AssemblyManager.Instance.AssemblyInitialize().Register(DependencyManager.Instance.Register).Register(UnitTestManager.Instance.Register);
            //注册测试用例
            UnitTestManager.Instance.Run("Eiap.Framework.Base.UnitTest.SXW.Test").Print("Eiap.Framework.Base.UnitTest.SXW.Test");
            Console.ReadLine();
        }
    }

    public interface IUnitTestAppInterface : IRealtimeDependency
    {
        int Add(int a, int b);
    }

    public class UnitTestApp : IUnitTestAppInterface
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
