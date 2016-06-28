using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.UnitTest.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.LoadAllAssembly().Register(DependencyManager.Instance.Register);
        }
    }

    public interface IUnitTestAppInterface : IRealtimeDependency
    {
        void Add(int a, int b);
    }
}
