using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.Base.Dependency.SXW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.DynamicProxy.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.LoadAllAssembly().Register(DependencyManager.Instance.Register);
            //普通方法
            IAddTest1 t = DependencyManager.Instance.Resolver<IAddTest1>();
            Console.WriteLine(t.Add(1, 1));

            //泛型方法
            IAddTest2 t2 = DependencyManager.Instance.Resolver<IAddTest2>();
            Console.WriteLine(t2.Add2<AddTest, AddTest, AddTest>(new AddTest(), new AddTest(), 1, 1).Add(2, 2));

            IAddTest3<AddTest> t3 = DependencyManager.Instance.Resolver<IAddTest3<AddTest>>();
            Console.WriteLine(t3.Add3<AddTest, AddTest>(new AddTest(), new AddTest(), 1, 1).Add(3, 3));

            IAddTest4<AddTest> t4 = DependencyManager.Instance.Resolver<IAddTest4<AddTest>>();
            Console.WriteLine(t4.Add(5, 5));

            Console.ReadLine();
        }
    }

    public class TestModule : IComponentModule
    {
        public void Initialize()
        {
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public interface IAddTest1 : IRealtimeDependency
    {
        int Add(int a, int b);
    }

    public interface IAddTest2 : IRealtimeDependency
    {
        T Add2<T, K, Q>(T t, K k, int a, int b)
            where T : class, new()
            where K : IAddTest1
            where Q : IAddTest1;
    }

    public interface IAddTest3<Q> : IRealtimeDependency where Q : class, IAddTest1, new()
    {
        Q Add3<T, K>(T t, K k, int a, int b)
            where T : class, new()
            where K : IAddTest1;
    }

    public class AddTest : IAddTest1, IAddTest2, IAddTest3<AddTest>
    {
        public int Add(int a, int b)
        {
            return a + b;
        }


        public AddTest Add3<T, K>(T t, K k, int a, int b)
            where T : class, new()
            where K : IAddTest1
        {
            return new AddTest();
        }

        public T Add2<T, K, Q>(T t, K k, int a, int b)
            where T : class, new()
            where K : IAddTest1
            where Q : IAddTest1
        {
            return new T();
        }
    }

    public interface IAddTest4<T> : IRealtimeDependency
        where T : class,IAddTest1, new()
    {
        T Add(int a, int b);
    }

    public class AddTest4<T> : IAddTest4<T>
        where T : class, IAddTest1,new()
    {
        public T Add(int a, int b)
        {
            return new T();
        }
    }
}
