using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Dependency.SXW;
using Eiap.Framework.Base.DynamicProxy;
using Eiap.Framework.Base.DynamicProxy.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Temp.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //DynamicProxyInterceptor.Instance.MethodBeginEvent
            AssemblyManager.Instance.Register(DependencyManager.Instance.Register);
            var test = (ITest)DependencyManager.Instance.Resolver(typeof(ITest));
            int xx = test.Add(1, 2);
            Console.WriteLine(xx);
            Console.ReadLine();
        }
    }

    public interface ITest : IRealtimeDependency
    {
        int Add(int a, int b);
    }

    public class Test : ITest
    {
        public int Add(int a, int b)
        {
            int c = a + b;
            Console.WriteLine(c);
            return c;
        }
    }
}
