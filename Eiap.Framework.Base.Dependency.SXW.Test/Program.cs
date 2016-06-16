using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.AssemblyService;
using System.Reflection;

namespace Eiap.Framework.Base.Dependency.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.LoadAllAssembly(@"C:\MyWork\EiapV3.0\Eiap.Framework\Eiap.Framework.Base.Dependency.SXW.Test\bin\Debug\")
                .Register(DependencyManager.Instance.Register);
            //根据接口返回接口实现类对象
            //ISchoolManager schoolManager = (ISchoolManager)DependencyManager.Instance.Resolver(typeof(ISchoolManager));
            //根据泛型接口返回实现类对象
            ISchoolManager schoolManager = DependencyManager.Instance.Resolver<ISchoolManager>();
            //根据实现类返回实现类对象
            //SchoolManager schoolManager = (SchoolManager)DependencyManager.Instance.Resolver(typeof(SchoolManager));
            School sch = schoolManager.Create("123", 10);
            Console.WriteLine(sch.Name + ":" + sch.Age);
            Console.ReadLine();
        }
    }

    public class School
    {
        public string Name { get; set; }

        public int Age { get; set; }

        List<Student> StudentList { get; set; }
    }

    public class Student {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }
    }

    public interface ISchoolManager : IRealtimeDependency
    {
        School Create(string name, int age);
    }

    public class SchoolManager : ISchoolManager
    {
        public School Create(string name, int age)
        {
            return new School { Name = name, Age = age };
        }
    }

    public class DependencyTestModule : IComponentModule
    {
        public void Initialize()
        {
            AssemblyManager.Instance.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
